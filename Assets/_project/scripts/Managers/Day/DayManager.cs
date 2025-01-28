using NaughtyAttributes;
using System;
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

        [ShowNonSerializedField] private EDayPhase _currentDayPhase;

        [ShowNonSerializedField] private int _currentDayIndex;

        [SerializeField] private List<DayDataSO> _daysData;

        [ShowNonSerializedField] private int _currentInteractionCountRemaining;

        private bool _canInteract;

        #endregion

        #region Properties
        public static DayManager Instance { get => _instance; set => _instance = value; }
        public int CurrentDayIndex { get => _currentDayIndex; set => _currentDayIndex = value; }
        public EDayPhase CurrentDayPhase { get => _currentDayPhase; }
        public int CurrentInteractionCountRemaining { get => _currentInteractionCountRemaining; set => _currentInteractionCountRemaining = value; }

        #endregion

        #region Delegates
        public UnityEvent OnNextDayUnity;
        public UnityEvent OnBeginDayUnity;
        public UnityEvent OnEndDayUnity;

        public UnityEvent OnUpdateCurrentInteractionCountRemainingUnity;

        public event Action<EDayPhase> OnUpdateDayPhase;
        public event Action<int, int> OnUpdateCurrentInteractionCountRemaining;

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
        [Button]
        public void InitDay()
        {
            ChangeDayPhase(EDayPhase.PRE_DAY);

            if (CurrentDayIndex >= _daysData.Count) return;

            DayDataSO currentDayData = _daysData[CurrentDayIndex];

            if (currentDayData == null)
            {
                Debug.LogWarning("day data missing in the List of the DayManager. Please fix the null reference in the List");
                return;
            }

            CurrentInteractionCountRemaining = currentDayData.MaxInteractionCount;
        }

        #region Day Global
        public void NextDay()
        {
            ++_currentDayIndex;

            OnNextDayUnity?.Invoke();
        }
        public void BeginDay()
        {
            ChangeDayPhase(EDayPhase.IN_DAY);

            OnBeginDayUnity?.Invoke();
        }
        public void EndDay()
        {
            ChangeDayPhase(EDayPhase.POST_DAY);

            CurrentInteractionCountRemaining = 0;

            OnEndDayUnity?.Invoke();
        }
        #endregion

        #region Day Phases / interactions
        [Button]
        public void DecrementCurrentInteractionCountRemaining()
        {
            --CurrentInteractionCountRemaining;

            if (CurrentInteractionCountRemaining <= 0)
            {
                EndDay();
            }

            OnUpdateCurrentInteractionCountRemainingUnity?.Invoke();
            OnUpdateCurrentInteractionCountRemaining?.Invoke(_currentDayIndex, CurrentInteractionCountRemaining);
        }

        [Button]
        public void NextDayPhase()
        {
            switch (_currentDayPhase)
            {
                case EDayPhase.PRE_DAY:
                    ChangeDayPhase(EDayPhase.IN_DAY);
                    break;
                case EDayPhase.IN_DAY:
                    ChangeDayPhase(EDayPhase.POST_DAY);
                    break;
                case EDayPhase.POST_DAY:
                    break;
                default:
                    break;
            }
        }
        private void ChangeDayPhase(EDayPhase dayPhase)
        {
            _currentDayPhase = dayPhase;

            OnUpdateDayPhase?.Invoke(_currentDayPhase);
        }
        #endregion
    }
}
