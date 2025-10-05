using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static ServerManager.ServiceChecker;
using Timer = System.Windows.Forms.Timer;

namespace ServerManager
{
    public partial class MainForm : Form
    {
        // ---- Service status polling ----
        private readonly Timer _statusTimer = new Timer();
        private Dictionary<string, ToolStripStatusLabel> _labelMap = null!;

        // ---- Paired-view navigation (left+right panels) ----
        private readonly Stack<(Control Left, Control Right)> _navStack = new();
        private Control? _currentLeft;
        private Control? _currentRight;

        public MainForm()
        {
            InitializeComponent();

            // ---- Status labels map ----
            _labelMap = new Dictionary<string, ToolStripStatusLabel>(StringComparer.OrdinalIgnoreCase)
            {
                ["ChattingServer"] = ChattingServerLabel,
                ["FrontServer"] = FrontServerLabel,
                ["GameServer"] = GameServerLabel,
                ["GatewayServer"] = GatewayServerLabel,
                ["WorldServer"] = WorldServerLabel,
                ["MySQL"] = MySQLLabel,
                ["Couchbase"] = CouchbaseLabel,
                ["Memurai"] = MemuraiLabel
            };

            // ---- Timer for status checking ----
            _statusTimer.Interval = 2000;
            _statusTimer.Tick += (_, __) => RefreshStatuses();

            // ---- Form events ----
            Load += MainForm_Load;
            FormClosed += (_, __) => _statusTimer.Stop();

            // ---- Keyboard back navigation ----
            KeyPreview = true;
            KeyDown += MainForm_KeyDown;

            // Optional: smoother scrolling in the view panel
            ViewPanel.SuspendLayout();
            ViewPanel.AutoScroll = true;
            ViewPanel.ResumeLayout();
        }

        #region Form Load + Navigation Root
        private void MainForm_Load(object? sender, EventArgs e)
        {
            // Wrap whatever is already in the panels as the "root" pair
            SetupNavigationRoot();

            // Kick off service polling
            _statusTimer.Start();
        }

        private void SetupNavigationRoot()
        {
            _currentLeft = WrapExistingChildren(ButtonPanel);
            _currentRight = WrapExistingChildren(ViewPanel);
        }

        private static Control WrapExistingChildren(Panel host)
        {
            var wrapper = new Panel { Dock = DockStyle.Fill };
            // Move existing children into a single wrapper (don’t dispose)
            while (host.Controls.Count > 0)
            {
                var c = host.Controls[0];
                host.Controls.RemoveAt(0);
                c.Dock = c.Dock; // keep current docking
                wrapper.Controls.Add(c);
            }
            host.Controls.Add(wrapper);
            return wrapper;
        }
        #endregion

        #region Keyboard Back Navigation
        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                GoBack();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GoBack()
        {
            if (_navStack.Count == 0) return;

            // Dispose current pair (not kept in history)
            RemoveCurrentFrom(ButtonPanel, dispose: true);
            RemoveCurrentFrom(ViewPanel, dispose: true);

            // Restore previous pair
            var (prevLeft, prevRight) = _navStack.Pop();
            ReplacePanelContent(ButtonPanel, prevLeft);
            ReplacePanelContent(ViewPanel, prevRight);

            _currentLeft = prevLeft;
            _currentRight = prevRight;
        }
        #endregion

        #region Panel Helpers (no disposal for history safety)
        private static void ReplacePanelContent(Panel host, Control next)
        {
            host.SuspendLayout();
            try
            {
                host.Controls.Clear(); // don’t dispose; we’re reusing from history
                next.Dock = DockStyle.Fill;
                host.Controls.Add(next);
            }
            finally
            {
                host.ResumeLayout();
            }
        }

        private static void RemoveCurrentFrom(Panel host, bool dispose)
        {
            if (host.Controls.Count == 0) return;
            var current = host.Controls[0];
            host.Controls.Clear();
            if (dispose) current.Dispose();
        }
        #endregion

        #region Legacy Helpers (still handy for one-off swaps)
        private void ShowView(Control view)
        {
            ViewPanel.SuspendLayout();
            ViewPanel.Controls.Clear();
            view.Dock = DockStyle.Fill;
            ViewPanel.Controls.Add(view);
            ViewPanel.ResumeLayout();
        }

        private static void ShowInPanel(Panel host, Control view)
        {
            host.SuspendLayout();
            try
            {
                foreach (Control c in host.Controls) c.Dispose(); // one-off swap frees resources
                host.Controls.Clear();
                view.Dock = DockStyle.Fill;
                host.Controls.Add(view);
            }
            finally
            {
                host.ResumeLayout();
            }
        }
        #endregion

        #region Service Status
        private void RefreshStatuses()
        {
            var targets = DefineTargets();
            var results = ServiceChecker.CheckAll(targets);

            foreach (var kv in results)
            {
                if (_labelMap.TryGetValue(kv.Key, out var lbl))
                    SetStatus(lbl, kv.Value);
            }
        }

        private static IEnumerable<Target> DefineTargets()
        {
            return new[]
            {
                new Target { Key = "ChattingServer", Type = TargetType.Process,        Names = new[] { "ChattingServer" } },
                new Target { Key = "FrontServer",    Type = TargetType.Process,        Names = new[] { "FrontServer" } },
                new Target { Key = "GameServer",     Type = TargetType.Process,        Names = new[] { "GameServer" } },
                new Target { Key = "GatewayServer",  Type = TargetType.Process,        Names = new[] { "GatewayServer" } },
                new Target { Key = "WorldServer",    Type = TargetType.Process,        Names = new[] { "WorldServer" } },
                new Target { Key = "MySQL",          Type = TargetType.WindowsService, Names = new[] { "MySQL80", "MySQL57", "MySQL" } },
                new Target { Key = "Couchbase",      Type = TargetType.WindowsService, Names = new[] { "CouchbaseServer" } },
                new Target { Key = "Memurai",        Type = TargetType.WindowsService, Names = new[] { "Memurai" } },
            };
        }

        private static readonly Color OkColor = Color.FromArgb(40, 167, 69);
        private static readonly Color FailColor = Color.FromArgb(220, 53, 69);

        private static void SetStatus(ToolStripStatusLabel label, bool ok)
        {
            label.Text = ok
                ? $"{label.Name.Replace("Label", "")}: Online"
                : $"{label.Name.Replace("Label", "")}: Offline";
            label.BackColor = ok ? OkColor : FailColor;
            label.ForeColor = Color.White;
            label.Margin = new Padding(2);
            label.Padding = new Padding(6, 2, 6, 2);
        }
        #endregion

        #region Buttons (navigation)
        private void PlayersButton_Click(object sender, EventArgs e)
        {
            var left = new ServerManager.Views.PlayersButtons(); // your left-side actions for Players
            var right = new ServerManager.Views.PlayersList();     // the main Players view

            // Push current pair, then swap both panels
            if (_currentLeft != null && _currentRight != null)
            {
                _navStack.Push((_currentLeft, _currentRight));
            }

            ReplacePanelContent(ButtonPanel, left);
            ReplacePanelContent(ViewPanel, right);

            _currentLeft = left;
            _currentRight = right;
        }
        #endregion
    }
}
