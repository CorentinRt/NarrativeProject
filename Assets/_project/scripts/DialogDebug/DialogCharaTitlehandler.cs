using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NarrativeProject
{
    public class DialogCharaTitlehandler : MonoBehaviour
    {
        #region Fields
        [SerializeField] private TextMeshProUGUI _titleCharaText;

        [SerializeField] private string _testName;

        #endregion

        [Button]
        private void TestChangeTitleCharaDialog() => ChangeTitleCharaDialog(_testName);

        public void ChangeTitleCharaDialog(string charaName)
        {
            if (_titleCharaText == null) return;

            _titleCharaText.text = charaName;
        }
    }
}
