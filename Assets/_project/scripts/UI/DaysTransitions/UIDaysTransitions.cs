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
        [SerializeField] private Transform _btnNewsPaper;
        [SerializeField] private Transform _show;
        [SerializeField] private Transform _hide;

        [SerializeField] private float _duration;

        [SerializeField] private float _newsPaperAppearDuration;

        [SerializeField] private Ease _ease;

        private Tween _transitionTween;
        private Tween _transitionNewsPaperTween;
        private Tween _transitionRotNewsPaperTween;

        #endregion

        #region Properties


        #endregion

        private void Start()
        {
            if (DayManager.Instance != null)
            {
                DayManager.Instance.OnPreDay += ShowTransition;
            }
            ShowTransition();

            //ShowTransition(0);
            //HideTransition();
        }

        private void OnDestroy()
        {
            if (DayManager.Instance != null)
            {
                DayManager.Instance.OnPreDay -= ShowTransition;
            }
        }

        [Button] private void TestShowTransition() => ShowTransition();
        public void ShowTransition(int dayIndex = 0)
        {
            if (_transitionTween != null)
            {
                _transitionTween.Kill();
            }
            _transitionTween = _containerTransition.DOMove(_show.position, _duration).SetEase(_ease).OnComplete(() => ShowNewsPaper());
        }

        [Button]
        public void HideTransition()
        {
            if (_transitionTween != null)
            {
                _transitionTween.Kill();
            }
            _transitionTween = _containerTransition.DOMove(_hide.position, _duration).OnComplete(() => HideNewsPaper());
        }

        public void ShowNewsPaper()
        {
            if (_transitionNewsPaperTween != null)
            {
                _transitionNewsPaperTween.Kill(true);
            }
            if (_transitionRotNewsPaperTween != null)
            {
                _transitionRotNewsPaperTween.Kill(true);
            }

            _btnNewsPaper.transform.rotation = Quaternion.identity;
            _btnNewsPaper.transform.localScale = Vector3.zero;

            _btnNewsPaper.gameObject.SetActive(true);
            _transitionNewsPaperTween = _btnNewsPaper.DOScale(1f, _newsPaperAppearDuration);
            _transitionRotNewsPaperTween = _btnNewsPaper.DORotate(new Vector3(0f, 0f, 380f), 0.7f, RotateMode.FastBeyond360);
        }
        public void HideNewsPaper()
        {
            if (_transitionNewsPaperTween != null)
            {
                _transitionNewsPaperTween.Kill(true);
            }
            if (_transitionRotNewsPaperTween != null)
            {
                _transitionRotNewsPaperTween.Kill(true);
            }
            _transitionNewsPaperTween = _btnNewsPaper.transform.DOScale(Vector3.zero, _newsPaperAppearDuration);
        }

    }
}
