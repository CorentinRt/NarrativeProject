using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NarrativeProject
{
    public class UIButtonsEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Tween _btnEffectTween;
        [SerializeField] private float _targetPressedScale;
        [SerializeField] private float _duration;

        public void OnPointerDown(PointerEventData eventData)
        {
            
             _btnEffectTween?.Kill(true);

            _btnEffectTween = transform.DOScale(_targetPressedScale, _duration);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _btnEffectTween?.Kill(true);

            _btnEffectTween = transform.DOScale(1f, _duration);
        }
    }
}
