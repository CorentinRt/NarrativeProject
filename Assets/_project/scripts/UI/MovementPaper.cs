using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class MovementPaper : MonoBehaviour
    {
        private void Start()
        {
            SetPotistionPaper("Credits");
        }

        void SetPotistionPaper(string PositionPaper)
        {
            switch (PositionPaper)
            {
                case "MainMenu":
                {
                    gameObject.transform.position = gameObject.transform.position + gameObject.transform.GetChild(0).position + new Vector3(100,-180,0);
                        break;
                }
                case "Settings":
                {
                    gameObject.transform.position += new Vector3(0, -50,0);
                    break; 
                }
                case "Credits":
                {
                    gameObject.transform.position = gameObject.transform.position - gameObject.transform.GetChild(2).position;
                    break;
                }
            }
        }
    }
}
