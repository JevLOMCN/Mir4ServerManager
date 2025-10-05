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
                    if (createIfMissing)
                    {
                        // Create a default file with safe dev defaults
                        var fresh = new AppSettings();
                        fresh.Validate();
                        Save(fresh);
                        Current = fresh;
                    }
                    else
                    {
                        var fresh = new AppSettings();
                        fresh.Validate();
                        Current = fresh;
                    }
                    return;
                }

                var json = File.ReadAllText(SettingsPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json, _opts) ?? new AppSettings();
                loaded.Validate();
                Current = loaded;
            }
            catch
            {
                // Failsafe: keep defaults if anything goes wrong
                var fresh = new AppSettings();
                fresh.Validate();
                Current = fresh;
            }
        }

        /// <summary>
        /// Persist settings to disk. If <paramref name="settings"/> is null, saves <see cref="Current"/>.
        /// </summary>
        public static void Save(AppSettings? settings = null)
        {
            var toSave = settings ?? Current;
            toSave.Validate();

            var json = JsonSerializer.Serialize(toSave, _opts);
            var dir = Path.GetDirectoryName(SettingsPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(SettingsPath, json);
        }

        /// <summary>
        /// Ensure required fields have sane values (avoids empty/invalid settings causing implicit localhost, etc.).
        /// </summary>
        private void Validate()
        {
            if (Sql == null) Sql = new SqlSettings();

            // Sensible dev defaults
            if (string.IsNullOrWhiteSpace(Sql.Ip)) Sql.Ip = "127.0.0.1";
            if (Sql.Port <= 0 || Sql.Port > 65535) Sql.Port = 3306;
            if (string.IsNullOrWhiteSpace(Sql.Username)) Sql.Username = "root";
            if (Sql.Password is null) Sql.Password = string.Empty;
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
