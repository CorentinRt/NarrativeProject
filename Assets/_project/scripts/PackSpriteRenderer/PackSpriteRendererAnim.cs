using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class PackSpriteRendererAnim : MonoBehaviour
    {
        [Header("Color")]
        [SerializeField] private Color _darkenColor;
        [SerializeField] private float _animColorDuration;

        [Header("Fade in / out")]
        [SerializeField] private float _animFadeDuration;

        private List<SpriteRenderer> _packSpriteRenderer;

        private List<Tween> _allFadeInTweens;
        private List<Tween> _allDarkenLightUpTweens;

        public List<SpriteRenderer> PackSpriteRenderer { get => _packSpriteRenderer; }

        private void Awake()
        {
            _packSpriteRenderer = new List<SpriteRenderer>();

            _allFadeInTweens = new List<Tween>();
            _allDarkenLightUpTweens = new List<Tween>();

            _packSpriteRenderer = InitPackSpriteRenderer();
        }

        private List<SpriteRenderer> InitPackSpriteRenderer()
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            List<SpriteRenderer> spriteRenderersList = new List<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderersList.Add(spriteRenderer);

                _allFadeInTweens.Add(null);
                _allDarkenLightUpTweens.Add(null);
            }

            return spriteRenderersList;
        }

        public void FadeInAll()
        {
            for (int i = 0; i < _packSpriteRenderer.Count; ++i)
            {
                if (_allFadeInTweens.Count <= i) return;
                if (_packSpriteRenderer[i] == null) return;

                if (_allFadeInTweens[i] != null)
                {
                    _allFadeInTweens[i].Kill();
                }

                _allFadeInTweens[i] = _packSpriteRenderer[i].DOFade(1f, _animFadeDuration);
            }
        }
        public void FadeOutAll()
        {
            for (int i = 0; i < _packSpriteRenderer.Count; ++i)
            {
                if (_allFadeInTweens.Count <= i) return;
                if (_packSpriteRenderer[i] == null) return;

                if (_allFadeInTweens[i] != null)
                {
                    _allFadeInTweens[i].Kill();
                }

                _allFadeInTweens[i] = _packSpriteRenderer[i].DOFade(0f, _animFadeDuration);
            }
        }
        public void DarkenAll()
        {
            for (int i = 0; i < _packSpriteRenderer.Count ; ++i)
            {
                if (_allDarkenLightUpTweens.Count <= i) return;
                if (_packSpriteRenderer[i] == null) return;

                if (_allDarkenLightUpTweens[i] != null)
                {
                    _allDarkenLightUpTweens[i].Kill();
                }

                _allDarkenLightUpTweens[i] = _packSpriteRenderer[i].DOColor(_darkenColor, _animColorDuration);
            }
        }
        public void LightUpAll()
        {
            for (int i = 0; i < _packSpriteRenderer.Count; ++i)
            {
                if (_allDarkenLightUpTweens.Count <= i) return;
                if (_packSpriteRenderer[i] == null) return;

                if (_allDarkenLightUpTweens[i] != null)
                {
                    _allDarkenLightUpTweens[i].Kill();
                }

                _allDarkenLightUpTweens[i] = _packSpriteRenderer[i].DOColor(Color.white, _animColorDuration);
            }
        }
    }
}
