using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NarrativeProject
{
    public class EndGameManager : MonoBehaviour
    {
        string title;

        string fact_1 = "";
        string fact_2 = "";

        Sprite img;

        [SerializeField] TextMeshProUGUI titleText, fact1, fact2;
        [SerializeField] Image imgSprite;
        public UnityEvent OnOpenEndGameUnity;


        public void NewFact(string newFact)
        {
            if(fact_1 != "")
            {
                fact_2 = newFact;
            }
            else
            {
                fact_1 = newFact;
            }
        }
        public void DeadSpriteMan(Sprite sprite)
        {
            img = sprite;
        }

        public void OpenEndGame()
        {
            OnOpenEndGameUnity?.Invoke();
            titleText.text = title;
            fact1.text = fact_1;
            fact2.text = fact_2;
        }
    }
}
