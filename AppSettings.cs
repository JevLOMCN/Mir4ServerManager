// File: AppSettings.cs
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServerManager
{
    /// <summary>
    /// JSON-backed application settings. Reads/writes "settings.json" next to the executable by default.
    /// </summary>
    public sealed class AppSettings
    {
        // --- JSON options ---
        private static readonly JsonSerializerOptions _opts = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>Full path to the settings file currently in use.</summary>
        public static string SettingsPath { get; private set; } =
            Path.Combine(AppContext.BaseDirectory, "settings.json");

        /// <summary>The singleton instance loaded into memory.</summary>
        public static AppSettings Current { get; private set; } = new AppSettings();

        /// <summary>SQL connection settings used by the app.</summary>
        public SqlSettings Sql { get; set; } = new SqlSettings();

        /// <summary>
        /// Root folder for server data (logs, configs, etc.). If blank, defaults to "<app>\ServerData".
        /// Supports environment variables like "%PROGRAMDATA%\TopazMir4".
        /// </summary>
        public string ServerData { get; set; } = "";

        /// <summary>
        /// Load settings from disk (or create defaults if missing).
        /// Call once at startup before using <see cref="Current"/>.
        /// </summary>
        /// <param name="customPath">Optional custom path to the JSON file.</param>
        /// <param name="createIfMissing">If true, writes a default file when not found.</param>
        public static void Initialize(string? customPath = null, bool createIfMissing = true)
        {
            if (!string.IsNullOrWhiteSpace(customPath))
                SettingsPath = customPath!;

            try
            {
                if (!File.Exists(SettingsPath))
                {
                    var fresh = new AppSettings();
                    fresh.ValidateAndNormalize();
                    if (createIfMissing) Save(fresh);
                    Current = fresh;
                    return;
                }

                var json = File.ReadAllText(SettingsPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json, _opts) ?? new AppSettings();
                loaded.ValidateAndNormalize();
                Current = loaded;
            }
            catch
            {
                // Failsafe: keep defaults if anything goes wrong
                var fresh = new AppSettings();
                fresh.ValidateAndNormalize();
                Current = fresh;
            }
        }

        /// <summary>Persist settings to disk. If <paramref name="settings"/> is null, saves <see cref="Current"/>.</summary>
        public static void Save(AppSettings? settings = null)
        {
            var toSave = settings ?? Current;
            toSave.ValidateAndNormalize();

            var json = JsonSerializer.Serialize(toSave, _opts);
            var dir = Path.GetDirectoryName(SettingsPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(SettingsPath, json);
        }

        /// <summary>
        /// Ensure required fields have sane values and normalize paths/values.
        /// </summary>
        private void ValidateAndNormalize()
        {
            // SQL defaults
            if (Sql == null) Sql = new SqlSettings();
            if (string.IsNullOrWhiteSpace(Sql.Ip)) Sql.Ip = "127.0.0.1";
            if (Sql.Port <= 0 || Sql.Port > 65535) Sql.Port = 3306;
            if (string.IsNullOrWhiteSpace(Sql.Username)) Sql.Username = "dev";
            if (Sql.Password is null) Sql.Password = string.Empty;

            // ServerData path: default to "<app>\ServerData" if empty
            if (ServerData is null) ServerData = "";
            ServerData = ServerData.Trim();
            if (string.IsNullOrWhiteSpace(ServerData))
            {
                ServerData = Path.Combine(AppContext.BaseDirectory, "ServerData");
            }
            else
            {
                // Expand %ENV% and normalize to full path
                var expanded = Environment.ExpandEnvironmentVariables(ServerData);
                try
                {
                    ServerData = Path.GetFullPath(expanded);
                }
                catch
                {
                    // If invalid, fall back to default under app dir
                    ServerData = Path.Combine(AppContext.BaseDirectory, "ServerData");
                }
            }
        }

        /// <summary>Settings for MySQL connectivity.</summary>
        public sealed class SqlSettings
        {
            public string Ip { get; set; } = "127.0.0.1";
            public int Port { get; set; } = 3306;
            public string Username { get; set; } = "dev";
            public string Password { get; set; } = "1111";
        }
    }
}
