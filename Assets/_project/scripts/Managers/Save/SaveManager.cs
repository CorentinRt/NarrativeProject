using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public static class SaveManager
    {
        public static void SaveClue(string name, int key, string clue)
        {
            string keySave = name + "_" + key;
            PlayerPrefs.SetString(keySave, clue);
            PlayerPrefs.Save();
            Debug.Log("Saved " + keySave + " " + clue);
        }

        public static void LoadClues(Dictionary<string, Dictionary<int, string>> clues, string name, int nClues)
        {
            for(int i = 0; i < nClues - 1; i++)
            {
                string key = name + "_" + i;
                string clue = PlayerPrefs.GetString(key);
                if (clue == "") break;
                if (!clues[name].ContainsKey(i))
                {
                    clues[name].Add(i, clue);
                    Debug.Log("Loaded " + key + " " + clue);
                }
            }
        }

        public static void ResetSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
