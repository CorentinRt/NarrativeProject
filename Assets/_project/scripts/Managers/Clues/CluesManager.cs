using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class CluesManager : MonoBehaviour
    {
        private static CluesManager _instance;

        public event Action<string, int, string> OnAddClue;
        public UnityEvent OnAddClueUnity;

        public static CluesManager Instance { get => _instance; set => _instance = value; }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        public void AddClue(string characterName, int key, string clue)
        {
            OnAddClue?.Invoke(characterName, key, clue);
            OnAddClueUnity?.Invoke();
            SaveManager.SaveClue(characterName, key, clue);
        }

        [Button]
        public void AddClueTest()
        {
            AddClue("Linda", 0, " Bah c'est une pute");
        }
    }
}
