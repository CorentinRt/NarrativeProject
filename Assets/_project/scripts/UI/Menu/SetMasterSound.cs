using NarrativeProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMasterSound : MonoBehaviour
{
    [SerializeField]Slider soundSlider;

    private void Start()
    {
        soundSlider.value = SoundManager.Instance.GetMasterVolume();
        soundSlider.onValueChanged.AddListener(UpdateValue);
    }

    void UpdateValue(float value)
    {
        SoundManager.Instance.SetMasterVolume(value);
    }
}
