using System.Collections.Generic;
namespace Project5LMS.Interfaces
{
    public interface ISettingsService
    {
        string GetSetting(string key, string defaultValue = "");
        bool SaveSetting(string key, string value, string category = "General");
        Dictionary<string, string> GetSettingsByCategory(string category);
        bool EnsureSettingsTableExists();
    }
}