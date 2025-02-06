using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    [CreateAssetMenu(fileName = "DataSound", menuName = "ScriptableObjects/Sound/DataSound", order = 2)]
    public class CreateSoundData : ScriptableObject
    {
        [SerializeField] public DataSound[] soundClips;
    }
}
