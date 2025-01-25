using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class GameManager : MonoBehaviour
    {
        #region Fields
        private static GameManager _instance;



        #endregion

        #region Properties
        public static GameManager Instance { get => _instance; set => _instance = value; }

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
