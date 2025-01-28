using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class UIDaysTransitions : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform _containerTransition;
        [SerializeField] private Transform _show;
        [SerializeField] private Transform _hide;

        [SerializeField] private float _duration;

        [SerializeField] private Ease _ease;

        private Tween _transitionTween;

        #endregion

        #region Properties


        #endregion

        private void Start()
        {
            HideTransition();
        }

        [Button]
        public void ShowTransition()
        {
            if (_transitionTween != null)
            {
                _transitionTween.Kill();
            }
            _containerTransition.DOMove(_show.position, _duration).SetEase(_ease);
        }

        [Button]
        public void HideTransition()
        {
            if (_transitionTween != null)
            {
                _transitionTween.Kill();
            }
            _containerTransition.DOMove(_hide.position, _duration);
        }
    }
}
