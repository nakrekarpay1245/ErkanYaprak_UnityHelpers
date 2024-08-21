using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace _Game.Scripts._helpers.Audios
{
    /// <summary>
    /// Manages the playing of audio clips using a pool of AudioSources.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Manager Parameters")]
        [Tooltip("List of audio configurations.")]
        [SerializeField]
        private List<Audio> _audioList = new List<Audio>();

        [Tooltip("Maximum number of AudioSource components to manage.")]
        [SerializeField, Range(1, 20)]
        private int _maximumAudioCount = 10;

        [Tooltip("Master volume for all audio.")]
        [SerializeField, Range(0f, 1f)]
        private float _masterVolume = 1f;

        [Tooltip("Mute all audio sources.")]
        [SerializeField]
        private bool _isAudioSourceMuted = false;

        [Header("Audio Mixer")]
        [Tooltip("Audio mixer group for sound effects.")]
        [SerializeField]
        private AudioMixerGroup _soundMixerGroup;

        private readonly List<AudioSource> _audioSources = new List<AudioSource>();

        private void Awake()
        {
            InitializeAudioSources();
        }

        /// <summary>
        /// Initializes the AudioSource components.
        /// </summary>
        private void InitializeAudioSources()
        {
            for (int i = 0; i < _maximumAudioCount; i++)
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.outputAudioMixerGroup = _soundMixerGroup;
                _audioSources.Add(newSource);
            }
        }

        /// <summary>
        /// Plays an audio clip by name with specified volume and loop settings.
        /// </summary>
        /// <param name="clipName">The name of the audio clip to play.</param>
        /// <param name="volume">Volume level for this instance.</param>
        /// <param name="loop">Whether the audio should loop.</param>
        public void PlaySound(string clipName, float volume = 1f, bool loop = false)
        {
            Audio audio = GetAudioByName(clipName);
            if (audio == null) return;

            AudioSource source = GetAvailableAudioSource();
            if (source == null)
            {
                Debug.LogWarning("No available AudioSource to play sound.");
                return;
            }

            ConfigureAndPlayAudioSource(source, audio, volume, loop);
        }

        /// <summary>
        /// Plays an audio clip by name with default settings.
        /// </summary>
        /// <param name="clipName">The name of the audio clip to play.</param>
        public void PlaySound(string clipName)
        {
            PlaySound(clipName, 1f, false);
        }

        /// <summary>
        /// Plays a specific audio clip with specified volume and loop settings.
        /// </summary>
        /// <param name="clip">The AudioClip to play.</param>
        /// <param name="volume">Volume level for this instance.</param>
        /// <param name="loop">Whether the audio should loop.</param>
        public void PlaySound(AudioClip clip, float volume = 1f, bool loop = false)
        {
            AudioSource source = GetAvailableAudioSource();
            if (source == null)
            {
                Debug.LogWarning("No available AudioSource to play sound.");
                return;
            }

            ConfigureAndPlayAudioSource(source, clip, volume, loop);
        }

        /// <summary>
        /// Retrieves an audio configuration by name.
        /// </summary>
        /// <param name="clipName">The name of the audio clip.</param>
        /// <returns>The Audio object with the specified name, or null if not found.</returns>
        private Audio GetAudioByName(string clipName)
        {
            return _audioList.Find(audio => audio.Name == clipName);
        }

        /// <summary>
        /// Retrieves the first available AudioSource that is not currently playing.
        /// </summary>
        /// <returns>An available AudioSource, or null if none are available.</returns>
        private AudioSource GetAvailableAudioSource()
        {
            return _audioSources.Find(source => !source.isPlaying);
        }

        /// <summary>
        /// Configures an AudioSource and plays the specified audio.
        /// </summary>
        /// <param name="source">The AudioSource to configure.</param>
        /// <param name="audio">The Audio object containing the clip and settings.</param>
        /// <param name="volume">The volume level to apply.</param>
        /// <param name="loop">Whether the audio should loop.</param>
        private void ConfigureAndPlayAudioSource(AudioSource source, Audio audio, float volume, bool loop)
        {
            source.clip = audio.Clip;
            source.volume = _masterVolume * volume * audio.Volume;
            source.pitch = audio.Pitch;
            source.loop = loop;
            source.mute = _isAudioSourceMuted;
            source.Play();
        }

        /// <summary>
        /// Configures an AudioSource and plays the specified AudioClip.
        /// </summary>
        /// <param name="source">The AudioSource to configure.</param>
        /// <param name="clip">The AudioClip to play.</param>
        /// <param name="volume">The volume level to apply.</param>
        /// <param name="loop">Whether the audio should loop.</param>
        private void ConfigureAndPlayAudioSource(AudioSource source, AudioClip clip, float volume, bool loop)
        {
            source.clip = clip;
            source.volume = _masterVolume * volume;
            source.loop = loop;
            source.mute = _isAudioSourceMuted;
            source.Play();
        }
    }
}