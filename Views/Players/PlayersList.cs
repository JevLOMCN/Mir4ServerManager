using System.Diagnostics;

namespace ServerManager.Views
{
    public partial class PlayersList : UserControl
    {
        private readonly BindingSource _bs = new BindingSource();

        public PlayersList()
        {
            InitializeComponent();

            // Grid defaults (you can still tweak via Designer)
            PlayersListDataGrid.ReadOnly = true;
            PlayersListDataGrid.AllowUserToAddRows = false;
            PlayersListDataGrid.AllowUserToDeleteRows = false;
            PlayersListDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PlayersListDataGrid.AutoGenerateColumns = true;
            PlayersListDataGrid.BorderStyle = BorderStyle.None;
            PlayersListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; 
            PlayersListDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            PlayersListDataGrid.DataSource = _bs;

            this.Load += async (_, __) => await LoadDataAsync();
        }

        /// <summary>Load all characters from mm_game_db_release.character_tb.</summary>
        public async Task LoadDataAsync()
        {
            try
            {
                UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;

                // Ensure configured (safe if already done)
                AppSettings.Initialize();
                Database.ConfigureAll(AppSettings.Current.Sql);

                const string cols =
                    "`CharacterUID`,`CharacterName`,`AccountUID`,`Class`,`Lev`,`Exp`,`CurrentHP`,`CurrentMP`,`StageIdx`," +
                    "`PositionX`,`PositionY`,`PositionZ`,`Wonbo`,`Gold`,`CreateTime`,`LoginTime`,`LogoutTime`,`PlaytimeSec`," +
                    "`CombatPoint`,`EnergyPoint`,`BlackIron`,`AncientCoin`,`ActionPoint`,`LastActionPointUpdateTime`," +
                    "`AwakenGrade`,`InvenExtendCnt`,`PKPoint`,`CostumeIdx`";

                var dt = await Database.QueryAsync("mm_game_db_release",
                    $"SELECT {cols} FROM `character_tb` ORDER BY `CreateTime` DESC");

                Debug.WriteLine($"[PlayersList] Loaded {dt.Rows.Count} rows.");
                _bs.DataSource = dt;

                PlayersListDataGrid.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PlayersList.LoadDataAsync error: {ex}");
                MessageBox.Show(this,
                    $"Failed to load character list:\n{ex.Message}",
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
