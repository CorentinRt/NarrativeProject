using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(Image))]
public class DragDropObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Drag Drop start parameters")]
    [SerializeField] private Transform _startingPoint;
    private Vector2 _startingPointPosition;
    [SerializeField] private bool _bDropReturnToStartingPoint;
    [SerializeField] private float _returnToStartingPointDuration;

    [Header("Drag Drop lag parameters")]
    [SerializeField] private bool _bUseLag;
    [SerializeField] private float _lagDuration;

    private Tween _lagDragTween;


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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = eventData.position;

        _lagDragTween.Kill();

        if (_bUseLag)
        {

            _lagDragTween = transform.DOMove(targetPosition, _lagDuration, true);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Drop(eventData);
    }

    protected virtual void Drop(PointerEventData eventData)
    {
        _lagDragTween.Kill(true);
        if (_bDropReturnToStartingPoint)
        {
            _lagDragTween = transform.DOMove(_startingPointPosition, _returnToStartingPointDuration, true);
        }
    }
}
