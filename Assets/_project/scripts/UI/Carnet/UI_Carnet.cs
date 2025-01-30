using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NarrativeProject
{
    public class UI_Carnet : MonoBehaviour
    {
        [SerializeField] Dictionary<string, Dictionary<string, string>> clues;
        [SerializeField] SO_Characters charactersDatabase;

        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] GameObject content;

        public Dictionary<string, Dictionary<string, string>> Clues { get => clues; set => clues = value; }


        private void Start()
        {
            FindAnyObjectByType<CluesManager>().OnAddClue += UnlockClue;
            clues = new Dictionary<string, Dictionary<string, string>>();
        }

        void UnlockClue(string characterName, string key, string clue)
        {
            if (!clues.ContainsKey(characterName))
            {
                clues.Add(characterName, new Dictionary<string, string>());
            }

            if (!clues[characterName].ContainsKey(key)) clues[characterName].Add(key, clue);

        }

        void ShowUI(string CharacterName)
        {
            characterImage.sprite = charactersDatabase.GetCharacter(CharacterName).Sprites[0];
            characterName.text = CharacterName;

            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            foreach(KeyValuePair<string, string> clue in clues[CharacterName])
            {
                GameObject go = new GameObject(clue.Key);
                go.transform.SetParent(content.transform);
                go.AddComponent<TextMeshProUGUI>().text = clue.Value;
                go.GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -550, 0);
            }
        }

        [Button]
        void UnlockClue_Test()
        {
            UnlockClue("a", "mechant", "Elle est pas sympa");
        }

        [Button]
        void ShowUI_Test()
        {
            ShowUI("a");
        }
    }
}
