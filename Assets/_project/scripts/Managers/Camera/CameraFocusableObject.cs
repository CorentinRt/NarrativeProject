using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public class CameraFocusableObject : MonoBehaviour, ICameraFocusable
    {
        public bool CanFocus()
        {
            return true;    // test en attendant mais verif avec Player Input manager
        }

        public void Focus()
        {
            
        }
    }
}
