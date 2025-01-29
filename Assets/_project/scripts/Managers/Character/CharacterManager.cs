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
        public List<Character> CharactersThisDay { get => _charactersThisDay; set => _charactersThisDay = value; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }
        private void OnDestroy()
        {
            if (DayManager.Instance != null)
            {
                DayManager.Instance.OnUpdateCurrentInteractionCount -= CheckWhoIsComing;
                DayManager.Instance.OnUpdateCurrentInteractionCount -= CheckWhoIsLeaving;
                DayManager.Instance.OnEndDay -= ResetCharactersDrunkState;
            }
        }

        public void InitManager()
        {
            if (_characterList == null)
            {
                Debug.LogWarning("Character List is missing in Character Manager");
            }

            foreach(Character character in _characterList)
            {
                character.Init();
            }
            if (DayManager.Instance != null)
            {
                DayManager.Instance.OnUpdateCurrentInteractionCount += CheckWhoIsComing;
                DayManager.Instance.OnUpdateCurrentInteractionCount += CheckWhoIsLeaving;
                DayManager.Instance.OnEndDay += ResetCharactersDrunkState;
            }
        }

        public List<Character> GetCharactersThisDay(int day)
        {
            CharactersThisDay = new List<Character>();
            foreach (Character character in _characterList)
            {
                Debug.Log(character.Data.DaysComingData.ContainsKey(day));
                if (character.Data.DaysComingData.ContainsKey(day))
                {
                    character.ComingState = ComingState.Coming;
                    CharactersThisDay.Add(character);
                    Debug.Log("Add " + character.Data.Name);
                }
            }
            return CharactersThisDay;
        }

        public void ResetCharactersDrunkState(int value)
        {
            foreach (Character character in _characterList)
            {
                if(!character.IsDead) character.ResetDrunkState();
            }
            RemoveAllCharacters();
        }

        public void CheckWhoIsComing(int currentDay, int interactions)
        {
            bool changed = false;
            bool b = false;
            bool c = false;
            foreach (Character character in CharactersThisDay)
            {
                if (character.ComingState == ComingState.Coming)
                {
                    //TODO FAIRE VIENDRE LE PERSO
                    if(character.CheckComingAtDay(currentDay, interactions))
                    {
                        character.ComingState = ComingState.Here;
                        changed = true;
                    }
                    
                    b = true;
                    c = true;
                }
            }
            if (!changed && c)  // Si il reste des perso derriere mais plus personne au bar
            {
                Debug.LogWarning("Call force endDay test 0");

                if (GetNextCharacter(currentDay, interactions) != null) changed = true;
                else
                {
                    /*
                    if (DayManager.Instance != null)
                    {
                        Debug.LogWarning("Call force endDay");
                        DayManager.Instance.EndDay();
                    }
                    return;
                    */
                }
            }

            if (changed) BringCharacters();
        }

        public Character GetNextCharacter(int currentDay, int interactions)
        {
            Character chosenCharacter = null;
            int minInteraction = 1000;
            foreach(Character character in CharactersThisDay)
            {
                if(character.ComingState != ComingState.Coming) continue;
                if ((character.Data.DaysComingData[currentDay].InteractionsBeforeComing - interactions) < minInteraction )
                {
                    chosenCharacter = character;
                    minInteraction = character.Data.DaysComingData[currentDay].InteractionsBeforeComing - interactions;

                }
            }
            if (chosenCharacter != null)
            {
                DayInteractions newDayInteraction = new DayInteractions();
                newDayInteraction.InteractionsBeforeComing = 0;
                newDayInteraction.InteractionsBeforeLeaving = chosenCharacter.Data.DaysComingData[currentDay].InteractionsBeforeLeaving;
                chosenCharacter.Data.DaysComingData[currentDay] = newDayInteraction;
            }
            return chosenCharacter;
        }
        public void CheckWhoIsLeaving(int currentDay, int interactions)
        {
            bool changed = false;
            bool b = false;
            foreach (Character character in CharactersThisDay)
            {
                if (character.CheckLeavingAtDay(currentDay, interactions) && character.ComingState == ComingState.Here)
                {
                    //TODO FAIRE PLUS VIENDRE LE PERSO
                    character.ComingState = ComingState.Leaving;
                    changed = true;

                }
                if (character.ComingState == ComingState.Here || character.ComingState == ComingState.Coming)
                {
                    Debug.LogWarning("c'est vrai !");
                    b = true;
                }
            }

            if (!b)
            {
                if (DayManager.Instance != null)
                {
                    Debug.LogWarning("Call force endDay");
                    DayManager.Instance.EndDay();
                }
            }

            if (changed) RemoveCharacters();
        }

        public void BringCharacters()
        {
            foreach (Character character in CharactersThisDay)
            {
                if (character.ComingState == ComingState.Here)
                {
                    character.Coming();

                }
            }
        }
        public void RemoveCharacters()
        {
            foreach (Character character in CharactersThisDay)
            {
                if (character.ComingState == ComingState.Leaving)
                {
                    character.Leaving();
                }
            }
        }

        public void RemoveAllCharacters()
        {
            foreach (Character character in _characterList)
            {
                if (character.ComingState != ComingState.Left)
                {
                    character.Leaving();
                }
            }
        }
    }   
}
