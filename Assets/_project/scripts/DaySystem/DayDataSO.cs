using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    [CreateAssetMenu(fileName = "DayDataSO", menuName = "ScriptableObjects/DaySystem/DayDataSO", order = 1)]
    public class DayDataSO : ScriptableObject
    {
        #region Fields
        [SerializeField] private int _maxInteractionCount;


        #endregion

        #region Properties
        public int MaxInteractionCount { get => _maxInteractionCount; set => _maxInteractionCount = value; }


        #endregion

    }
}
