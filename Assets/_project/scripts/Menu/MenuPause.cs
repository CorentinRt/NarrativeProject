using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class MenuPause : MonoBehaviour
    {

        public UnityEvent OnPause;
        [SerializeField] private GameObject _pauseMenu;
        Vector3 _defaultPos;
        Vector3 _defaultScale;
        Quaternion _defaultRot;
        private void Start()
        {
            _defaultPos = new Vector3(_pauseMenu.transform.position.x, _pauseMenu.transform.position.y, _pauseMenu.transform.position.z);
            _defaultScale = new Vector3(_pauseMenu.transform.localScale.x, _pauseMenu.transform.localScale.y, _pauseMenu.transform.localScale.z);
            _defaultRot = new Quaternion(_pauseMenu.transform.rotation.x, _pauseMenu.transform.rotation.y, _pauseMenu.transform.rotation.z, _pauseMenu.transform.rotation.w);

        }
        public void Open()
        {
            _pauseMenu.SetActive(true);
            _pauseMenu.transform.position = _defaultPos;
            _pauseMenu.transform.rotation = _defaultRot;
            _pauseMenu.transform.localScale = _defaultScale;
            OnPause?.Invoke();
        }

        public void Close()
        {
            _pauseMenu.SetActive(false);

        }
    }
}
