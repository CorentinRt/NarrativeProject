using DG.Tweening;
using PlasticGui.WorkspaceWindow.QueryViews;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace NarrativeProject
{
    [RequireComponent(typeof(Image))]
    public class DraggableObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Fields
        [Header("Drag Drop Parameters")]
        [SerializeField] private LayerMask _dropZonesLayerMask;
        [SerializeField] private Vector2 _offset = Vector2.zero;

        private IDropReceiver _overlappingDropReceiver;


        [Header("Drag Drop start parameters")]
        [SerializeField] private Transform _startingPoint;
        private Vector2 _startingPointPosition;
        [SerializeField] private bool _bDropReturnToStartingPoint;
        [SerializeField] private float _returnToStartingPointDuration;

        [Space(10)]

        [Header("Drag Drop lag parameters")]
        [SerializeField] private bool _bUseLag;
        [SerializeField] private float _lagDuration;
        private Tween _lagDragTween;

        [Header("Drag Drop physic simulation parameters")]
        [SerializeField] private bool _bUsePhysicSimulation;
        [SerializeField] private float _massSimulatedTreshhold;
        [SerializeField] private float _physicSimLagDuration;
        private Quaternion _startRotation;
        private Vector2 _lastMousePosition;

        private Tween _physicSimulationTween;

        #endregion

        public UnityEvent OnDropUnity;


        private void Reset()
        {
            _bUseLag = true;
            _lagDuration = 0.15f;

            _bDropReturnToStartingPoint = true;
            _returnToStartingPointDuration = 0.5f;

            _bUsePhysicSimulation = true;
            _massSimulatedTreshhold = 3f;

            _physicSimLagDuration = 0.3f;
        }
        private void Start()
        {
            if (_bDropReturnToStartingPoint)
            {
                if (_startingPoint == null)
                {
                    _startingPoint = transform;
                }

                _startingPointPosition = _startingPoint.position;
                transform.position = _startingPointPosition;
            }

            _startRotation = transform.rotation;
        }

        #region Drag
        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 targetPosition = eventData.position;

            targetPosition -= _offset;

            _lagDragTween.Kill();

            if (_bUseLag)
            {

                _lagDragTween = transform.DOMove(targetPosition, _lagDuration, true);
            }
            else
            {
                transform.position = targetPosition;
            }

            if (_bUsePhysicSimulation)
            {
                HandlePhysicsRotation(targetPosition);
            }

            _lastMousePosition = targetPosition;

            HandleDropZoneDetection(targetPosition);
        }

        #endregion

        public void OnEndDrag(PointerEventData eventData)
        {
            Drop(eventData);

            DraggableEndOverlappingDropReceiver();
        }

        protected virtual void Drop(PointerEventData eventData)
        {
            _lagDragTween.Kill(true);
            _physicSimulationTween.Kill(true);
            if (_bDropReturnToStartingPoint)
            {
                _lagDragTween = transform.DOMove(_startingPointPosition, _returnToStartingPointDuration, true);
            }

            _physicSimulationTween = transform.DORotate(new Vector3(0f, 0f, 0f), _returnToStartingPointDuration, RotateMode.Fast);

            OnDropUnity?.Invoke();
        }

        private void HandleDropZoneDetection(Vector2 mousePosition, bool bDropping = false)
        {
            if (Camera.main == null) return;

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);

            Collider2D detectedCollider = Physics2D.OverlapCircle(worldMousePos, 0.5f, _dropZonesLayerMask);

            if (detectedCollider == null)
            {
                DraggableEndOverlappingDropReceiver();
                return;
            }

            IDropReceiver dropReceiver = detectedCollider.GetComponent<IDropReceiver>();

            if (dropReceiver == null)
            {
                DraggableEndOverlappingDropReceiver();
                return;
            }

            if (dropReceiver != _overlappingDropReceiver)
            {
                DraggableEndOverlappingDropReceiver();

                dropReceiver.OnReceiveDragBeginOverlapping(gameObject);
            }

            _overlappingDropReceiver = dropReceiver;

            if (dropReceiver.CanReceive(gameObject))
            {
                if (bDropping)
                {
                    dropReceiver.OnReceiveDropDraggable(gameObject);
                }
            }
        }
        private void DraggableEndOverlappingDropReceiver()
        {
            if (_overlappingDropReceiver == null) return;

            _overlappingDropReceiver.OnReceiveDragEndOverlapping(gameObject);
            _overlappingDropReceiver = null;
        }


        #region Physics simualte rotation
        private void HandlePhysicsRotation(Vector2 newPosition)
        {
            Vector2 move = newPosition - _lastMousePosition;

            _physicSimulationTween.Kill();

            if (move.magnitude > _massSimulatedTreshhold)
            {
                if (Mathf.Abs(move.y) > Mathf.Abs(move.x) && move.y < 0f)
                {
                    float strength = move.magnitude / 20f;
                    _physicSimulationTween = transform.DORotate(new Vector3(0f, 0f, 170f * -Mathf.Sign(move.x) * strength), _physicSimLagDuration, RotateMode.Fast);
                }
                else
                {
                    float strength = move.magnitude / 50f;
                    _physicSimulationTween = transform.DORotate(new Vector3(0f, 0f, 90f * -Mathf.Sign(move.x) * strength), _physicSimLagDuration, RotateMode.Fast);
                }
            }
            else
            {
                _physicSimulationTween = transform.DORotate(new Vector3(0f, 0f, 0f), _physicSimLagDuration, RotateMode.Fast);
            }
        }
        #endregion

    }
}
