using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class DayManager : MonoBehaviour
    {
        #region Fields
        private static DayManager _instance;



        #endregion

        #region Properties
        public static DayManager Instance { get => _instance; set => _instance = value; }

        #endregion

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

    }
}
