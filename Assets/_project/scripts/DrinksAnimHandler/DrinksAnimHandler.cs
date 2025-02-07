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

        [SerializeField] private Animator _blueCupAnimator;
        [SerializeField] private Animator _greenCupAnimator;
        [SerializeField] private Animator _coffeeCupAnimator;

        [SerializeField] private DataSound _soundCafe;
        [SerializeField] private DataSound _soundDrinkService;

        public void PlayBlueDrinkAnimation()
        {
            if (_blueAnimator == null) return;

            _blueAnimator.SetTrigger("AnimBottle");
            SoundManager.Instance.PlaySoundSFX(_soundDrinkService);
            SoundManager.Instance.PlaySoundSFX(_soundCafe);
        }
        public void PlayGreenDrinkAnimation()
        {
            if (_greenAnimator == null) return;

            _greenAnimator.SetTrigger("AnimBottle");
            SoundManager.Instance.PlaySoundSFX(_soundDrinkService);
            SoundManager.Instance.PlaySoundSFX(_soundCafe);
        }
        public void PlayCoffeeDrinkAnimation()
        {
            if (_coffeeAnimator == null) return;

            _coffeeAnimator.SetTrigger("AnimBottle");
            SoundManager.Instance.PlaySoundSFX(_soundDrinkService);
            SoundManager.Instance.PlaySoundSFX(_soundCafe);
        }

        public void PlayBlueCupAnimation()
        {
            if (_blueCupAnimator == null) return;

            _blueCupAnimator.SetTrigger("AnimBottle");
            SoundManager.Instance.PlaySoundSFX(_soundDrinkService);
            SoundManager.Instance.PlaySoundSFX(_soundCafe);
        }
        public void PlayGreenCupAnimation()
        {
            if (_greenCupAnimator == null) return;

            _greenCupAnimator.SetTrigger("AnimBottle");
            SoundManager.Instance.PlaySoundSFX(_soundDrinkService);
            SoundManager.Instance.PlaySoundSFX(_soundCafe);
        }
        public void PlayCofeeCupAnimation()
        {
            if (_coffeeCupAnimator == null) return;

            _coffeeCupAnimator.SetTrigger("AnimBottle");
            SoundManager.Instance.PlaySoundSFX(_soundDrinkService);
            SoundManager.Instance.PlaySoundSFX(_soundCafe);
        }
    }
}
