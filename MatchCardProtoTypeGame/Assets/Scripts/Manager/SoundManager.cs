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

        [Header("Sound Effects")]
        [SerializeField] private AudioClip _flipClip;
        [SerializeField] private AudioClip _matchClip;
        [SerializeField] private AudioClip _mismatchClip;
        [SerializeField] private AudioClip _gameOverClip;
        [SerializeField] private AudioClip _gamewinClip;

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
            _sfxSource.PlayOneShot(clip);
        }
    }
}
