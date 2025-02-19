using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NarrativeProject
{
    public class ScenesManager : MonoBehaviour
    {
        #region Fields
        private ScenesManager _instance;

        [SerializeField] private string _menuScene;
        [SerializeField] private string _gameScene;

        #endregion

        #region Properties
        public ScenesManager Instance { get => _instance; set => _instance = value; }


        #endregion


        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(_instance);
            }
            _instance = this;
        }

        #region Open Scene
        public void OpenMenuScene()
        {
            DOTween.Clear();
            SceneManager.LoadScene(_menuScene);
        }
        public void OpenGameScene()
        {
            DOTween.Clear();
            SceneManager.LoadScene(_gameScene);
        }

        public void ReloadCurrentScene()
        {
            DOTween.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion


        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
