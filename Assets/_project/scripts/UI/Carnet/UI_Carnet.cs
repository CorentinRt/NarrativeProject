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

        [VisibleInDebug] int indexInCarnet;
        UI_Carnet_Data data;

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
            foreach (GameObject go in content.transform)
            {
                Destroy(go);
            }
            foreach (KeyValuePair<int, string> clue in data.Clues[characterName.text] )
            {
                GameObject go = new GameObject(clue.Value);
                go.transform.SetParent(content.transform);
                go.AddComponent<TextMeshProUGUI>().text = clue.Value;
                go.GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -550, 0);
            }
        }

        public void ShowNextCharacter()
        {
            if(indexInCarnet + 1 >= data.Pages.Count)
            {
                Debug.Log("End of the list");
                data.ReturnFirstPage(0.25f);
                return;
            }
            data.ShowCharacter(indexInCarnet + 1);
            StartCoroutine(NextPage(1f));
        }

        public void ShowPreviousCharacter()
        {
            if(indexInCarnet - 1 < 0)
            {
                Debug.Log("End of the list");
                data.ReturnLastPage(0.5f);
                return;
            }
            data.ShowCharacter(indexInCarnet - 1);
            StartCoroutine(PreviousPage(1f));
        }

        IEnumerator NextPage(float time)
        {
            float elapsedTime = 0;
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
            Debug.Log("PreivousPage");
            float elapsedTime = 0;
            int index = indexInCarnet - 1;
            RectTransform page;
            if (index - 1 < 0) page = data.Pages[data.Pages.Count - 1].GetComponent<RectTransform>();
            else page = data.Pages[indexInCarnet - 1].GetComponent<RectTransform>();
            while (elapsedTime < time)
            {
                page.rotation = Quaternion.Euler(0, Mathf.Lerp(-90, 0, elapsedTime), 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
            yield return null;
        }
        public void Close()
        {
            data.CloseUI();
        }

        [Button]
        void ShowUI_Test()
        {
            //ShowUI("a");
        }
    }
}
