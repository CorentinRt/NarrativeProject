using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NarrativeProject
{
    public class SoundManager : MonoBehaviour
    {
        #region Fields
        private static SoundManager _instance;


        [Header("Audio Sources")]
        [SerializeField] private AudioSource _sourceSFX;
        [SerializeField] private AudioSource _sourceMUS;
        [SerializeField] private AudioSource _sourceVOC;

        [SerializeField] private AudioMixer _mixer;

        #endregion

        #region Properties
        public static SoundManager Instance { get => _instance; set => _instance = value; }

        #endregion

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        public void InitManager()
        {

        }

        #region Play Sound Geneeric

        private void PlaySFXSound(AudioClip clip, float volumeScale = 0f)
        {
            if (clip == null)   return;
            if (_sourceSFX == null) return;

            _sourceSFX.PlayOneShot(clip, volumeScale);
        }
        private void PlayVOCSound(AudioClip clip, float volumeScale = 0f)
        {
            if (clip == null) return;
            if (_sourceSFX == null) return;

            _sourceVOC.PlayOneShot(clip, volumeScale);
        }
        private void PlayMUSSound(AudioClip clip, float volumeScale = 0f)
        {
            if (clip == null) return;
            if (_sourceSFX == null) return;

            _sourceMUS.PlayOneShot(clip, volumeScale);
        }

        #endregion


        #region Volume

        #region Get Volume
        public float GetMasterVolume()
        {
            if (_mixer == null) return 0f;

            _mixer.GetFloat("MasterVolume", out float tempVolume);

            return tempVolume;
        }
        public float GetSFXVolume()
        {
            if (_mixer == null) return 0f;

            _mixer.GetFloat("SFXVolume", out float tempVolume);

            return tempVolume;
        }
        public float GetVOCVolume()
        {
            if (_mixer == null) return 0f;

            _mixer.GetFloat("VOCVolume", out float tempVolume);

            return tempVolume;
        }
        public float GetMUSVolume()
        {
            if (_mixer == null) return 0f;

            _mixer.GetFloat("MUSVolume", out float tempVolume);

            return tempVolume;
        }
        #endregion

        #region Set Volume
        public void SetMasterVolume(float volume)
        {
            if (_mixer == null) return;

            _mixer.SetFloat("MasterVolume", volume);
        }
        public void SetSFXVolume(float volume)
        {
            if (_mixer == null) return;

            _mixer.SetFloat("SFXVolume", volume);
        }
        public void SetMUSVolume(float volume)
        {
            if (_mixer == null) return;

            _mixer.SetFloat("SFXVolume", volume);
        }
        public void SetVOCVolume(float volume)
        {
            if (_mixer == null) return;

            _mixer.SetFloat("SFXVolume", volume);
        }
        #endregion

        #endregion
    }
}
