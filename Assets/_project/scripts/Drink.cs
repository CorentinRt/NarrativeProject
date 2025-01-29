using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class Drink : MonoBehaviour
    {
        [SerializeField] DrinkType _drinkType;

        public DrinkType DrinkType { get => _drinkType; set => _drinkType = value; }
    }
}
