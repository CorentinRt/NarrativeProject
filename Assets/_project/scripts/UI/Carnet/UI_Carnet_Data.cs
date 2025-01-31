using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static PlasticGui.Help.GuiHelp;

namespace NarrativeProject
{
    public class UI_Carnet_Data : MonoBehaviour
    {
        [SerializeField] Dictionary<string, Dictionary<int, string>> clues;
        [SerializeField] SO_Characters charactersDatabase;
        [SerializeField] GameObject prefabPage, anchor;

        [VisibleInDebug] List<UI_Carnet> pages = new List<UI_Carnet>();
        UI_Carnet_Data instance;

        public event Action OnCloseUI;
        public UnityEvent OnCloseUIUnity;

        public Dictionary<string, Dictionary<int, string>> Clues { get => clues; set => clues = value; }
        public UI_Carnet_Data Instance { get => instance; set => instance = value; }
        public List<UI_Carnet> Pages { get => pages; set => pages = value; }

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            instance = this;
        }


        void Start()
        {
            FindAnyObjectByType<CluesManager>().OnAddClue += UnlockClue;
            InitializeCarnet();

        }

        void InitializeCarnet()
        {
            clues = new Dictionary<string, Dictionary<int, string>>();
            int i = charactersDatabase.Characters.Count - 1;
            foreach (SO_CharacterData character in charactersDatabase.Characters)
            {
                GameObject go = Instantiate(prefabPage, anchor.transform);
                go.name = "Page_" + character.Name;
                UI_Carnet page = go.GetComponent<UI_Carnet>();
                page.Initialize(character.Name, character.Sprites[0], this, i);

                if(!clues.ContainsKey(character.Name)) clues.Add(character.Name, new Dictionary<int, string>());
                Pages.Add(page);
                Debug.Log(character.Name + " " + i);
                i--;
            }
            Pages.Reverse();
        }

        void UnlockClue(string characterName, int key, string clue)
        {
            if (!clues.ContainsKey(characterName))
            {
                clues.Add(characterName, new Dictionary<int, string>());
            }

            if (!clues[characterName].ContainsValue(clue)) clues[characterName][key] = clue;

        }

        public void ShowCharacter(int index)
        {
            if (index <= -1)
            {
                Debug.Log("if " + index);
                pages[pages.Count - 1].ShowUI();
            }
            else if (index >= pages.Count)
            {
                Debug.Log("else if " + index);
                pages[0].ShowUI();
            }
            else
            {
                Debug.Log("else " + index);
                pages[index].ShowUI();
            }
        }
        public void CloseUI()
        {
            OnCloseUI?.Invoke();
            OnCloseUIUnity?.Invoke();
        }

        public void DeactivatePages()
        {
            foreach (UI_Carnet page in pages)
            {
                page.gameObject.SetActive(false);

            }
        }

        [Button]
        void UnlockClue_Test()
        {
            UnlockClue("a", 0, "Elle est pas sympa");
        }
    }
}
