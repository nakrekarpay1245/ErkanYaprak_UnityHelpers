using UnityEngine;

namespace _Game.Scripts._helpers.Audios
{
    /// <summary>
    /// A script to test audio playback functionality with the AudioManager.
    /// This script manages audio playback for different clips based on user input.
    /// </summary>
    public class AudioTestScript : MonoBehaviour
    {
        [Header("Audio Manager Reference")]
        [Tooltip("Reference to the AudioManager that handles audio playback.")]
        [SerializeField]
        private AudioManager _audioManager;

        [Header("Audio Clip Keys")]
        [Tooltip("Key for the 'pop' audio clip.")]
        [SerializeField]
        private string _popClipKey = "pop";
        [Tooltip("KeyCode for the play 'pop' sound")]
        [SerializeField]
        private KeyCode _popClipKeyCode = KeyCode.LeftControl;
        [Space]
        [Tooltip("Key for the 'poof' audio clip.")]
        [SerializeField]
        private string _poofClipKey = "poof";
        [Tooltip("KeyCode for the play 'poof' sound")]
        [SerializeField]
        private KeyCode _poofClipKeyCode = KeyCode.LeftShift;

        [Space]
        [Tooltip("Key for the 'click' audio clip.")]
        [SerializeField]
        private string _clickClipKey = "click";
        [Tooltip("KeyCodes for the play 'click' sound")]
        private KeyCode[] _playbackKeys = new KeyCode[]
        {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
        KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
        KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
        KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
        KeyCode.Z
        };


        private void Update()
        {
            HandleAudioInput();
        }

        /// <summary>
        /// Handles user input for audio playback.
        /// </summary>
        private void HandleAudioInput()
        {
            if (Input.GetKeyDown(_popClipKeyCode))
            {
                PlayAudioClip(_popClipKey);
            }

            if (Input.GetKeyDown(_poofClipKeyCode))
            {
                PlayAudioClip(_poofClipKey);
            }

            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in _playbackKeys)
                {
                    if (Input.GetKeyDown(key))
                    {
                        PlayAudioClip(_clickClipKey);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Plays the audio clip associated with the given key.
        /// </summary>
        /// <param name="clipKey">The key for the audio clip to play.</param>
        private void PlayAudioClip(string clipKey)
        {
            if (_audioManager != null)
            {
                _audioManager.PlaySound(clipKey);
            }
            else
            {
                Debug.LogWarning("AudioManager reference is not set.");
            }
        }
    }
}