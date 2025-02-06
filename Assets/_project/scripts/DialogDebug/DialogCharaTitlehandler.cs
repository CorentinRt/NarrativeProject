using CREMOT.DialogSystem;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace NarrativeProject
{
    public class DialogCharaTitlehandler : MonoBehaviour
    {
        [System.Serializable]
        struct DialogPrefixToCharaTitle
        {
            [SerializeField] private string _dialogPrefix;
            [SerializeField] private string _charaTitle;
            [SerializeField] private Character _character;

            public string DialogPrefix { get => _dialogPrefix; set => _dialogPrefix = value; }
            public string CharaTitle { get => _charaTitle; set => _charaTitle = value; }
            public Character Character { get => _character; set => _character = value; }
        }

        #region Fields
        [Header("Param")]
        [SerializeField] private TextMeshProUGUI _titleCharaText;
        [SerializeField] private bool _useAutoHighlight;
        [SerializeField] private List<DialogPrefixToCharaTitle> _prefixToCharaTitleList;

        [Space(20)]

        [Header("Tests")]
        [SerializeField] private string _testName;

        [SerializeField] private DialogueController _dialogueController;



        #endregion

        private void Awake()
        {
            if (_dialogueController != null)
            {
                _dialogueController.OnDialogueUpdated += AutoSetCharaTitleFromDialId;

                if (_useAutoHighlight)
                {
                    _dialogueController.OnDialogueUpdated += AutoHighlightCharacterFromDialID;
                }
            }
        }
        private void OnDestroy()
        {
            if (_dialogueController != null)
            {
                _dialogueController.OnDialogueUpdated -= AutoSetCharaTitleFromDialId;

                if (_useAutoHighlight)
                {
                    _dialogueController.OnDialogueUpdated -= AutoHighlightCharacterFromDialID;
                }
            }
        }


        [Button]
        private void TestChangeTitleCharaDialog() => ChangeTitleCharaDialog(_testName);

        public void ChangeTitleCharaDialog(string charaName)
        {
            if (_titleCharaText == null) return;

            _titleCharaText.text = charaName;
        }

        private void AutoSetCharaTitleFromDialId(string dialogId)
        {
            Debug.LogWarning(dialogId);

            if (_prefixToCharaTitleList == null || _titleCharaText == null) return;


            var matchingEntry = _prefixToCharaTitleList.FirstOrDefault(entry => dialogId.StartsWith(entry.DialogPrefix));

            if (matchingEntry.CharaTitle == null) return;

            ChangeTitleCharaDialog(matchingEntry.CharaTitle);
        }

        private void AutoHighlightCharacterFromDialID(string dialogId)
        {
            if (_prefixToCharaTitleList == null) return;

            var matchingEntry = _prefixToCharaTitleList.FirstOrDefault(entry => dialogId.StartsWith(entry.DialogPrefix));

            if (matchingEntry.Character == null) return;

            if (CharacterManager.Instance != null)
            {
                CharacterManager.Instance.DarkenAllCharacters();
            }

            matchingEntry.Character.LightUpCharacter();
        }
    }
}
