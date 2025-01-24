using UnityEngine;

namespace NarrativeProject
{
    public class Character : MonoBehaviour
    {
        [VisibleInDebug] SO_CharacterData _data;
        [VisibleInDebug] SpriteRenderer _spriteRenderer;
        [VisibleInDebug] DrunkState _state;
        [VisibleInDebug] int _inComingDays;
        [VisibleInDebug] bool _isDead;

        void Init()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _data.Sprites[0];
            _state = _data.DefaultState;
            _inComingDays = 0;
            _isDead = false;
        }

        bool CheckDrink(DrinkType drink) => _data.Prefs.Contains(drink);

        void DecrementInComingDays() => _inComingDays--;

        void Die() => _isDead = true;


        DrunkState Drink(DrinkType drink)
        {
            if (CheckDrink(drink))
            {
                switch (drink)
                {
                    case DrinkType.Vin_Puissance_1:
                        _state = DrunkState.Arrache;
                        break;
                    case DrinkType.Biere_Puissance_2:
                        _state = DrunkState.Clean;
                        break;
                    case DrinkType.Whisky_Puissance_3:
                        _state = DrunkState.Coma;
                        break;
                    case DrinkType.PierreChabrier_Puissance_4:
                        _state = DrunkState.ComaEthylique;
                        break;
                    case DrinkType.LeComteDeMentheEtCristaux_Puissance_5:
                        _state = DrunkState.ComaEthylique;
                        break;
                }
            }
            return _state;
        }

        void ReactToState()
        {

        }
    }
}

