using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class CharacterManager : MonoBehaviour
    {
        static CharacterManager _instance;

        [SerializeField] List<Character> _characterList;

        public static CharacterManager Instance { get => _instance; set => _instance = value; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }

        public void InitManager()
        {
            foreach(Character character in _characterList)
            {
                character.Init();
            }
        }

        public List<Character> GetCharactersThisDay()
        {
            List<Character> charactersThisDay = new List<Character>();
            foreach (Character character in _characterList)
            {
                if (character.InComingDays <= 0)
                {
                    charactersThisDay.Add(character);
                }
            }
            return charactersThisDay;
        }
    }
}
