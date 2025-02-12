using NarrativeProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonGeneratedSoundLink : MonoBehaviour
{
    public void PlayBtnClickSound()
    {
        if (SoundManager.Instance == null)  return;

        SoundManager.Instance.PlayBtnClickSingle();
    }
}
