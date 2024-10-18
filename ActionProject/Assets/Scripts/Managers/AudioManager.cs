using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.Util;


namespace Action.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum eBGM
        {
            GAMEOVER,
            MAIN,
            TOWN,
            HUNT
        }
      
        public enum eSfx
        {
            ARROW,
            BUILDDONE,
            BUILDHIT,
            CLICK,
            COINPICKUP,
            COLLAPSE,
            DASH,
            DEFENSEDONE,
            EXPPICKUP,
            FIRE,
            LEVELUP,
            POWERUP,
            SLASH,
            TOHUNT
        }

        AudioClip[] _bgmClip;
        AudioClip[] _sfxClip;

        AudioSource _bgmAudioSource;
        AudioSource[] _sfxAudioSources;
        int _channelCount;
        int _channelIndex;
        float _bgmVolume;
        float _sfxVolume;

        public UnityEvent onBgmVolumeChanged;
        public UnityEvent onSfxVolumeChanged;

        public float BGMVolume { get { return _bgmVolume; } set { _bgmVolume = value; } }
        public float SFXVolume { get { return _sfxVolume; } set { _sfxVolume = value; } }
      
        void _LoadAssets()
        {
            _bgmClip[0] = Resources.Load("Audio/Bgm/GameOver") as AudioClip;
            _bgmClip[1] = Resources.Load("Audio/Bgm/Main") as AudioClip;
            _bgmClip[2] = Resources.Load("Audio/Bgm/Town") as AudioClip;
            _bgmClip[3] = Resources.Load("Audio/Bgm/Hunt") as AudioClip;

            _sfxClip[0] = Resources.Load("Audio/Sfx/Arrow") as AudioClip;
            _sfxClip[1] = Resources.Load("Audio/Sfx/BuildDone") as AudioClip;
            _sfxClip[2] = Resources.Load("Audio/Sfx/BuildHit") as AudioClip;
            _sfxClip[3] = Resources.Load("Audio/Sfx/Click") as AudioClip;
            _sfxClip[4] = Resources.Load("Audio/Sfx/CoinPickUp") as AudioClip;
            _sfxClip[5] = Resources.Load("Audio/Sfx/Collapse") as AudioClip;
            _sfxClip[6] = Resources.Load("Audio/Sfx/Dash") as AudioClip;
            _sfxClip[7] = Resources.Load("Audio/Sfx/DefenseDone") as AudioClip;
            _sfxClip[8] = Resources.Load("Audio/Sfx/ExpPickUp") as AudioClip;
            _sfxClip[9] = Resources.Load("Audio/Sfx/Fire") as AudioClip;
            _sfxClip[10] = Resources.Load("Audio/Sfx/GameOver") as AudioClip;
            _sfxClip[11] = Resources.Load("Audio/Sfx/LevelUp") as AudioClip;
            _sfxClip[12] = Resources.Load("Audio/Sfx/PowerUp") as AudioClip;
            _sfxClip[13] = Resources.Load("Audio/Sfx/Slash") as AudioClip;
            _sfxClip[14] = Resources.Load("Audio/Sfx/ToHunt") as AudioClip;
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        public void PlayBGM(eBGM clip)
        {
            if (!_bgmAudioSource.isPlaying || _bgmAudioSource.clip != _bgmClip[(int)clip])
            {
                _bgmAudioSource.clip = _bgmClip[(int)clip];
                _bgmAudioSource.Play();
            }
                
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

        void _ChangeBgmVolume()
        {
            _bgmAudioSource.volume = _bgmVolume;
        }

        void _ChangeSfxVolume()
        {
            for (int i = 0; i < _channelCount; i++)
                _sfxAudioSources[i].volume = _sfxVolume;
        }

        void Awake()
        {
            _channelCount = 8;
            _channelIndex = 0;
            _bgmVolume = 0.5f;
            _sfxVolume = 1.0f;
            _bgmClip = new AudioClip[4];
            _sfxClip = new AudioClip[15];
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

            onBgmVolumeChanged = new UnityEvent();
            onSfxVolumeChanged = new UnityEvent();
            onBgmVolumeChanged.AddListener(_ChangeBgmVolume);
            onSfxVolumeChanged.AddListener(_ChangeSfxVolume);

            _LoadAssets();
        }

    }
}

