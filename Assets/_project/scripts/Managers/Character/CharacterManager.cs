using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class CharacterManager : MonoBehaviour
    {
        static CharacterManager _instance;

        [SerializeField] List<Character> _characterList;

        [SerializeField] private bool _autoFillCharacterListOnStart;

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
                /*DayManager.Instance.OnUpdateCurrentInteractionCount -= CheckWhoIsComing;
                DayManager.Instance.OnUpdateCurrentInteractionCount -= CheckWhoIsLeaving;*/
                DayManager.Instance.OnEndDay -= ResetCharactersDrunkState;
            }
        }

        public void InitManager()
        {
            if (_characterList == null)
            {
                Debug.LogWarning("Character List is missing in Character Manager");
            }

            if (_autoFillCharacterListOnStart)
            {
                Character[] tempAllChara = GameObject.FindObjectsOfType<Character>();

                _characterList.Clear();
                foreach (var character in tempAllChara)
                {
                    if (character ==  null) continue;
                    _characterList.Add(character);
                }
            }

            foreach(Character character in _characterList)
            {
                character.Init();
            }
            if (DayManager.Instance != null)
            {
                /*DayManager.Instance.OnUpdateCurrentInteractionCount += CheckWhoIsComing;
                DayManager.Instance.OnUpdateCurrentInteractionCount += CheckWhoIsLeaving;*/
                DayManager.Instance.OnEndDay += ResetCharactersDrunkState;
            }
        }

        /*public List<Character> GetCharactersThisDay(int day)
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
        }*/

        public void ResetCharactersDrunkState(int value)
        {
            foreach (Character character in _characterList)
            {
                if(!character.IsDead) character.ResetDrunkState();
            }
            RemoveAllCharacters();
        }

        /*public void CheckWhoIsComing(int currentDay, int interactions)
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
                    
                    c = true;
                }
                if (character.ComingState == ComingState.Here && character.Data.DaysComingData[currentDay].InteractionsBeforeLeaving != interactions)  // Check si y a au moins une personne au bar
                {
                    b = true;
                }
            }
            if (!changed && c && !b)  // Si il reste des perso derriere mais plus personne au bar
            {
                //Debug.LogWarning("Call force endDay test 0");

                var tempChara = GetNextCharacter(currentDay, interactions);

                if (tempChara != null)
                {
                    changed = true;
                    tempChara.ComingState = ComingState.Here;
                    //Debug.LogWarning("Call force endDay test 1 : " + tempChara.name);
                }
            }

            if (changed) BringCharacters();
        }*/

        /*public Character GetNextCharacter(int currentDay, int interactions)
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
        }*/
        /*public void CheckWhoIsLeaving(int currentDay, int interactions)
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
                    //Debug.LogWarning("c'est vrai !");
                    b = true;
                }
            }

            if (!b)
            {
                if (DayManager.Instance != null)
                {
                    //Debug.LogWarning("Call force endDay");
                    DayManager.Instance.EndDay();
                }
            }

            if (changed) RemoveCharacters();
        }*/

        /*public void BringCharacters()
        {
            foreach (Character character in CharactersThisDay)
            {
                if (character.ComingState == ComingState.Here)
                {
                    character.Coming();

                }
            }
        }*/
        /*public void RemoveCharacters()
        {
            foreach (Character character in CharactersThisDay)
            {
                if (character.ComingState == ComingState.Leaving)
                {
                    character.Leaving();
                }
            }
        }*/

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
