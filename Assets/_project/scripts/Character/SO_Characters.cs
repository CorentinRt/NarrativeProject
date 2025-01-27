using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    [CreateAssetMenu(fileName = "CharactersDatabase", menuName = "ScriptableObjects/Character/CharactersDatabase", order = 1)]
    public class SO_Characters : ScriptableObject
    {
        public List<SO_CharacterData> Characters = new List<SO_CharacterData>();
        public SO_CharacterData GetCharacter(string name)
        {
            foreach (SO_CharacterData character in Characters)
            {
                if (character.Name== name)
                {
                    return character;
                }
            }
            return null;
        }

        public void AddCharacter(SO_CharacterData character)
        {
            Characters.Add(character);
        }
    }
}
