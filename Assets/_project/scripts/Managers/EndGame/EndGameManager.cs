using NaughtyAttributes;
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
        [SerializeField] private Canvas _canvasEndGame;

        string title;

        string fact_1 = "";
        string fact_2 = "";

        Sprite img;

        [SerializeField] TextMeshProUGUI titleText, fact1, fact2;
        [SerializeField] Image imgSprite;
        public UnityEvent OnOpenEndGameUnity;

        private void Start()
        {
            if (_canvasEndGame)
                _canvasEndGame.gameObject.SetActive(false);
        }

        public void SetEndingTitle(int endingIndex)
        {
            switch (endingIndex)
            {
                case 0: // Axeman
                    title = "BREAKING NEWS: FARM WORKER NAMED ALBERT AXEMAN FOUND DEAD IN GARBAGE CAN OF ILLEGAL BAR!";
                    break;
                case 1: // Log Lady
                    title = "BREAKING NEWS: DIVA NAMED LINDA LOG-LADY FOUND DEAD IN GARBAGE CAN OF ILLEGAL BAR!";
                    break;
                case 2: // Lenny
                    title = "BREAKING NEWS: THE CRAZY MAN WITH A FORK NAMED LENNY THE DEVIL FOUND DEAD IN THE TRASH OF AN ILLEGAL BAR!";
                    break;
                case 3: // Phil
                    title = "BREAKING NEWS: DANDY NAMED BEAUTIFUL PHILIBERT FOUND DEAD IN THE TRASH OF AN ILLEGAL BAR!";
                    break;
                case 4: // Crackhead
                    title = "BREAKING NEWS: POLICE OFFICER NAMED SERGEANT CRACKHEAD FOUND DEAD IN GARBAGE CAN OF ILLEGAL BAR!";
                    break;
                case 5: // Muddy
                    title = "BREAKING NEWS: MURDER OF A HOMELESS MAN, A NEW KILLER IN CITY";
                    break;
                case 6: // Kina
                    title = "BREAKING NEWS: A SECOND SUICIDE HAS TAKEN PLACE IN DOWNTOWN, AN EPIDEMIC OF DEPRESSION OR A SIMPLE COINCIDENCE?";
                    break;
                case 7: // The player
                    title = "BREAKING NEWS: A SECOND SUICIDE HAS TAKEN PLACE IN DOWNTOWN, AN EPIDEMIC OF DEPRESSION OR A SIMPLE COINCIDENCE?";
                    break;
            }
        }

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

        [Button]
        public void OpenEndGame()
        {
            if (_canvasEndGame)
                _canvasEndGame.gameObject.SetActive(true);

            titleText.text = title;
            fact1.text = fact_1;
            fact2.text = fact_2;
            OnOpenEndGameUnity?.Invoke();
        }
    }
}
