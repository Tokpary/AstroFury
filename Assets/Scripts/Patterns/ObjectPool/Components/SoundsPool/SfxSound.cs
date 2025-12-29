using System;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Patterns.ObjectPool.Components
{
    public class SfxSound : MonoBehaviour, IPooleableObject
    {
        [SerializeField] private AudioSource _audioSource;


        private void Update()
        {
            if (!_audioSource.isPlaying)
            {
                elementPool?.Release(this);
            }
        }

        public void PlaySfx(AudioClip audioClip)
        {
            if (_audioSource.clip != null)
            {
                
                if(_audioSource.isPlaying)
                    _audioSource.Stop();
            }
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
        

        public IPooleableObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            SfxSound sfxSoundSource = clone.GetComponent<SfxSound>();
            return sfxSoundSource;
        }

        public bool Active
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }
        public IObjectPool elementPool { get; set; }
        public void Reset()
        {
            _audioSource.Stop();
        }
    }
}