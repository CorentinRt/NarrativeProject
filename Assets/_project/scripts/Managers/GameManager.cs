using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class GameManager : MonoBehaviour
    {
        #region Fields
        private static GameManager _instance;

        private SoundManager _soundManager;
        private DayManager _dayManager;
        private PlayerInputsManager _playerInputsManager;

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
        private void Start()
        {
            InitManager();

            InitAllManagers();

            StartGame();
        }

        public void InitManager()
        {
            _soundManager = SoundManager.Instance;
            _dayManager = DayManager.Instance;
            _playerInputsManager = PlayerInputsManager.Instance;
        }

        public void InitAllManagers()
        {
            if (_soundManager != null)  // Sound Manager
            {
                _soundManager.InitManager();
            }
            else
            {
                Debug.LogWarning("SoundManager Manager is missing in the scene ! Some behaviors may not work correctly !");
            }

            if (_dayManager != null)    // Day Manager
            {
                _dayManager.InitManager();
            }
            else
            {
                Debug.LogWarning("DayManager Manager is missing in the scene ! Some behaviors may not work correctly !");
            }

            if (_playerInputsManager != null)   // PlayerInputs Manager
            {
                _playerInputsManager.InitManager();
            }
            else
            {
                Debug.LogWarning("PlayerInputs Manager is missing in the scene ! Some behaviors may not work correctly !");
            }
        }

        private void StartGame()
        {
            if (_dayManager == null)    return;

            _dayManager.BeginDay();
        }
    }

}
