using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class SoundManager : MonoBehaviour
    {
        #region Fields
        private static SoundManager _instance;



        #endregion

        #region Properties
        public static SoundManager Instance { get => _instance; set => _instance = value; }

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
    }
}
