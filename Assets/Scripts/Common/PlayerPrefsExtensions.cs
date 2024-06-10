using UnityEngine;

namespace Common
{
    public class PlayerPrefsExtensions
    {
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
        
        public static bool GetBool(string key, bool defaultValue = false)
        {
            if (!PlayerPrefs.HasKey(key)) return defaultValue;
            return PlayerPrefs.GetInt(key) == 1;
        }
        
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        
        public static float GetFloat(string key, float defaultValue = 0f)
        {
            if (!PlayerPrefs.HasKey(key)) return defaultValue;
            return PlayerPrefs.GetFloat(key);
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}
