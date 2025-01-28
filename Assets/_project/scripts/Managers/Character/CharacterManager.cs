using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticPipe.Server.MonitorStats;

namespace NarrativeProject
{
    public class CharacterManager : MonoBehaviour
    {
        static CharacterManager _instance;

        [SerializeField] List<Character> _characterList;

        List<Character> _charactersThisDay = new List<Character>();

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

        public List<Character> GetCharactersThisDay(int day)
        {
            _charactersThisDay = new List<Character>();
            foreach (Character character in _characterList)
            {
                if (character.Data.DaysComingData.ContainsKey(day))
                {
                    _charactersThisDay.Add(character);
                }
            }
            return _charactersThisDay;
        }

        public void CheckWhoIsComing(int currentDay, int interactions)
        {
            foreach (Character character in _charactersThisDay)
            {
                if (character.CheckComingAtDay(currentDay, interactions) && character.ComingState == ComingState.Coming)
                {
                    //TODO FAIRE VIENDRE LE PERSO
                    character.ComingState = ComingState.Here;
                }
            }
        }

        public void CheckWhoIsLeaving(int currentDay, int interactions)
        {
            foreach (Character character in _charactersThisDay)
            {
                if (character.CheckLeavingAtDay(currentDay, interactions) && character.ComingState == ComingState.Here)
                {
                    //TODO FAIRE PLUS VIENDRE LE PERSO
                    character.ComingState = ComingState.Left;
                }
            }
        }
    }
}
