﻿namespace xCheatsFunctions
{
    public static class GlobalSettings
    {
        private static readonly IniConfig config;
        private static readonly string configFilePath;

        static GlobalSettings() // Static constructor
        {
            config = new IniConfig();
             string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
             string appFolderPath = Path.Combine(appDataPath, "xCheats");
             configFilePath = Path.Combine(appFolderPath, "data\\Config\\config.ini");
            LoadSettings();
        }

        public static bool AdminMode { get; set; }
        public static bool isOfflineMode { get; set; }
        public static bool BackgroundWork { get; set; }

        private static void LoadSettings()
        {
            config.Load(configFilePath);
            bool.TryParse(config.GetValue("Settings", "AdminMode"), out bool adminModeValue);
            bool.TryParse(config.GetValue("Settings", "OfflineMode"), out bool offlineModeValue);
            bool.TryParse(config.GetValue("Settings", "BackgroundWork"), out bool backgroundWorkValue);
            BackgroundWork = backgroundWorkValue;
            AdminMode = adminModeValue;
            isOfflineMode = offlineModeValue;
        }
    }
}
