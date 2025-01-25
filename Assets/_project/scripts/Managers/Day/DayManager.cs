using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class DayManager : MonoBehaviour
    {
        public enum EDayPhase
        {
            PRE_DAY = 0,
            IN_DAY = 1,
            POST_DAY = 2
        }

        #region Fields
        private static DayManager _instance;

        private EDayPhase _currentDayPhase;

        private int _currentDayIndex;

        [SerializeField] private List<DayDataSO> _daysData;

        private int _currentInteractionCountRemaining;

        private bool _canInteract;

        #endregion

        #region Properties
        public static DayManager Instance { get => _instance; set => _instance = value; }
        public int CurrentDayIndex { get => _currentDayIndex; set => _currentDayIndex = value; }
        public EDayPhase CurrentDayPhase { get => _currentDayPhase; set => _currentDayPhase = value; }

        #endregion

        #region Delegates
        public UnityEvent OnNextDayUnity;
        public UnityEvent OnBeginDayUnity;
        public UnityEvent OnEndDayUnity;

        public UnityEvent OnUpdateCurrentInteractionCountRemainingUnity;

        #endregion

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        public void InitManager()
        {

        }
        public void InitDay()
        {
            if (CurrentDayIndex >= _daysData.Count) return;

            DayDataSO currentDayData = _daysData[CurrentDayIndex];

            if (currentDayData == null)
            {
                Debug.LogWarning("day data missing in the List of the DayManager. Please fix the null reference in the List");
                return;
            }

            _currentInteractionCountRemaining = currentDayData.MaxInteractionCount;
        }
        public void NextDay()
        {
            ++_currentDayIndex;

            OnNextDayUnity?.Invoke();
        }
        public void BeginDay()
        {


            OnBeginDayUnity?.Invoke();
        }
        public void EndDay()
        {


            OnEndDayUnity?.Invoke();
        }

        public void DecrementCurrentInteractionCountRemaining()
        {
            --_currentInteractionCountRemaining;

            if (_currentInteractionCountRemaining <= 0)
            {
                EndDay();
            }

            OnUpdateCurrentInteractionCountRemainingUnity?.Invoke();
        }

    }
}
