using System.Collections.Generic;
using UnityEngine;
using CREMOT.DialogSystem;


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
        Clean,
        Happy,
        Drunk
    };

    public enum FriendshipState
    {
        Neutral,
        Happy,
        Sad
    };


    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character/CharacterData", order = 1)]
    public class SO_CharacterData : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField, Range(0, 100)] int _defaultFriendShipScale;
        [SerializeField, Range(0, 100)] int _defaultdrunkScale;
        [SerializeField] DialogueGraphSO _dialogueGraphData;
        [SerializeField] List<Sprite> _sprites = new List<Sprite>() { null, null, null, null, null, null };
        [HideInInspector][SerializeField]List<DrinkType> _drinkType = new List<DrinkType>();
        [HideInInspector][SerializeField]List<int> _drinkEffect = new List<int>();
        [HideInInspector][SerializeField] List<int> _drinkEffectFriendShip = new List<int>();
        [SerializeField] Dictionary<DrinkType, int> _drinkEffects = new Dictionary<DrinkType, int>();
        [SerializeField] Dictionary<DrinkType, int> _drinkEffectsFriendShip = new Dictionary<DrinkType, int>();

        public List<Sprite> Sprites { get => _sprites; }
        public DialogueGraphSO DialogueGraphData { get => _dialogueGraphData; }
        public Dictionary<DrinkType, int> DrinkEffects { get => _drinkEffects; }
        public Dictionary<DrinkType, int> DrinkEffectsFriendShip { get => _drinkEffectsFriendShip; }
        public string Name { get => _name; set => _name = value; }
        public List<int> DrinkEffect { get => _drinkEffect; }
        public List<int> DrinkEffectFriendShip { get => _drinkEffectFriendShip; }
        public List<DrinkType> DrinkType { get => _drinkType; }
        public int DefaultFriendShipScale { get => _defaultFriendShipScale; set => _defaultFriendShipScale = value; }
        public int DefaultdrunkScale { get => _defaultdrunkScale; set => _defaultdrunkScale = value; }

        private void Reset()
        {
            _drinkEffect.Clear();
            _drinkType.Clear();
            _drinkEffects.Clear();
            Debug.Log("Reset" + _drinkEffect.Count);
        }
    }
}

