using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CREMOT.LocalizationSystem;
using TMPro;
using System;

namespace NarrativeProject
{
    public class SwitchLanguage : MonoBehaviour
    {
        [SerializeField] private DataSound Sound;
        [SerializeField] private TextMeshProUGUI textLanguage;
        private LocalizationManager localizationManager;
        private int numberLanguage = Enum.GetValues(typeof(LocalizationManager.Language)).Length;
        void Start ()
        {
            localizationManager = FindObjectOfType<LocalizationManager>();
            UpdateText();
        }

        public void LeftButton()
        {
            if((int)localizationManager.GetLanguage() == 0)
            {
                localizationManager.ChangeLanguages(numberLanguage - 1);
            }
            else
            {
                localizationManager.ChangeLanguages((int)localizationManager.GetLanguage() - 1);
            }
            UpdateText();
        }

        public void RightButton()
        {
            if ((int)localizationManager.GetLanguage() == numberLanguage-1)
            {
                localizationManager.ChangeLanguages(0);
            }
            else
            {
                localizationManager.ChangeLanguages((int)localizationManager.GetLanguage() + 1);
            }
            UpdateText();
        }

        private void UpdateText()
        {
            textLanguage.text = localizationManager.GetLanguage().ToString();
        }

    }
}
