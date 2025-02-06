using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.DialogSystem
{
    public class DialogueInventory : MonoBehaviour
    {
        #region Fields
        private static DialogueInventory _instance;

        private Dictionary<string, int> _dialogueItemsInventory = new Dictionary<string, int>();

        #endregion

        #region Properties
        public static DialogueInventory Instance { get => _instance; set => _instance = value; }
        public Dictionary<string, int> DialogueItemsInventory { get => _dialogueItemsInventory; set => _dialogueItemsInventory = value; }


        #endregion

        #region Delegates

        public event Action OnUpdatedDialogueInventory;

        public event Action<string, int> OnUpdateDialogInventoryWithNewValue;

        #endregion


        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            _instance = this;
        }


        public void AddItem(string stringName, int quantity)
        {
            if (string.IsNullOrEmpty(stringName)) return;

            Debug.LogWarning($"Call functions with param worked : {stringName} + {quantity}");

            if (_dialogueItemsInventory.ContainsKey(stringName))
            {
                _dialogueItemsInventory[stringName] += quantity;
            }
            else
            {
                _dialogueItemsInventory[stringName] = quantity;
            }

            OnUpdatedDialogueInventory?.Invoke();
            OnUpdateDialogInventoryWithNewValue?.Invoke(stringName, _dialogueItemsInventory[stringName]);
        }
        public void RemoveItem(string stringName, int quantity)
        {
            if (string.IsNullOrEmpty(stringName)) return;

            if (_dialogueItemsInventory.ContainsKey(stringName))
            {
                _dialogueItemsInventory[stringName] -= quantity;
                if (_dialogueItemsInventory[stringName] < 0)
                {
                    _dialogueItemsInventory[stringName] = 0;
                }
            }

            OnUpdatedDialogueInventory?.Invoke();
            OnUpdateDialogInventoryWithNewValue?.Invoke(stringName, _dialogueItemsInventory[stringName]);
        }
        public void SetItem(string stringName, int quantity)
        {
            if (string.IsNullOrEmpty(stringName)) return;

            _dialogueItemsInventory[stringName] = quantity;
            
            OnUpdatedDialogueInventory?.Invoke();
            OnUpdateDialogInventoryWithNewValue?.Invoke(stringName, _dialogueItemsInventory[stringName]);
        }
        public bool HasItem(string item, int quantity)
        {
            if (string.IsNullOrEmpty(item)) return false;

            if (quantity <= 0) return true;

            if (!_dialogueItemsInventory.ContainsKey(item)) return false;

            if (_dialogueItemsInventory[item] < quantity) return false;

            return true;
        }
        public bool HasExactlyItem(string item, int quantity)
        {
            if (string.IsNullOrEmpty(item)) return false;

            if (quantity <= 0) return true;

            if (!_dialogueItemsInventory.ContainsKey(item)) return false;

            if (_dialogueItemsInventory[item] == quantity) return true;

            return false;
        }
        public bool IsUnderItem(string item, int quantity)
        {
            if (string.IsNullOrEmpty(item)) return false;

            if (quantity <= 0) return true;

            if (!_dialogueItemsInventory.ContainsKey(item)) return true;

            if (_dialogueItemsInventory[item] < quantity) return true;

            return false;
        }
    }
}