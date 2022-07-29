namespace Project
{
    public enum Prefs
    {
        // Default
        [Pref("Undefined", null)] Undefined = 0,
        
        // General
        [Pref("SettingsDone", false)] SettingsDone = 50,

        // Settings
        [Pref("MasterVolume", 0f)] MasterVolume = 100,
        [Pref("MasterEnabled", true)] MasterEnabled = 101,
        [Pref("SoundsVolume", 1f)] SoundsVolume = 102,
        [Pref("SoundsEnabled", true)] SoundsEnabled = 103,
        [Pref("MusicVolume", 1f)] MusicVolume = 104,
        [Pref("MusicEnabled", true)] MusicEnabled = 105,
        
        [Pref("Gamma", 0f)] Gamma = 106
    }
}
