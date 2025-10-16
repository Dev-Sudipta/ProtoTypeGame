using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchCard
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _bgmSource;
        [Header("Sound Effects")]
        [SerializeField] private AudioClip _flipClip;
        [SerializeField] private AudioClip _matchClip;
        [SerializeField] private AudioClip _mismatchClip;
        [SerializeField] private AudioClip _gameOverClip;
        [SerializeField] private AudioClip _gamewinClip;
        [Header("Background Music")]
        [SerializeField] private AudioClip _bgMusic;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayFlipSound() => PlayClip(_flipClip);
        public void PlayMatchSound() => PlayClip(_matchClip);
        public void PlayMismatchSound() => PlayClip(_mismatchClip);
        public void PlayGameOverSound() => PlayClip(_gameOverClip);
        public void PlayGameWinSound() => PlayClip(_gamewinClip);

        private void PlayClip(AudioClip clip)
        {
            if (clip == null) return;

           
            if (_sfxSource.isPlaying)
                _sfxSource.Stop();

            _sfxSource.clip = clip;
            _sfxSource.volume = 0.9f;
            _sfxSource.Play();
        }
        public void PlayBackgroundMusic()
        {
            if (_bgMusic == null || _bgmSource == null) return;

            _bgmSource.clip = _bgMusic;
            _bgmSource.loop = true;
            _bgmSource.volume = 0.5f; // softer than SFX
            _bgmSource.Play();
        }

        public void PauseBackgroundMusic()
        {
            if (_bgmSource.isPlaying)
                _bgmSource.Pause();
        }
    }
}
