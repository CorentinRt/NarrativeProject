using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        [Header("Others")]
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] public CreateSoundData _dataSound;

        [SerializeField] private DataSound _btnClickSingle;


        [SerializeField] private DataSound _nextPage;

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

        
        public void PlayNextPage()
        {
            if (_nextPage == null)  return;

            PlaySoundSFX(_nextPage);
        }

        #region Play Sound Generic

        private void PlaySFXSound(AudioClip clip, float volumeScale = 0f)
        {
            if (clip == null)   return;
            if (_sourceSFX == null) return;

            _sourceSFX.PlayOneShot(clip, volumeScale);
        }
        private void PlayVOCSound(AudioClip clip, float volumeScale = 0f)
        {
            if (clip == null) return;
            if (_sourceVOC == null) return;

            _sourceVOC.PlayOneShot(clip, volumeScale);
        }
        private void PlayMUSSound(AudioClip clip, float volumeScale = 0f)
        {
            if (clip == null) return;
            if (_sourceMUS == null) return;

            _sourceMUS.clip = clip;
            _sourceMUS.Play();
        }

        #endregion


        public void PlaySoundSFX(UnityEngine.Object obj)
        {
            DataSound sound = null;

            if (obj is DataSound so)
            {
                sound = so;
                //Debug.Log("ScriptableObject re�u : " + so.name);
            }
            else
            {
                Debug.LogWarning("L'objet fourni n'est pas un ScriptableObject valide !");
            }

            if (sound == null) return;

            PlaySFXSound(sound.clip, sound.volumeScale + _sourceSFX.volume);
        }
        public void PlaySoundMUS(UnityEngine.Object obj)
        {
            DataSound sound = null;

            if (obj is DataSound so)
            {
                sound = so;
                //Debug.Log("ScriptableObject re�u : " + so.name);
            }
            else
            {
                Debug.LogWarning("L'objet fourni n'est pas un ScriptableObject valide !");
            }

            if (sound == null) return;

            PlayMUSSound(sound.clip, sound.volumeScale + _sourceMUS.volume);
        }
        public void PlaySoundVOC(UnityEngine.Object obj)
        {
            DataSound sound = null;

            if (obj is DataSound so)
            {
                sound = so;
                //Debug.Log("ScriptableObject re�u : " + so.name);
            }
            else
            {
                Debug.LogWarning("L'objet fourni n'est pas un ScriptableObject valide !");
            }

            if (sound == null) return;

            PlayVOCSound(sound.clip, sound.volumeScale + _sourceVOC.volume);
        }

        public void FadeOutMusic()
        {
            if (_sourceMUS ==  null) return;

            _sourceMUS.DOFade(0f, 2f);
        }
        public void FadeInMusic()
        {
            if (_sourceMUS == null) return;

            _sourceMUS.DOFade(0.5f, 1f);
        }
        public void SwitchMusic(DataSound sound)
        {
            if (_sourceMUS == null) return;

            _sourceMUS.DOFade(0f, 2f).OnComplete(() => { PlaySoundMUS(sound); FadeInMusic(); });
        }

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

        public void PlayBtnClickSingle()
        {
            if (!_btnClickSingle) return;

            PlaySoundSFX(_btnClickSingle);
        }
    }
}
