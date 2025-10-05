using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
// using ServerManager; // same assembly; add if your IDE complains

namespace ServerManager.Views
{
    public partial class PlayersList : UserControl
    {
        private readonly BindingSource _bs = new BindingSource();

        public PlayersList()
        {
            InitializeComponent();

            // Grid behavior (keep here so you can tweak in Designer if you like)
            PlayersListDataGrid.ReadOnly = true;
            PlayersListDataGrid.AllowUserToAddRows = false;
            PlayersListDataGrid.AllowUserToDeleteRows = false;
            PlayersListDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PlayersListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PlayersListDataGrid.AutoGenerateColumns = true;
            PlayersListDataGrid.RowHeadersVisible = false;
            PlayersListDataGrid.BorderStyle = BorderStyle.None;

            PlayersListDataGrid.DataSource = _bs;

            this.Load += async (_, __) => await LoadDataAsync();
        }

        /// <summary>
        /// Loads all characters from mm_game_db_release.character_tb into the grid.
        /// </summary>
        public async Task LoadDataAsync()
        {
            try
            {
                UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;

                const string sql = "SELECT * FROM character_tb";
                DataTable dt = await Database.QueryAsync("mm_game_db_release", sql);
                _bs.DataSource = dt;

                PlayersListDataGrid.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PlayersList.LoadDataAsync error: {ex}");
                MessageBox.Show(this, $"Failed to load character list:\n{ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bs.DataSource = null;
            }
            finally
            {
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
            }
        }

        public Task RefreshDataAsync() => LoadDataAsync();
    }
}
