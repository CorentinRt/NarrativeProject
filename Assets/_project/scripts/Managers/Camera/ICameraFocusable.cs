using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public interface ICameraFocusable
    {
        public abstract bool CanFocus();

        public abstract void Focus();
    }
}
