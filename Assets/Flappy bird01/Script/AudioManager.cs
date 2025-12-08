using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlappyBird
{
    public enum Audio
    {
        BGMusic,
        Die,
        Hit,
        Score,
        Swoosh,
        WingFlap
    }

    [System.Serializable]
    public class FlappyAudio
    {
        public Audio AudioType;
        public AudioClip Clip;
        public float Volume = 1f;
        public bool Loop = false;
    }
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance
        {
            get; private set;
        }

        [SerializeField] private List<FlappyAudio> _audioClips = new List<FlappyAudio>();
        private List<AudioSource> _availableAudioSources = new List<AudioSource>();

        private void Awake()
        {
            Instance = this;
            _availableAudioSources = GetComponentsInChildren<AudioSource>().ToList();
        }

        public void PlayAudio(Audio audio)
        {
            
            int flappyClipIndex = _audioClips.FindIndex(o => o.AudioType == audio);
            if (flappyClipIndex == -1)
                return;

            FlappyAudio currentAudioClip = _audioClips[flappyClipIndex];
            foreach (AudioSource source in _availableAudioSources)
            {
                if (!source.isPlaying)
                {
                    source.volume = currentAudioClip.Volume;
                    source.loop = currentAudioClip.Loop;
                    source.clip = currentAudioClip.Clip;
                    source.Play();
                    return;
                }
            }
        }
    }
}