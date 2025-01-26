using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class CameraFocusableObject : MonoBehaviour, ICameraFocusable, IInputsInteractible
    {
        #region Fields
        private bool _canFocus = true;

        private bool _isFocused = false;

        [SerializeField] private LayerMask _interactibleLayerMask;

        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Collider2D detectedCollider = Physics2D.OverlapCircle(worldMousePos, 0.5f, _interactibleLayerMask);

                if (detectedCollider == null)   return;

                if (detectedCollider.gameObject == gameObject)
                {
                    Focus();
                }
            }
        }

        public bool CanFocus()
        {
            return _canFocus;
        }


        public void Focus()
        {
            if (!CanFocus())    return;

            NotifyInteraction(EPlayerInputsState.INTERACTION);
            if (!CanInteract()) return;

            if (CameraManager.Instance != null)
            {
                if (_isFocused)
                {
                    CameraManager.Instance.UnfocusCamera();
                    _isFocused = false;
                }
                else
                {
                    CameraManager.Instance.FocusCameraOn(transform);
                    _isFocused = true;
                }
            }

            NotifyEndInteraction();
        }

        #region Inputs Interactible Interface
        public bool CanInteract()
        {
            return _isAbleToInteract;
        }
        public event Action<IInputsInteractible, EPlayerInputsState> OnInteractionBegin;
        public event Action<IInputsInteractible, EPlayerInputsState> OnInteractionEnd;
        private bool _isAbleToInteract = true;
        public void NotifyEndInteraction()
        {
            OnInteractionEnd(this, EPlayerInputsState.INTERACTION);
        }

        public void NotifyInteraction(EPlayerInputsState inputState)
        {
            OnInteractionBegin?.Invoke(this, inputState);
        }

        public void SetIsAbleToInteract(bool value)
        {
            _isAbleToInteract = value;
        }

        #endregion
    }
}
