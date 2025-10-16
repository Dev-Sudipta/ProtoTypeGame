using System.IO;
using UnityEngine;

namespace MatchCard
{
    public class GameDataSaveManager 
    {
        private static string savePath => Path.Combine(Application.persistentDataPath, "gamesave.json");

        /// <summary>Save the game data to JSON file.</summary>
        public static void Save(GameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
        }

        /// <summary>Load the game data from JSON file. Returns default if no file exists.</summary>
        public static GameData Load()
        {
            if (!File.Exists(savePath))
                return new GameData { score = 0 };

            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameData>(json);
        }

        /// <summary>Delete saved data (optional for new game).</summary>
        public static void Reset()
        {
            if (File.Exists(savePath))
                File.Delete(savePath);
        }
    }
}
