using System.Collections.Generic;
using UnityEngine;
using CREMOT.DialogSystem;
using static UnityEngine.GraphicsBuffer;


namespace NarrativeProject
{
    public enum DrinkType
    {
        Vin_Puissance_1,
        Biere_Puissance_2,
        Whisky_Puissance_3,
        PierreChabrier_Puissance_4,
        LeComteDeMentheEtCristaux_Puissance_5
    };

    public enum DrunkState
    {
        Arrache,
        Clean,
        Coma,
        ComaEthylique
    }


    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
    public class SO_CharacterData : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField] DrunkState _defaultState;
        [SerializeField] DialogueGraphSO _dialogueGraphData;
        [SerializeField] List<DrinkType> _prefs = new List<DrinkType>();
        [SerializeField] List<Sprite> _sprites = new List<Sprite>();
        [VisibleInDebug][SerializeField]List<DrinkType> _drinkType = new List<DrinkType>();
        [VisibleInDebug][SerializeField]List<DrunkState> _drinkEffect = new List<DrunkState>();
        [SerializeField] Dictionary<DrinkType, DrunkState> _drinkEffects = new Dictionary<DrinkType, DrunkState>();

        public List<Sprite> Sprites { get => _sprites; }
        public List<DrinkType> Prefs { get => _prefs; }
        public DrunkState DefaultState { get => _defaultState; }
        public DialogueGraphSO DialogueGraphData { get => _dialogueGraphData; }
        public Dictionary<DrinkType, DrunkState> DrinkEffects { get => _drinkEffects; }
        public string Name { get => _name; set => _name = value; }
        public List<DrunkState> DrinkEffect { get => _drinkEffect; }
        public List<DrinkType> DrinkType { get => _drinkType; }

        private void Reset()
        {
            _drinkEffect.Clear();
            _drinkType.Clear();
            _drinkEffects.Clear();
            Debug.Log("Reset" + _drinkEffect.Count);
        }
    }
}

