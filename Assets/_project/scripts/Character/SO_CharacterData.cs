using System.Collections.Generic;
using UnityEngine;
using CREMOT.DialogSystem;
using System;


namespace NarrativeProject
{
    public enum DrinkType
    {
        Rhum_Puissance_2,
        Whisky_Puissance_2,
        Cofee
    };

    public enum DrunkState
    {
        Clean = 0,
        Dizzy = 1,
        Drunk = 2
    };

    public enum FriendshipState
    {
        Neutral,
        Happy,
        Sad
    };

    [Serializable]
    public struct DayInteractions
    {
        public int InteractionsBeforeComing;
        public int InteractionsBeforeLeaving;  
    }



    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character/CharacterData", order = 1)]
    public class SO_CharacterData : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField, Range(0, 100)] int _defaultFriendShipScale;
        [SerializeField, Range(0, 100)] int _defaultdrunkScale;
        [SerializeField] DialogueGraphSO _dialogueGraphData;
        [SerializeField] int numberOfClues = 0;
        [SerializeField] List<Sprite> _sprites = new List<Sprite>() { null, null, null, null, null, null };
        [HideInInspector][SerializeField] List<DrinkType> _drinkType = new List<DrinkType>();
        [HideInInspector][SerializeField] List<int> _drinkEffect = new List<int>();
        //[HideInInspector][SerializeField] List<int> _daysComing = new List<int>();
        //[HideInInspector][SerializeField] List<DayInteractions> _interactionsData = new List<DayInteractions>();
        [HideInInspector][SerializeField] List<int> _drinkEffectFriendShip = new List<int>();
        [SerializeField] Dictionary<DrinkType, int> _drinkEffects = new Dictionary<DrinkType, int>();
        [SerializeField] Dictionary<DrinkType, int> _drinkEffectsFriendShip = new Dictionary<DrinkType, int>();
        //[SerializeField] Dictionary<int, DayInteractions> _daysComingData = new Dictionary<int, DayInteractions>();

        public List<Sprite> Sprites { get => _sprites; }
        public DialogueGraphSO DialogueGraphData { get => _dialogueGraphData; }
        public Dictionary<DrinkType, int> DrinkEffects { get => _drinkEffects; }
        public Dictionary<DrinkType, int> DrinkEffectsFriendShip { get => _drinkEffectsFriendShip; }
        //public Dictionary<int, DayInteractions> DaysComingData { get => _daysComingData; }
        public string Name { get => _name; set => _name = value; }
        public List<int> DrinkEffect { get => _drinkEffect; }
        public List<int> DrinkEffectFriendShip { get => _drinkEffectFriendShip; }
        public List<DrinkType> DrinkType { get => _drinkType; }
        public int DefaultFriendShipScale { get => _defaultFriendShipScale; set => _defaultFriendShipScale = value; }
        public int DefaultdrunkScale { get => _defaultdrunkScale; set => _defaultdrunkScale = value; }
        public int NumberOfClues { get => numberOfClues; }

        //public List<int> DaysComing { get => _daysComing; }
        //public List<DayInteractions> InteractionsData { get => _interactionsData; }

        private void Reset()
        {
            _drinkEffect.Clear();
            _drinkType.Clear();
            _drinkEffects.Clear();
            Debug.Log("Reset" + _drinkEffect.Count);
        }
    }
}

