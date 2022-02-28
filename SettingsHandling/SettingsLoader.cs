using System.Text.Json;

namespace PathFindingAI.SettingsHandling
{
    internal static class SettingsLoader
    {
        private const string settingsFileName = "app-settings.json";
        public static AppSettings Load()
        {
            if (File.Exists(settingsFileName))
            {
                using StreamReader streamReader = new(settingsFileName);

                var settings = JsonSerializer.Deserialize<AppSettings>(streamReader.ReadToEnd());
                return settings;

            }
            else
            {
                var defaultSettings = new AppSettings(width: 800, height: 800, cellSizeLength: 20, margin: 40);

                using StreamWriter streamWriter = new(settingsFileName);
                streamWriter.Write(JsonSerializer.Serialize<AppSettings>(defaultSettings));
                return defaultSettings;
            }

        }
    }
}
