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
            POST_DAY = 2,
        }

        #region Fields
        private static DayManager _instance;

        [ShowNonSerializedField] private EDayPhase _currentDayPhase;

        [ShowNonSerializedField] private int _currentDayIndex;

        [SerializeField] private List<DayDataSO> _daysData;

        [ShowNonSerializedField] private int _currentInteractionCount;

        private bool _canInteract;

        #endregion

        #region Properties
        public static DayManager Instance { get => _instance; set => _instance = value; }
        public int CurrentDayIndex { get => _currentDayIndex; set => _currentDayIndex = value; }
        public EDayPhase CurrentDayPhase { get => _currentDayPhase; }
        public int CurrentInteractionCount { get => _currentInteractionCount; set => _currentInteractionCount = value; }

        #endregion

        #region Delegates
        public UnityEvent OnNextDayUnity;

        public event Action<int> OnPreDay;

        public event Action<int> OnBeginDay;
        public UnityEvent OnBeginDayUnity;
        
        public event Action<int> OnEndDay;
        public UnityEvent OnEndDayUnity;

        public event Action OnEndGame;
        public UnityEvent OnEndGameUnity;

        public UnityEvent OnUpdateCurrentInteractionCountUnity;

        public event Action<EDayPhase> OnUpdateDayPhase;
        public event Action<int, int> OnUpdateCurrentInteractionCount;

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

            CurrentInteractionCount = 0;
        }

        #region Day Global
        public void NextDay()
        {
            ++_currentDayIndex;
            if(_currentDayIndex >= _daysData.Count)
            {
                _currentDayIndex = 0;
                OnEndGame?.Invoke();
                OnEndGameUnity?.Invoke();
                return;
            }
            ChangeDayPhase(EDayPhase.PRE_DAY);

            OnNextDayUnity?.Invoke();
        }
        public void BeginDay()
        {
            CurrentInteractionCount = 0;

            ChangeDayPhase(EDayPhase.IN_DAY);

            OnBeginDay?.Invoke(CurrentDayIndex);
            OnBeginDayUnity?.Invoke();
        }
        public void EndDay()
        {
            ChangeDayPhase(EDayPhase.POST_DAY);

            CurrentInteractionCount = 0;

            OnEndDay?.Invoke(CurrentDayIndex);
            OnEndDayUnity?.Invoke();
        }
        #endregion

        #region Day Phases / interactions
        [Button]
        public void IncrementCurrentInteractionCount()
        {
            ++CurrentInteractionCount;

            CurrentInteractionCount = Mathf.Clamp(CurrentInteractionCount, 0, _daysData[_currentDayIndex].MaxInteractionCount);

            OnUpdateCurrentInteractionCountUnity?.Invoke();
            OnUpdateCurrentInteractionCount?.Invoke(_currentDayIndex, CurrentInteractionCount);

            if (CurrentInteractionCount >= _daysData[_currentDayIndex].MaxInteractionCount)
            {
                EndDay();
            }
        }

        [Button]
        public void NextDayPhase()
        {
            switch (_currentDayPhase)
            {
                case EDayPhase.PRE_DAY:
                    BeginDay();
                    break;
                case EDayPhase.IN_DAY:
                    EndDay();
                    break;
                case EDayPhase.POST_DAY:
                    NextDay();
                    break;
                default:
                    break;
            }
        }
        private void ChangeDayPhase(EDayPhase dayPhase)
        {
            _currentDayPhase = dayPhase;

            OnUpdateDayPhase?.Invoke(_currentDayPhase);

            if (dayPhase == EDayPhase.PRE_DAY)
            {
                OnPreDay?.Invoke(CurrentDayIndex);
            }
        }
        #endregion

    }
}
