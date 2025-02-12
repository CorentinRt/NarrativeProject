using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NarrativeProject
{
    public class UICounterDrinks : MonoBehaviour
    {
        [Header("Drinks")]

        [SerializeField] private RectTransform _drinksHub;

        [SerializeField] private List<RectTransform> _drinks;
        private List<Vector3> _drinksOriginalPosition;
        private List<Vector3> _drinksHubsPosition;
        [SerializeField] private float _offsetsHub;

        [SerializeField] private float _displayAllDuration;
        [SerializeField] private float _groupHubAllDuration;


        [Header("Gun")]

        [SerializeField] private Button _gunButton;
        public UnityEvent OnDisplayGunUnity;


        private List<Tween> _moveTweens;

        public UnityEvent OnGroupAllDrinks;
        public UnityEvent OnDisplayAllDrinks;

        private void Start()
        {
            _drinksOriginalPosition = new List<Vector3>();
            _drinksHubsPosition = new List<Vector3>();
            _moveTweens = new List<Tween>();

            Vector3 currentOffset = new Vector3(0f, 0f, 0f);

            foreach (var drink in _drinks)
            {
                if (drink == null) continue;

                _drinksOriginalPosition.Add(drink.position);
                _drinksHubsPosition.Add(_drinksHub.position - currentOffset);
                _moveTweens.Add(null);

                currentOffset += new Vector3(_offsetsHub, 0f, 0f);
            }

            GroupDrinksToHub();

            HideGun();
            DisableGun();
        }

        [Button]
        public void GroupDrinksToHub()
        {
            for (int i = 0; i < _drinks.Count; ++i)
            {
                if (_drinks[i] == null) continue;
                if (_drinksHubsPosition[i] == null) continue;
                if (_moveTweens.Count <= i) continue;

                _moveTweens[i].Kill(true);

                _moveTweens[i] = _drinks[i].DOMoveX(_drinksHubsPosition[i].x, _groupHubAllDuration);
            }
            OnGroupAllDrinks?.Invoke();
        }
        [Button]
        public void DisplayDrinks()
        {
            for (int i = 0; i < _drinks.Count; ++i)
            {
                if (_drinks[i] == null) continue;
                if (_drinksOriginalPosition[i] == null) continue;
                if (_moveTweens.Count <= i) continue;

                _moveTweens[i].Kill(true);

                _moveTweens[i] = _drinks[i].DOMoveX(_drinksOriginalPosition[i].x, _displayAllDuration);
            }
            OnDisplayAllDrinks?.Invoke();
        }

        [Button]
        public void DisplayGun()
        {
            if (!_gunButton.gameObject) return;

            _gunButton.gameObject.SetActive(true);
            OnDisplayGunUnity?.Invoke();
        }

        [Button]
        public void HideGun()
        {
            if (!_gunButton.gameObject) return;

            _gunButton.gameObject.SetActive(false);
        }
        public void EnableGun()
        {
            if (!_gunButton) return;

            _gunButton.enabled = true;
        }
        public void DisableGun()
        {
            if (!_gunButton) return;

            _gunButton.enabled = false;
        }
    }
}
