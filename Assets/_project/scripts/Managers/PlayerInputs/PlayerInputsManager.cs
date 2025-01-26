using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NarrativeProject
{
    public enum EPlayerInputsState
    {
        AVAILABLE = 0,
        DRAGGING = 1,
        UIINTERACTION = 2,
        LOCKED = 3
    }
    public class PlayerInputsManager : MonoBehaviour
    {

        #region Fields
        private static PlayerInputsManager _instance;

        private EPlayerInputsState _currentPlayerInputsState = EPlayerInputsState.AVAILABLE;

        private List<IInputsInteractible> _inputsInteractibles;

        private IInputsInteractible _currentInputInteractible;

        #endregion

        #region Properties
        public static PlayerInputsManager Instance { get => _instance; set => _instance = value; }
        public EPlayerInputsState PlayerInputsState { get => _currentPlayerInputsState; }


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
        private void RegisterAllInputsInteractibleObjects()
        {
            _inputsInteractibles = new List<IInputsInteractible>();
            var tempInputsInteractibles = FindObjectsOfType<MonoBehaviour>().OfType<IInputsInteractible>();

            foreach(var input in tempInputsInteractibles)
            {
                _inputsInteractibles.Add(input);

                input.OnInteractionBegin += ReceiveNewInputStateDemand;
                input.OnInteractionEnd += ReceiveInputStateEnd;
            }
        }
        private void OnDestroy()
        {
            if (_inputsInteractibles != null)
            {
                foreach (var input in _inputsInteractibles)
                {
                    input.OnInteractionBegin -= ReceiveNewInputStateDemand;
                    input.OnInteractionEnd -= ReceiveInputStateEnd;
                }
            }
        }


        public void SetInputsState(EPlayerInputsState state)
        {
            _currentPlayerInputsState = state;
        }
        public bool InputsAvailable()
        {
            if (_currentPlayerInputsState == EPlayerInputsState.AVAILABLE) return true;

            return false;
        }

        private void ReceiveNewInputStateDemand(IInputsInteractible inputsInteractible, EPlayerInputsState playerInputsState)
        {
            if (_currentPlayerInputsState != EPlayerInputsState.AVAILABLE) return;

            _currentInputInteractible = inputsInteractible;

            _currentInputInteractible.SetIsAbleToInteract(true);

            _currentPlayerInputsState = playerInputsState;

            Debug.Log("ReceiveNewInputStateDemand accepted !!!");
        }

        private void ReceiveInputStateEnd(IInputsInteractible inputsInteractible, EPlayerInputsState PlayerInputsState)
        {
            if (_currentInputInteractible == null) return;

            if (_currentInputInteractible != inputsInteractible)    return;

            _currentInputInteractible.SetIsAbleToInteract(false);

            _currentInputInteractible = null;

            _currentPlayerInputsState = EPlayerInputsState.AVAILABLE;
        }
    }
}
