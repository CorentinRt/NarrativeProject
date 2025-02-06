using CREMOT.DialogSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json.Bson;
using UnityEngine;

namespace NarrativeProject
{
    public class DialogInventoryToGameLink : MonoBehaviour
    {
        [System.Serializable]
        enum EActionType
        {
            NONE = 0,
            CLUE = 1,
            DRINKCHARA = 2
        }
        [System.Serializable]
        struct KeyNameToActionType
        {
            public string actionNom;
            public EActionType actionType;
        }
        [System.Serializable]
        struct InventoryPrefixToGameAction
        {
            [SerializeField] private string _dialogPrefix;
            [SerializeField] private Character _character;

            public string DialogPrefix { get => _dialogPrefix; set => _dialogPrefix = value; }
            public Character Character { get => _character; set => _character = value; }
        }

        #region Fields
        [SerializeField] private DialogueInventory _dialogueInventory;

        [SerializeField] private List<InventoryPrefixToGameAction> _inventoryPrefixToGameAction;

        [SerializeField] private List<KeyNameToActionType> _keyNameToActionsTypes;

        #endregion


        private void Awake()
        {
            if (_dialogueInventory != null)
            {
                _dialogueInventory.OnUpdateDialogInventoryWithNewValue += LinkNewValueToGame;
            }
        }
        private void OnDestroy()
        {
            if (_dialogueInventory != null)
            {
                _dialogueInventory.OnUpdateDialogInventoryWithNewValue -= LinkNewValueToGame;
            }
        }


        private void LinkNewValueToGame(string keyName, int newValue)
        {
            var matchingChara = _inventoryPrefixToGameAction.FirstOrDefault(entry => keyName.StartsWith(entry.DialogPrefix));

            EActionType actionType = GetActionType(keyName);


        }

        private EActionType GetActionType(string keyName)
        {
            var action = _keyNameToActionsTypes.FirstOrDefault(entry => keyName.Contains(entry.actionNom));

            return action.actionType;
        }

        public void AddDrinkStateToInventoryDialog(Character character, int drinkLevel)
        {
            if (_dialogueInventory == null) return;

            var matchingChara = _inventoryPrefixToGameAction.FirstOrDefault(entry => character == entry.Character);

            string tempKey = matchingChara.DialogPrefix + "_" + "Drunkness";

            Debug.LogWarning("Set key : " + tempKey + " : " + drinkLevel.ToString());

            _dialogueInventory.SetItem(tempKey, drinkLevel);
        }
    }
}
