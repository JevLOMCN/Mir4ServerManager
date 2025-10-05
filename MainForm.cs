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
        private readonly Timer _statusTimer = new Timer();
        private Dictionary<string, ToolStripStatusLabel> _labelMap = null!;

        public MainForm()
        {
            InitializeComponent();

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

            _statusTimer.Interval = 2000;
            _statusTimer.Tick += (_, __) => RefreshStatuses();
            Load += (_, __) => _statusTimer.Start();
            FormClosed += (_, __) => _statusTimer.Stop();
        }

        #region Refresh Statuses
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
        #endregion

        #region Define Targets
        private static IEnumerable<Target> DefineTargets()
        {
            return new[]
            {
                new Target { Key = "ChattingServer", Type = TargetType.Process,       Names = new[] { "ChattingServer" } },
                new Target { Key = "FrontServer",    Type = TargetType.Process,       Names = new[] { "FrontServer" } },
                new Target { Key = "GameServer",     Type = TargetType.Process,       Names = new[] { "GameServer" } },
                new Target { Key = "GatewayServer",  Type = TargetType.Process,       Names = new[] { "GatewayServer" } },
                new Target { Key = "WorldServer",    Type = TargetType.Process,       Names = new[] { "WorldServer" } },
                new Target { Key = "MySQL",          Type = TargetType.WindowsService, Names = new[] { "MySQL80", "MySQL57", "MySQL" } },
                new Target { Key = "Couchbase",      Type = TargetType.WindowsService, Names = new[] { "CouchbaseServer" } },
                new Target { Key = "Memurai",        Type = TargetType.WindowsService, Names = new[] { "Memurai" } },
            };
        }
        #endregion

        #region Set Status
        private static readonly Color OkColor = Color.FromArgb(40, 167, 69);
        private static readonly Color FailColor = Color.FromArgb(220, 53, 69);
        private static void SetStatus(ToolStripStatusLabel label, bool ok)
        {
            label.Text = ok ? $"{label.Name.Replace("Label", "")}: Online" : $"{label.Name.Replace("Label", "")}: Offline";
            label.BackColor = ok ? OkColor : FailColor;
            label.ForeColor = Color.White;
            label.Margin = new Padding(2);
            label.Padding = new Padding(6, 2, 6, 2);
        }
        #endregion

        #region Buttons
        #region Players Button
        private void PlayersButton_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion
    }
}
