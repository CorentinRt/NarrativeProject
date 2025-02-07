using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class UI_Carnet_Data : MonoBehaviour
    {
        [System.Serializable]
        struct PrefixToCharacterName
        {
            public string prefix;
            public string name;
        }

        [SerializeField] Dictionary<string, Dictionary<int, string>> clues;
        [SerializeField] SO_Characters charactersDatabase;
        [SerializeField] GameObject prefabPage, anchor;

        [VisibleInDebug] List<UI_Carnet> pages = new List<UI_Carnet>();
        UI_Carnet_Data instance;

        [SerializeField] private List<PrefixToCharacterName> _prefixToCharacterNames;

        public event Action OnCloseUI;
        public event Action<string, int, string> OnNewClue;

        public UnityEvent OnCloseUIUnity;
        public UnityEvent OnNewClueUnity;

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
            if (CluesManager.Instance != null)
            {
                CluesManager.Instance.OnAddClue += UnlockClue;
            }
            InitializeCarnet();

        }
        private void OnDestroy()
        {
            if (CluesManager.Instance != null)
            {
                CluesManager.Instance.OnAddClue -= UnlockClue;
            }
        }

        void InitializeCarnet()
        {
            //SaveManager.ResetSave();
            var _ = charactersDatabase;
            clues = new Dictionary<string, Dictionary<int, string>>();
            int i = charactersDatabase.Characters.Count - 1;
            foreach (SO_CharacterData character in charactersDatabase.Characters)
            {
                GameObject go = Instantiate(prefabPage, anchor.transform);
                go.name = "Page_" + character.Name;
                UI_Carnet page = go.GetComponent<UI_Carnet>();
                page.Initialize(character.Name, character.Sprites[1], this, i);

                if(!clues.ContainsKey(character.Name)) clues.Add(character.Name, new Dictionary<int, string>());
                Pages.Add(page);
                Debug.Log(character.Name + " " + i);
                i--;
            }
            foreach ( string name in clues.Keys)
            {
                SaveManager.LoadClues(clues, name, 6);
            }
            Pages.Reverse();
        }

        void UnlockClue(string characterPrefix, int key, string clue)
        {
            string characterName = "";

            characterName = _prefixToCharacterNames.FirstOrDefault(entry => entry.prefix == characterPrefix).name;


            if (!clues.ContainsKey(characterName))
            {
                Debug.Log("CREATE DICT " + characterPrefix);
                clues.Add(characterName, new Dictionary<int, string>());
                OnNewClue?.Invoke(characterName, key, clue);
                OnNewClueUnity?.Invoke();
            }
            Debug.Log("Unlocking clue " + characterPrefix + " " + key + " " + clue);
            Debug.Log("value: " + clues[characterName].ContainsValue(clue));
            if (!clues[characterName].ContainsValue(clue))
            {
                clues[characterName][key] = clue;
                OnNewClue?.Invoke(characterName, key, clue);
                OnNewClueUnity?.Invoke();
                Debug.Log("ADDING TO DICT clue " + characterPrefix + " " + key + " " + clue);
            }



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

        public void ReturnFirstPage(float time) => StartCoroutine(ReturnFirstPageCoroutine(time));
        public void ReturnLastPage(float time) => StartCoroutine(ReturnLastPageCoroutine(time));

        IEnumerator ReturnFirstPageCoroutine(float time)
        {
            for (int i = pages.Count - 1; i > 0; i--)
            {
                ShowCharacter(i - 1);
                float elapsedTime = 0f;
                RectTransform page;
                if (i - 1 < 0) page = pages[pages.Count - 1].GetComponent<RectTransform>();
                else page = pages[i - 1].GetComponent<RectTransform>();
                while (elapsedTime < time)
                {
                    page.rotation = Quaternion.Euler(0, Mathf.Lerp(-90, 0, Mathf.InverseLerp(0, time, elapsedTime)), 0);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                pages[i].gameObject.SetActive(false);
                yield return null;
            }
        }
        IEnumerator ReturnLastPageCoroutine(float time)
        {
            for (int i = 0; i < pages.Count - 1; i++)
            {
                ShowCharacter(i + 1);
                float elapsedTime = 0f;
                RectTransform page;
                if (i + 1 < 0) page = pages[pages.Count + 1].GetComponent<RectTransform>();
                else page = pages[i].GetComponent<RectTransform>();
                while (elapsedTime < time)
                {
                    page.rotation = Quaternion.Euler(0, Mathf.Lerp(0, -90, Mathf.InverseLerp(0, time, elapsedTime)), 0);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                pages[i].gameObject.SetActive(false);
                yield return null;
            }
        }

        [Button]
        void UnlockClue_Test()
        {
            UnlockClue("a", 0, "Elle est pas sympa");
        }
    }
}
