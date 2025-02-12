using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NarrativeProject
{
    public class UI_Carnet : MonoBehaviour
    {
        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] GameObject content;
        [SerializeField] List<string> characterNames;
        [SerializeField] Button nextButton, previousButton, closeButton;

        [SerializeField] private TMP_FontAsset _cluesFont;

        [VisibleInDebug] int indexInCarnet;
        UI_Carnet_Data data;
        List<GameObject> clues = new List<GameObject>();

        public UI_Carnet_Data Data { get => data; set => data = value; }

        public void Initialize(string name, Sprite image, UI_Carnet_Data d, int index)
        {
            data = d;
            characterImage.sprite = image;
            characterName.text = name;
            indexInCarnet = index;
            gameObject.SetActive(false);
        }


        public void ShowUI()
        {
            gameObject.SetActive(true);
            foreach (GameObject go in clues)
            {
                Destroy(go);
            }
            Debug.Log("for each in" + characterName.text);
            foreach (string clue in data.Clues[characterName.text].Values )
            {
                Debug.Log(clue);
                GameObject go = new GameObject(clue);
                go.transform.SetParent(content.transform);
                go.AddComponent<TextMeshProUGUI>().text = clue;
                TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();
                text.fontSize = 40;
                text.font = _cluesFont;
                text.enableAutoSizing = true;
                text.color = Color.black;
                clues.Add(go);
            }
            previousButton.interactable = true;
            nextButton.interactable = true;
            closeButton.interactable = true;
        }

        public void ShowNextCharacter()
        {
            previousButton.interactable = false;
            nextButton.interactable = false;
            closeButton.interactable = false;

            if (indexInCarnet + 1 >= data.Pages.Count)
            {
                Debug.Log("End of the list");
                data.ReturnFirstPage(0.25f);
                return;
            }
            data.ShowCharacter(indexInCarnet + 1);
            StartCoroutine(NextPage(1f));

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayNextPage();
            }
        }

        public void ShowPreviousCharacter()
        {
            previousButton.interactable = false;
            nextButton.interactable = false;
            closeButton.interactable = false;

            if (indexInCarnet - 1 < 0)
            {
                Debug.Log("End of the list");
                data.ReturnLastPage(0.5f);
                return;
            }
            data.ShowCharacter(indexInCarnet - 1);
            StartCoroutine(PreviousPage(1f));

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayNextPage();
            }
        }

        IEnumerator NextPage(float time)
        {
            float elapsedTime = 0;
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            while (elapsedTime < time)
            {
                GetComponent<RectTransform>().rotation = Quaternion.Euler(0, Mathf.Lerp(0, -90, elapsedTime), 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            gameObject.SetActive(false);
            yield return null;
        }

        IEnumerator PreviousPage(float time)
        {
            Debug.Log("PreivousPage" + indexInCarnet);
            float elapsedTime = 0;
            int index = indexInCarnet - 1;
            RectTransform page;
            if (index - 1 < 0) page = data.Pages[0].GetComponent<RectTransform>();
            else page = data.Pages[indexInCarnet - 1].GetComponent<RectTransform>();
            page.rotation = Quaternion.Euler(0, -90, 0);
            while (elapsedTime < time)
            {
                Debug.Log("anim");
                page.rotation = Quaternion.Euler(0, Mathf.Lerp(-90, 0, elapsedTime), 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
            yield return null;
        }
        public void Close()
        {
            previousButton.interactable = false;
            nextButton.interactable = false;
            closeButton.interactable = false;
            data.CloseUI();
        }

        [Button]
        void ShowUI_Test()
        {
            //ShowUI("a");
        }
    }
}
