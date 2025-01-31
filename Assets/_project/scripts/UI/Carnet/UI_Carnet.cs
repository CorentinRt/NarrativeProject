using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NarrativeProject
{
    public class UI_Carnet : MonoBehaviour
    {
        [SerializeField] Dictionary<string, Dictionary<int, string>> clues;
        [SerializeField] SO_Characters charactersDatabase;

        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] GameObject content;
        [SerializeField] List<string> characterNames;

        int currentIndexInCarnet = 0;

        public event Action OnCloseUI;
        public UnityEvent OnCloseUIUnity;

        public Dictionary<string, Dictionary<int, string>> Clues { get => clues; set => clues = value; }


        private void Start()
        {
            FindAnyObjectByType<CluesManager>().OnAddClue += UnlockClue;
            clues = new Dictionary<string, Dictionary<int, string>>();
            InitializeClues();
        }

        void InitializeClues()
        {
            foreach (SO_CharacterData character in charactersDatabase.Characters)
            {
                characterNames.Add(character.Name);
                if (!clues.ContainsKey(character.Name))
                {
                    clues.Add(character.Name, new Dictionary<int, string>());
                }
                for(int i = 0; i < character.NumberOfClues; i++)
                {
                    clues[character.Name].Add(i, "???");
                }
            }
        }

        void UnlockClue(string characterName, int key, string clue)
        {
            if (!clues.ContainsKey(characterName))
            {
                clues.Add(characterName, new Dictionary<int, string>());
            }

            if (!clues[characterName].ContainsValue(clue)) clues[characterName][key] = clue;

        }

        void ShowUI(string CharacterName)
        {
            characterImage.sprite = charactersDatabase.GetCharacter(CharacterName).Sprites[0];
            characterName.text = CharacterName;

            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            foreach(KeyValuePair<int, string> clue in clues[CharacterName])
            {
                GameObject go = new GameObject(clue.Value);
                go.transform.SetParent(content.transform);
                go.AddComponent<TextMeshProUGUI>().text = clue.Value;
                go.GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -550, 0);
            }
        }

        public void ShowNextCharacter()
        {
            currentIndexInCarnet++;
            if (currentIndexInCarnet >= characterNames.Count) currentIndexInCarnet = 0;
            ShowUI(characterNames[currentIndexInCarnet]);
        }

        public void ShowPreviousCharacter()
        {
            currentIndexInCarnet--;
            if (currentIndexInCarnet < 0) currentIndexInCarnet = characterNames.Count - 1;
            ShowUI(characterNames[currentIndexInCarnet]);
        }

        public void CloseUI()
        {
            currentIndexInCarnet = 0;
            OnCloseUI?.Invoke();
            OnCloseUIUnity?.Invoke();
        }

        [Button]
        void UnlockClue_Test()
        {
            UnlockClue("a", 0, "Elle est pas sympa");
        }

        [Button]
        void ShowUI_Test()
        {
            ShowUI("a");
        }
    }
}
