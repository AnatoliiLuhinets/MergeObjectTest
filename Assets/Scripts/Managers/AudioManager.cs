using Common;
using UnityEngine;

namespace Managers
{
    public class AudioManager
    {
        private AudioSource _audioSource;
        
        public void UpdateAudioSource()
        {
            if (PlayerPrefs.HasKey(Consts.Audio.AudioState))
            {
                _audioSource.mute = PlayerPrefsExtensions.GetBool(Consts.Audio.AudioState);
            }
            
            if(PlayerPrefsExtensions.HasKey(Consts.Audio.Volume))
            {
                _audioSource.volume = (PlayerPrefsExtensions.GetFloat(Consts.Audio.Volume));
            }
        }

        public void SetAudioSource(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void SetAudioState(bool state)
        {
            _audioSource.mute = state;
            
            PlayerPrefsExtensions.SetBool(Consts.Audio.AudioState, state);
        }

        public bool GetAudioState()
        {
            return _audioSource.mute;
        }

        public void SetVolume(float value)
        {
            _audioSource.volume = value;

            PlayerPrefsExtensions.SetFloat(Consts.Audio.Volume, value);
        }
    }
}
