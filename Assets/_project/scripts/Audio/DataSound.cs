using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    [CreateAssetMenu(fileName = "DataSoundSingle", menuName = "ScriptableObjects/Sound/DataSoundSingle", order = 2)]
    [System.Serializable]
    public class DataSound : ScriptableObject
    {
        public AudioClip clip;
        [Range(-50, 50)]
        public float volumeScale = 1;
    }
}
