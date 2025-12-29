using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Systems.SoundSystem
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioClip[] BGMList;
        public AudioClip[] SFXList;
        [SerializeField] private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
        [SerializeField] private Dictionary<string, AudioClip> musics = new Dictionary<string, AudioClip>();
        
        private MusicPlayer _musicPlayer;
        private SfxPlayer _sfxPlayer;
            

        private void Start()
        {
            for (int i = 0; i < SFXList.Length; i++)
            {
                AudioClip sound = SFXList[i];
                sounds.Add(sound.name, sound);
            }
            for (int i = 0; i < BGMList.Length; i++)
            {
                AudioClip music = BGMList[i];
                musics.Add(music.name, music);
            }

            _musicPlayer = GetComponentInChildren<MusicPlayer>();
            _sfxPlayer = GetComponentInChildren<SfxPlayer>();
            PlayMusic("MenuMusic");
        }

        public void PlaySfx(string name)
        {
            _sfxPlayer.PlaySfx(sounds[name]);
        }

        public void PlayMusic(string name)
        {
            Debug.Log("Playing music: " + name);
            Debug.Log("Music: " + musics[name]);
            Debug.Log("Music player: " + _musicPlayer);
            _musicPlayer.PlayMusic(musics[name]);
        }

        public void TogglePauseMusic()
        {
            _musicPlayer.TogglePauseMusic();
        }
        
        public void StopMusic()
        {
            _musicPlayer.StopMusic();
        }
        
    }
}