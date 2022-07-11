using UnityEngine;

namespace Infrastructure.Services
{
    public class SoundService
    {
        private AudioSource _audioSource;
        private AudioClip[] _audioClips = new AudioClip[0];
        
        public SoundService(AudioSource audioSource, AudioClip[] audioClips)
        {
            _audioSource = audioSource;
            _audioClips = audioClips;
        }

        public void PlayMainTheme()
        {
            _audioSource.clip = _audioClips[(int)SoundsId.MainTheme];
            _audioSource.Play();
        }
    }
    
    public enum SoundsId
    {
        MainTheme = 0
    }
}