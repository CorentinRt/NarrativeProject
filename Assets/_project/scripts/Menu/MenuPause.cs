using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class MenuPause : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        public void Open() => _pauseMenu.SetActive(true);
        public void Close() => _pauseMenu.SetActive(false);
    }
}
