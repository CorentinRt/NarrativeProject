using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    [System.Serializable]
    public class DataSound
    {
        public AudioClip clip;
        [Range(-50, 50)]
        public int volumeScale = 0;
    }
}
