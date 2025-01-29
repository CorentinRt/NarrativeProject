using CREMOT.DialogSystem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

namespace NarrativeProject
{

    public enum ComingState
    {
        Coming,
        Here,
        Leaving,
        Left
    };
    public class Character : MonoBehaviour
    {
        [SerializeField] SO_CharacterData _data;
        [SerializeField] GameObject _visual;
        [SerializeField] DrunkState _state;
        [SerializeField] FriendshipState _friendshipState;
        [SerializeField] int _drunkScale, _friendshipScale;
        [SerializeField] bool _isDead;
        [SerializeField] ComingState _comingState;

        public UnityEvent _onCharacterComing = new UnityEvent();
        public UnityEvent _onCharacterLeaving = new UnityEvent();

        public SO_CharacterData Data { get => _data; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public ComingState ComingState { get => _comingState; set => _comingState = value; }

        public void Init()
        {
            for (int i = 0; i < _data.DrinkType.Count; i++)
            {
                if (!_data.DrinkEffects.ContainsKey(_data.DrinkType[i]))
                {
                    _data.DrinkEffects.Add(_data.DrinkType[i], _data.DrinkEffect[i]);
                    _data.DrinkEffectsFriendShip.Add(_data.DrinkType[i], _data.DrinkEffectFriendShip[i]);
                    Debug.Log("Added " + _data.DrinkType[i] + " to the dictionary at " + _data.DrinkEffect[i]);
                }
            }
            for (int i = 0; i < _data.DaysComing.Count; i++)
            {
                Debug.Log("checking");
                if (!_data.DaysComingData.ContainsKey(_data.DaysComing[i]))
                {
                    Debug.Log("add eleemtn");
                    _data.DaysComingData.Add(_data.DaysComing[i], _data.InteractionsData[i]);
                }
            }
            _visual.GetComponent<SpriteRenderer>().sprite = Data.Sprites[0];
            SetDrunkState();
            SetFriendShipState();
            IsDead = false;
        }

        public void Die() => IsDead = true;


        public DrunkState Drink(DrinkType drink)
        {
            switch (drink)
            {
                case DrinkType.Rhum_Puissance_2:
                    _drunkScale += Data.DrinkEffects[drink];
                    _friendshipScale += Data.DrinkEffectsFriendShip[drink];
                    break;
                case DrinkType.Whisky_Puissance_2:
                    _drunkScale += Data.DrinkEffects[drink];
                    _friendshipScale += Data.DrinkEffectsFriendShip[drink];
                    break;
                case DrinkType.Cofee:
                    _drunkScale -= Data.DrinkEffects[drink];
                    //_friendshipScale += 2;
                    break;
            }
            return SetDrunkState();
        }

        public DrunkState SetDrunkState()
        {
            if(_drunkScale <= 3)
            {
                _state = DrunkState.Clean;
                return _state;
            }
            else if (_drunkScale > 3 && _drunkScale <= 6)
            {
                _state = DrunkState.Happy;
                return _state;
            }
            else
            {
                _state = DrunkState.Drunk;
                return _state;
            }
        }
        public FriendshipState SetFriendShipState()
        {
            if (_friendshipScale <= 3)
            {
                _state = DrunkState.Clean;
                return _friendshipState;
            }
            else if (_friendshipScale > 3 && _friendshipScale <= 6)
            {
                _state = DrunkState.Happy;
                return _friendshipState;
            }
            else
            {
                _state = DrunkState.Drunk;
                return _friendshipState;
            }
        }

        public void ReactToState()
        {
            DialogueInventory.Instance.AddItem(Data.Name + "_" + _state + "_" + _friendshipState, 1);
        }
        public void Coming()
        {
            _onCharacterComing?.Invoke();

        }

        public void Leaving()
        {
            _onCharacterLeaving?.Invoke();
            _comingState = ComingState.Left;
        }

        public bool CheckComingAtDay(int day, int currentInteractionCount) => _data.DaysComingData[day].InteractionsBeforeComing <= currentInteractionCount;
        public bool CheckLeavingAtDay(int day, int currentInteractionCount) => _data.DaysComingData[day].InteractionsBeforeLeaving <= currentInteractionCount;
    }
}

