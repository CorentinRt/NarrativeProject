using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NarrativeProject
{
    public class CameraFocusableObject : MonoBehaviour, ICameraFocusable, IInputsInteractible
    {
        #region Fields
        private bool _canFocus = true;

        private bool _isFocused = false;

        [SerializeField] private Vector2 _focusCameraOffset = Vector2.zero;

        [SerializeField] private LayerMask _interactibleLayerMask;

        #endregion

        #region Delegates
        public UnityEvent OnReceiveCameraFocusUnity;
        public UnityEvent OnReceiveCameraUnfocusUnity;


        #endregion


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main == null) return;

                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

                /*
                if (hit.collider != null)
                {
                    Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                }
                */

                Collider2D detectedCollider = hit.collider;

                if (detectedCollider == null)   return;

                if (detectedCollider.gameObject == gameObject)
                {
                    ToggleFocus();
                }
            }
        }

        public bool CanFocus()
        {
            return _canFocus;
        }

        public void Focus()
        {
            if (_isFocused) return;

            CameraManager.Instance.FocusCameraOn(this, _focusCameraOffset);
            _isFocused = true;
        }
        public void Unfocus()
        {
            if (!_isFocused) return;

            CameraManager.Instance.UnfocusCamera();
            _isFocused = false;
            _isFocused = false;
        }
        public void ToggleFocus()
        {
            if (!CanFocus())    return;

            NotifyInteraction(EPlayerInputsState.INTERACTION);
            if (!CanInteract()) return;

            if (CameraManager.Instance != null)
            {
                if (_isFocused)
                {
                    Unfocus();
                }
                else
                {
                    Focus();
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
            OnInteractionEnd?.Invoke(this, EPlayerInputsState.INTERACTION);
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
