using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class PlayerInputsManager : MonoBehaviour
    {
        public enum EPlayerInputsState
        {
            AVAILABLE = 0,
            NOTAVAILABLE = 1
        }

        #region Fields
        private static PlayerInputsManager _instance;

        private EPlayerInputsState _playerInputsState;

        private bool _bInputsLocked;

        #endregion

        #region Properties
        public static PlayerInputsManager Instance { get => _instance; set => _instance = value; }
        public EPlayerInputsState PlayerInputsState { get => _playerInputsState; set => _playerInputsState = value; }


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

        public bool InputsAvailable()
        {
            if (_playerInputsState == EPlayerInputsState.NOTAVAILABLE) return false;

            if (_bInputsLocked) return false;


            return true;
        }

        #region Lock / UNLOCK
        public void LockInput()
        {
            _bInputsLocked = true;
        }
        public void UnlockInput()
        {
            _bInputsLocked = false;
        }
        #endregion
    }
}
