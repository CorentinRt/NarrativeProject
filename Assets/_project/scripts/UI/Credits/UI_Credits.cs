using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class UI_Credits : MonoBehaviour
    {
        [SerializeField] private GameObject _creditWrongEnding;
        [SerializeField] private GameObject _creditRightEnding;


        public UnityEvent OnRequestDisplayWrongEndingUnity;
        public UnityEvent OnRequestDisplayRightEndingUnity;

        private bool _bDisplayRightEnding = false;


        private void Start()
        {
            _creditWrongEnding.SetActive(false);

            _creditRightEnding.SetActive(false);


        }

        public void DisplayCredit()
        {
            if (_bDisplayRightEnding)
            {
                RequestDisplayRightEnding();
            }
            else
            {
                RequestDisplayWrongEnding();
            }
        }

        public void RequestNotifyDisplayWrongEnding()
        {
            _bDisplayRightEnding = false;
        }

        public void RequestNotifyDisplayRightEnding()
        {
            _bDisplayRightEnding = true;
        }

        [Button]
        private void RequestDisplayWrongEnding()
        {
            OnRequestDisplayWrongEndingUnity?.Invoke();
        }
        [Button]
        private void RequestDisplayRightEnding()
        {
            OnRequestDisplayRightEndingUnity?.Invoke();
        }
    }
}
