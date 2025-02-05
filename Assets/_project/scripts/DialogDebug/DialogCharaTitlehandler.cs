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

            public string DialogPrefix { get => _dialogPrefix; set => _dialogPrefix = value; }
            public string CharaTitle { get => _charaTitle; set => _charaTitle = value; }
        }

        #region Fields
        [SerializeField] private TextMeshProUGUI _titleCharaText;

        [SerializeField] private string _testName;

        [SerializeField] private DialogueController _dialogueController;

        [SerializeField] private List<DialogPrefixToCharaTitle> _prefixToCharaTitleList;

        #endregion

        private void Awake()
        {
            if (_dialogueController != null)
            {
                _dialogueController.OnDialogueUpdated += AutoSetCharaTitleFromDialId;
            }
        }
        private void OnDestroy()
        {
            if (_dialogueController != null)
            {
                _dialogueController.OnDialogueUpdated -= AutoSetCharaTitleFromDialId;
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
    }
}
