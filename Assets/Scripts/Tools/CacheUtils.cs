using UnityEngine;

namespace Tools
{
    public static class CacheUtils
    {
        public static void SetLevelCompleted(in int levelId)
        {
            PlayerPrefs.SetInt(GetLevelKey(levelId), 1);
            PlayerPrefs.Save();
        }

        public static bool GetLevelCompleted(int levelId)
        {
            return PlayerPrefs.GetInt(GetLevelKey(levelId), 0) == 1;
        }

        private static string GetLevelKey(int levelId)
        {
            return "Level" + levelId;
        }
    }
}