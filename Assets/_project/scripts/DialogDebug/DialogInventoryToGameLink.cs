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
        #region Datas
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
        #endregion


        #region Fields
        [SerializeField] private DialogueInventory _dialogueInventory;

        [SerializeField] private List<InventoryPrefixToGameAction> _inventoryPrefixToGameAction;

        [SerializeField] private List<KeyNameToActionType> _keyNameToActionsTypes;

        #endregion

        // Drink state create key
        public void AddDrinkStateToInventoryDialog(Character character, int drinkLevel)
        {
            if (_dialogueInventory == null) return;
            if (character == null) return;

            var matchingChara = _inventoryPrefixToGameAction.FirstOrDefault(entry => character == entry.Character);

            string tempKey = matchingChara.DialogPrefix + "_" + "Drunkness";

            Debug.LogWarning("Set drink key : " + tempKey + " : " + drinkLevel.ToString());

            _dialogueInventory.SetItem(tempKey, drinkLevel);
        }

        // Clue create key
        public void AddClueToInventoryDialog(string characterPrefix, int clueId)
        {
            if (_dialogueInventory == null) return;
            if (characterPrefix == "") return;

            //var matchingChara = _inventoryPrefixToGameAction.FirstOrDefault(entry => character == entry.Character);

            string tempKey = characterPrefix + "_" + "Clue" + clueId.ToString();

            Debug.LogWarning("Set Clue key : " + tempKey);

            _dialogueInventory.SetItem(tempKey, 1);
        }
    }
}
