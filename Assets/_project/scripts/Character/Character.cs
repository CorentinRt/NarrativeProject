using CREMOT.DialogSystem;
using DG.Tweening;
using JetBrains.Annotations;
using NaughtyAttributes;
using System;
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
        [SerializeField] GameObject _visual, Collision;
        [SerializeField] DrunkState _state;
        [SerializeField] FriendshipState _friendshipState;
        [SerializeField] int _drunkScale, _friendshipScale;
        [SerializeField] bool _isDead;
        [SerializeField] ComingState _comingState;

        [Space(20)]

        [Header("Darken / Light Up parameters")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _darkenTargetColor;
        [SerializeField] private float _darkenColorDuration;
        private Tween _darkenTween;

        public event Action<Character> OnCharacterComing;
        public event Action<Character> OnCharacterLeaving;

        public UnityEvent OnCharacterComingUnity = new UnityEvent();
        public UnityEvent OnCharacterLeavingUnity = new UnityEvent();

        public SO_CharacterData Data { get => _data; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public ComingState ComingState { get => _comingState; set => _comingState = value; }


        #region Darken / light up

        [Button]
        public void DarkenCharacter()
        {
            if (_darkenTween != null)
            {
                _darkenTween.Kill();
            }

            _darkenTween = _spriteRenderer.DOColor(_darkenTargetColor, _darkenColorDuration);
        }
        [Button]
        public void LightUpCharacter()
        {
            if (_darkenTween != null)
            {
                _darkenTween.Kill();
            }

            _darkenTween = _spriteRenderer.DOColor(Color.white, _darkenColorDuration);
        }

        #endregion

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
            /*for (int i = 0; i < _data.DaysComing.Count; i++)
            {
                //Debug.Log("checking");
                if (!_data.DaysComingData.ContainsKey(_data.DaysComing[i]))
                {
                    //Debug.Log("add eleemtn");
                    _data.DaysComingData.Add(_data.DaysComing[i], _data.InteractionsData[i]);
                }
            }*/
            _visual.GetComponent<SpriteRenderer>().sprite = Data.Sprites[0];
            _friendshipScale = Data.DefaultFriendShipScale;
            _drunkScale = Data.DefaultdrunkScale;
            SetDrunkState();
            SetFriendShipState();
            IsDead = false;
            Collision.GetComponentInChildren<DropZone>().OnReceiveDrop += ReceiveDrop;
        }

        public void ResetDrunkState()
        {
            _drunkScale = Data.DefaultdrunkScale;
            SetDrunkState();
        }

        private void OnDestroy()
        {
            Collision.GetComponentInChildren<DropZone>().OnReceiveDrop -= ReceiveDrop;
        }



        private void ReceiveDrop(GameObject obj)
        {
            Drink _drink = obj.GetComponent<Drink>();
            if (_drink != null)
            {
                Drink(_drink.DrinkType);

                ReactToState();
                Debug.Log("Drink " + _drink.DrinkType + "Reaction :" + _state + " " + _friendshipState);
            }
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

            if (DayManager.Instance != null)
            {
                DayManager.Instance.IncrementCurrentInteractionCount();
            }

            SetFriendShipState();
            return SetDrunkState();
        }

        public DrunkState SetDrunkState()
        {
            if(_drunkScale <= 30)
            {
                _state = DrunkState.Clean;
                return _state;
            }
            else if (_drunkScale > 30 && _drunkScale <= 60)
            {
                _state = DrunkState.Dizzy;
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
            if (_friendshipScale <= 30)
            {
                _friendshipState = FriendshipState.Sad;
                return _friendshipState;
            }
            else if (_friendshipScale > 30 && _friendshipScale <= 60)
            {
                _friendshipState = FriendshipState.Neutral;
                return _friendshipState;
            }
            else
            {
                _friendshipState = FriendshipState.Happy;
                return _friendshipState;
            }
        }

        public void ReactToState()
        {
            DialogueInventory.Instance.AddItem(Data.Name + "_" + _state + "_" + _friendshipState, 1);
        }
        [Button]
        public void Coming()
        {
            OnCharacterComing?.Invoke(this);
            OnCharacterComingUnity?.Invoke();

        }

        [Button]
        public void Leaving()
        {
            OnCharacterLeaving?.Invoke(this);
            OnCharacterLeavingUnity?.Invoke();
            //_comingState = ComingState.Left;
        }

       //public bool CheckComingAtDay(int day, int currentInteractionCount) => _data.DaysComingData[day].InteractionsBeforeComing <= currentInteractionCount;
       //public bool CheckLeavingAtDay(int day, int currentInteractionCount) => _data.DaysComingData[day].InteractionsBeforeLeaving <= currentInteractionCount;
    }
}

