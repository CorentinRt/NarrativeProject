using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class CameraManager : MonoBehaviour
    {
        #region Fields
        private static CameraManager _instance;

        private Camera _cameraMain;

        [SerializeField] private Transform _unfocusedCameraTransform;
        private Vector3 _unfocusedCameraPosition;

        [SerializeField] private float _focusedCameraSize;
        [SerializeField] private float _focusedDuration;

        [SerializeField] private float _unfocusedCameraSize;
        [SerializeField] private float _unfocusedDuration;

        private Tween _cameraSizeTween;
        private Tween _cameraMoveXTween;
        private Tween _cameraMoveYTween;

        private Tween _cameraPosShakeTween;
        private Tween _cameraRotShakeTween;

        private CameraFocusableObject _currentFocusedObject;

        #endregion

        #region Properties
        public static CameraManager Instance { get => _instance; set => _instance = value; }


        #endregion

        #region Delegates
        [Space(10)]
        public UnityEvent OnFocusCameraUnity;
        public UnityEvent OnUnfocusCameraUnity;
        public UnityEvent OnShakeCameraUnity;

        #endregion

        #region Editor Test
        [Space(20)]
        [HorizontalLine(2, EColor.Yellow)]
        [Header("Test in Editor")]

        [SerializeField] private Transform _testTransform;
        #endregion


        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;

            _cameraMain = Camera.main;

            _unfocusedCameraPosition = _unfocusedCameraTransform.position;
        }

        [Button]
        private void TestFocusCameraOn()
        {
            FocusCameraOn(_testTransform);
        }

        public void FocusCameraOn(CameraFocusableObject cameraFocusable, Vector2? focusOffset = null)
        {
            if (cameraFocusable == null) return;

            if (_currentFocusedObject != null)
            {
                _currentFocusedObject.OnReceiveCameraUnfocusUnity?.Invoke();
            }

            _currentFocusedObject = cameraFocusable;
            _currentFocusedObject.OnReceiveCameraFocusUnity?.Invoke();

            FocusCameraOn(_currentFocusedObject.transform, focusOffset);
        }
        public void FocusCameraOn(Transform transform, Vector2? focusOffset = null)
        {
            if (_cameraMain == null) return;

            Vector2 tempOffset = Vector2.zero;
            if (focusOffset != null)
            {
                tempOffset = focusOffset.Value;
            }
            Debug.Log("Vector : " + tempOffset);

            _cameraSizeTween.Kill();
            _cameraSizeTween = _cameraMain.DOOrthoSize(_focusedCameraSize, _focusedDuration);

            _cameraMoveXTween.Kill();
            _cameraMoveYTween.Kill();
            _cameraMoveXTween = _cameraMain.transform.DOMoveX(transform.position.x + tempOffset.x, _focusedDuration);
            _cameraMoveYTween = _cameraMain.transform.DOMoveY(transform.position.y + tempOffset.y, _focusedDuration);

            OnFocusCameraUnity?.Invoke();
        }

        [Button]
        public void UnfocusCamera()
        {
            _cameraSizeTween.Kill();
            _cameraSizeTween = _cameraMain.DOOrthoSize(_unfocusedCameraSize, _unfocusedDuration);

            _cameraMoveXTween.Kill();
            _cameraMoveYTween.Kill();
            _cameraMoveXTween = _cameraMain.transform.DOMoveX(_unfocusedCameraPosition.x, _unfocusedDuration);
            _cameraMoveYTween = _cameraMain.transform.DOMoveY(_unfocusedCameraPosition.y, _unfocusedDuration);

            OnUnfocusCameraUnity?.Invoke();

            if (_currentFocusedObject != null)
            {
                _currentFocusedObject.OnReceiveCameraUnfocusUnity?.Invoke();
                _currentFocusedObject = null;
            }
        }

        [Button]
        public void ShakeCamTest() => ShakeCamera(1f);
        public void ShakeCamera(float duration, float shakePosStrength = 1f, float shakeRotStrength = 1f)
        {
            if (_cameraMain == null) return;

            if (_cameraPosShakeTween != null)
            {
                if (_cameraPosShakeTween.IsPlaying()) return;
            }
            if (_cameraRotShakeTween != null)
            {
                if (_cameraRotShakeTween.IsPlaying())
                {
                    return;
                }
            }

            _cameraPosShakeTween.Kill();
            _cameraRotShakeTween.Kill();

            _cameraPosShakeTween = _cameraMain.DOShakePosition(duration, shakePosStrength);
            _cameraPosShakeTween = _cameraMain.DOShakeRotation(duration, shakeRotStrength);

            OnShakeCameraUnity?.Invoke();
        }
    }
}
