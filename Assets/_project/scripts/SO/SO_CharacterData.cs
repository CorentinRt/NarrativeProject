using System.Collections.Generic;
using UnityEngine;
using CREMOT.DialogSystem;
public enum DrinkType
{
    Vin,
    Biere,
    Whisky,
    PierreChabrier,
    LeComteDeMentheEtCristaux
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
    [SerializeField] List<Sprite> _sprites = new List<Sprite>();
    [SerializeField] string _name;
    [SerializeField] List<DrinkType> _prefs = new List<DrinkType>();
    [SerializeField] DrunkState _defaultState;
    [SerializeField] DialogueGraphSO _dialogueGraphData;

    public List<Sprite> Sprites { get => _sprites; }
    public string Name { get => _name; }
    public List<DrinkType> Prefs { get => _prefs; }
    public DrunkState DefaultState { get => _defaultState; }
    public DialogueGraphSO DialogueGraphData { get => _dialogueGraphData; }
}
