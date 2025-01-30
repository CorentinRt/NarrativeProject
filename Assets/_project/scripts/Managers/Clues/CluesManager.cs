using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class CluesManager : MonoBehaviour
    {
        CluesManager _instance;

        public event Action<string, string, string> OnAddClue;
        public UnityEvent OnAddClueUnity;

        public CluesManager Instance { get => _instance; set => _instance = value; }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        public void AddClue(string characterName, string key, string clue)
        {
            OnAddClue?.Invoke(characterName, key, clue);
            OnAddClueUnity?.Invoke();
        }
    }
}
