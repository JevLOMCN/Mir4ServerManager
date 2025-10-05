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

            this.Load += async (_, __) => await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            try
            {
                UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;

                const string sql = "SELECT * FROM character_tb";
                DataTable dt = await Database.QueryAsync("mm_game_db_release", sql);
                _bs.DataSource = dt;
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
