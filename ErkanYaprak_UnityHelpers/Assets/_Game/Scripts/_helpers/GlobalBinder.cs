using _Game.Scripts._helpers.Audios;
using _Game.Scripts._helpers.Particles;
using _Game.Scripts._helpers.TimeManagement;
using UnityEngine;

namespace _Game.Scripts._helpers
{
    /// <summary>
    /// Acts as a central hub for accessing various managers in the game.
    /// Inherits from MonoSingleton to ensure a single instance across the game.
    /// </summary>
    public class GlobalBinder : MonoSingleton<GlobalBinder>
    {
        [Header("Managers")]
        [Tooltip("Handles audio functionalities like playing sounds and managing music.")]
        [SerializeField] private AudioManager _audioManager;

        [Tooltip("Manages particle effects used throughout the game.")]
        [SerializeField] private ParticleManager _particleManager;

        [Tooltip("Handles time management including countdowns, timers, and related functions.")]
        [SerializeField] private TimeManager _timeManager;

        /// <summary>
        /// Provides public access to the AudioManager instance.
        /// </summary>
        public AudioManager AudioManager => _audioManager;

        /// <summary>
        /// Provides public access to the ParticleManager instance.
        /// </summary>
        public ParticleManager ParticleManager => _particleManager;

        /// <summary>
        /// Provides public access to the TimeManager instance.
        /// </summary>
        public TimeManager TimeManager => _timeManager;
    }
}