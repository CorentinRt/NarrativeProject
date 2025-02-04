using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class StartGame : MonoBehaviour
    {
        public void ButtonStart()
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
