using CREMOT.DialogSystem;
using UnityEngine;

namespace NarrativeProject
{
    public class Character : MonoBehaviour
    {
        [VisibleInDebug] SO_CharacterData _data;
        [VisibleInDebug] SpriteRenderer _spriteRenderer;
        [VisibleInDebug] DrunkState _state;
        [VisibleInDebug] FriendshipState _friendshipState;
        [VisibleInDebug] int _inComingDays, _drunkScale, _friendshipScale;
        [VisibleInDebug] bool _isDead;

        public int InComingDays { get => _inComingDays; }

        public void Init()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _data.Sprites[0];
            SetDrunkState();
            SetFriendShipState();
            _isDead = false;
        }
        public void DecrementInComingDays() => _inComingDays--;

        public void Die() => _isDead = true;


        public DrunkState Drink(DrinkType drink)
        {
            switch (drink)
            {
                case DrinkType.Rhum_Puissance_2:
                    _drunkScale += _data.DrinkEffects[drink];
                    _friendshipScale += _data.DrinkEffectsFriendShip[drink];
                    break;
                case DrinkType.Whisky_Puissance_2:
                    _drunkScale += _data.DrinkEffects[drink];
                    _friendshipScale += _data.DrinkEffectsFriendShip[drink];
                    break;
                case DrinkType.Cofee:
                    _drunkScale -= _data.DrinkEffects[drink];
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
            DialogueInventory.Instance.AddItem(_data.Name + "_" + _state + "_" + _friendshipState, 1);
        }
    }
}

