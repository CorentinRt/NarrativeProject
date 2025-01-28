using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class EndGameTrigger : MonoBehaviour
    {
        [SerializeField] private DropZone _associatedDropZone;
        public UnityEvent OnCallEndDayUnity;


        private void Awake()
        {
            if (_associatedDropZone != null)
            {
                _associatedDropZone.OnReceiveDrop += OnReceiveDropEvent;
            }    
        }
        private void OnDestroy()
        {
            if (_associatedDropZone != null)
            {
                _associatedDropZone.OnReceiveDrop -= OnReceiveDropEvent;
            }
        }



        public void OnReceiveDropEvent(GameObject gameObject)
        {
            Debug.Log(gameObject);

            CallEndDay();
        }
        public void CallEndDay()
        {
            if (DayManager.Instance != null)
            {
                if (DayManager.Instance.CurrentDayPhase == DayManager.EDayPhase.POST_DAY)
                {
                    DayManager.Instance.EndDay();
                    OnCallEndDayUnity?.Invoke();
                }
            }

        }

    }
}
