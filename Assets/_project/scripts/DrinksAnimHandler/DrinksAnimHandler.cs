using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class DrinksAnimHandler : MonoBehaviour
    {
        [SerializeField] private Animator _blueAnimator;
        [SerializeField] private Animator _greenAnimator;
        [SerializeField] private Animator _coffeeAnimator;


        public void PlayBlueDrinkAnimation()
        {
            if (_blueAnimator == null) return;

            _blueAnimator.SetTrigger("AnimBottle");
        }
        public void PlayGreenDrinkAnimation()
        {
            if (_greenAnimator == null) return;

            _greenAnimator.SetTrigger("AnimBottle");
        }
        public void PlayCoffeeDrinkAnimation()
        {
            if (_coffeeAnimator == null) return;

            _coffeeAnimator.SetTrigger("AnimBottle");
        }
    }
}
