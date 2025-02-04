using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class PositionPaper : MonoBehaviour
    {
        enum Page
        {
            MainMenu,
            Settings,
            Credits
        }
        [SerializeField] private GameObject Menu;
        private Vector3 CenterScreen;
        private float distanceBetweenPage;
        [SerializeField] private Page pageLoadOnStart = Page.MainMenu;

        private Tween _changePosTween;
        [SerializeField] private float _slideDuration;

        private void Start()
        {
            distanceBetweenPage = Screen.height*1.2f;
            CenterScreen = Menu.transform.position;
            SetPotistionPaper(pageLoadOnStart.ToString());
        }

        public void ButtonBack()
        {
            SetPotistionPaper("MainMenu");
        }

        public void ButtonSettings()
        {
            SetPotistionPaper("Settings");
        }

        public void ButtonCredits()
        {
            SetPotistionPaper("Credits");
        }

        void SetPotistionPaper(string PositionPaper)
        {
            if (_changePosTween != null)
            {
                _changePosTween.Kill();
            }

            switch (PositionPaper)
            {
                case "MainMenu":
                {
                        //gameObject.transform.position =  Camera.main.transform.position + new Vector3(CenterScreen.x,CenterScreen.y - distanceBetweenPage, 0);
                        _changePosTween = transform.DOMove(Camera.main.transform.position + new Vector3(CenterScreen.x, CenterScreen.y - distanceBetweenPage, 0), _slideDuration);
                    break;
                }
                case "Settings":
                {
                        //gameObject.transform.position = Camera.main.transform.position + new Vector3(CenterScreen.x, CenterScreen.y, 0);
                        _changePosTween = transform.DOMove(Camera.main.transform.position + new Vector3(CenterScreen.x, CenterScreen.y, 0), _slideDuration);

                    break; 
                }
                case "Credits":
                {
                        //gameObject.transform.position = Camera.main.transform.position + new Vector3(CenterScreen.x, CenterScreen.y +distanceBetweenPage, 0);
                        _changePosTween = transform.DOMove(Camera.main.transform.position + new Vector3(CenterScreen.x, CenterScreen.y + distanceBetweenPage, 0), _slideDuration);

                    break;
                }
            }
        }
    }
}
