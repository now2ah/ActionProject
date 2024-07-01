using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum eBGM
        {

        }
        
        public enum eSfx
        {

        }

        AudioClip[] _bgmClip;
        AudioClip[] _sfxClip;

        AudioSource _bgmAudioSource;
        AudioSource[] _sfxAudioSources;
        int _channelCount;
        int _channelIndex;
        float _bgmVolume;
        float _sfxVolume;
        
        public override void Initialize()
        {
            base.Initialize();

        }

        public void PlayBGM(eBGM clip)
        {
            if (!_bgmAudioSource.isPlaying || _bgmAudioSource.clip != _bgmClip[(int)clip])
                _bgmAudioSource.Play();
        }

        public void PlaySFX(eSfx clip)
        {
            for (int i=0; i<_sfxAudioSources.Length; i++)
            {
                int loopIndex = (i + _channelIndex) % _sfxAudioSources.Length;

                if (_sfxAudioSources[loopIndex].isPlaying)
                    continue;

                _channelIndex = loopIndex;

                _sfxAudioSources[loopIndex].clip = _sfxClip[(int)clip];
                _sfxAudioSources[loopIndex].Play();
                break;
            }
        }

        void Awake()
        {
            _bgmAudioSource = gameObject.AddComponent<AudioSource>();
            _bgmAudioSource.playOnAwake = false;
            _bgmAudioSource.loop = true;
            _bgmAudioSource.volume = _bgmVolume;

            _sfxAudioSources = new AudioSource[_channelCount];
            for (int i=0; i< _channelCount; i++)
            {
                _sfxAudioSources[i] = gameObject.AddComponent<AudioSource>();
                _sfxAudioSources[i].playOnAwake = false;
                _sfxAudioSources[i].loop = false;
                _sfxAudioSources[i].volume = _sfxVolume;
            }

            _channelCount = 8;
            _channelIndex = 0;
            _bgmVolume = 0.3f;
            _sfxVolume = 1.0f;
        }
    }
}

