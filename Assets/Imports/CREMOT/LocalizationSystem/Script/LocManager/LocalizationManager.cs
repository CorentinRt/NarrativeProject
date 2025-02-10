using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static LocalizationManager;


public class LocalizationManager : MonoBehaviour
{

    #region Fields
    private static LocalizationManager _instance;

    //private Language _currentLanguage;

    [System.Serializable]
    public enum Language
    {
        ENGLISH = 0,
        FRENCH = 1,
        //SPANISH = 2
    }

    [SerializeField] private LocalizationDataSO _localizationDataSO;

    #endregion


    #region Delegates

    public event Action<Language> OnLanguageUpdated;
    public event Action OnLanguageUpdatedNoParam;

    public UnityEvent OnLanguageUpdatedUnity;

    #endregion

    #region Properties
    public static LocalizationManager Instance { get => _instance; set => _instance = value; }
    //public Language CurrentLanguage { get => _currentLanguage; set => _currentLanguage = value; }

    #endregion

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    public void SetLanguage(Language language)
    {
        PlayerPrefs.SetInt("CurrentLanguage", (int)language);
        PlayerPrefs.Save();
    }

    public Language GetLanguage()
    {
        
        int languageIndex = PlayerPrefs.GetInt("CurrentLanguage", 0);
        return (Language)languageIndex;
    }

    public void ChangeLanguages(int nextLanguage)
    {
        SetLanguage((Language)nextLanguage);
        OnLanguageUpdated?.Invoke(GetLanguage());
        OnLanguageUpdatedNoParam?.Invoke();
        OnLanguageUpdatedUnity?.Invoke();
    }

    public string GetLocalisedTextWithID(string textId)
    {
        if (_localizationDataSO == null) return "";

        if (!_localizationDataSO.LocalizationData.ContainsKey(textId)) return "";

        string languageKeyId = GetLanguagekeyIdFromEnum(GetLanguage());

        if (!_localizationDataSO.LocalizationData[textId].ContainsKey(languageKeyId)) return "";

        return _localizationDataSO.LocalizationData[textId][languageKeyId];
    }

    public string GetLanguagekeyIdFromEnum(Language language)
    {
        switch (language)
        {
            case Language.ENGLISH:
                return "ENG";
            case Language.FRENCH:
                return "FR";
            //case Language.SPANISH:
            //    return "COR";
            default:
                return "";
        }
    }
}
