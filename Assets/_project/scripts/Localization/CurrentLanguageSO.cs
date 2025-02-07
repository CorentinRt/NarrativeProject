using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    [CreateAssetMenu(fileName = "CurrentLanguage", menuName = "Localization/CurrentLanguage", order = 2)]
    [System.Serializable]
    public class CurrentLanguageSO : ScriptableObject
    {
        public LocalizationManager.Language currentLanguage = LocalizationManager.Language.ENGLISH;
    }
}
