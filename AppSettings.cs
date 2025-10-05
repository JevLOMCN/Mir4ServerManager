using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServerManager
{
    public sealed class AppSettings
    {
        private static readonly JsonSerializerOptions _opts = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true,
        };

        public static string SettingsPath { get; private set; } =
            Path.Combine(AppContext.BaseDirectory, "settings.json");

        public SqlSettings Sql { get; set; } = new SqlSettings();

        public sealed class SqlSettings
        {
            public string Ip { get; set; } = "127.0.0.1";
            public int Port { get; set; } = 3306;
            public string Username { get; set; } = "dev";
            public string Password { get; set; } = "";
        }

        public static AppSettings Current { get; private set; } = new AppSettings();

        public static void Initialize(string? customPath = null)
        {
            if (!string.IsNullOrWhiteSpace(customPath))
                SettingsPath = customPath!;

            try
            {
                if (!File.Exists(SettingsPath))
                {
                    // Create a default file on first run
                    Save(new AppSettings());
                    Current = new AppSettings();
                    return;
                }

                var json = File.ReadAllText(SettingsPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json, _opts);
                Current = loaded ?? new AppSettings();
            }
            catch
            {
                // Fail safe: keep defaults if anything goes wrong
                Current = new AppSettings();
            }
        }

        public static void Save(AppSettings? settings = null)
        {
            var json = JsonSerializer.Serialize(settings ?? Current, _opts);
            File.WriteAllText(SettingsPath, json);
        }
    }
}
