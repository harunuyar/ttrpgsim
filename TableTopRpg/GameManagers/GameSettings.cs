namespace TableTopRpg.GameManagers;

public class GameSettings
{
    private readonly Dictionary<string, string> settingsDict;

    public GameSettings()
    {
        settingsDict = new Dictionary<string, string>();
    }

    public void RemoveSetting(string key)
    {
        settingsDict.Remove(key);
    }

    public string GetSetting(string key)
    {
        return settingsDict[key];
    }

    public void SetSetting(string key, string value)
    {
        settingsDict[key] = value;
    }

    public bool IsEnabled(string key)
    {
        return settingsDict[key] == "true";
    }

    public void EnableSetting(string key)
    {
        settingsDict[key] = "true";
    }

    public void ClearSettings()
    {
        settingsDict.Clear();
    }

    public void LoadSettings(string path)
    {
        var settings = File.ReadAllLines(path);
        foreach (var setting in settings)
        {
            var split = setting.Split("=");
            settingsDict[split[0]] = split[1];
        }
    }

    public void SaveSettings(string path)
    {
        var settings = settingsDict.Select(x => $"{x.Key}={x.Value}");
        File.WriteAllLines(path, settings);
    }
}
