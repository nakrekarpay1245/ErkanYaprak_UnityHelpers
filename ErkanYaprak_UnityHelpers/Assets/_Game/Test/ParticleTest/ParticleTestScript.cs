using UnityEngine;

namespace _Game.Scripts._helpers.Particles
{
    public class AudioTestScript : MonoBehaviour
    {
        [Header("Particle Manager Reference")]
        [Tooltip("Reference to the ParticleManager component responsible for managing and playing particle effects.")]
        [SerializeField]
        private ParticleManager _particleManager;

        [Header("Particle System Keys")]
        [Tooltip("Key for the 'pop' particle system.")]
        [SerializeField]
        private string _popParticleKey = "pop";
        [Tooltip("KeyCode to trigger the 'pop' particle effect.")]
        [SerializeField]
        private KeyCode _popParticleKeyCode = KeyCode.LeftControl;
        [Space]
        [Tooltip("Key for the 'poof' particle system.")]
        [SerializeField]
        private string _poofParticleKey = "poof";
        [Tooltip("KeyCode to trigger the 'poof' particle effect.")]
        [SerializeField]
        private KeyCode _poofParticleKeyCode = KeyCode.LeftShift;

        private void Update()
        {
            HandleParticleInput();
        }

        /// <summary>
        /// Handles user input to play particle effects based on configured key codes.
        /// </summary>
        private void HandleParticleInput()
        {
            if (Input.GetKeyDown(_popParticleKeyCode))
            {
                PlayParticle(_popParticleKey);
            }

            if (Input.GetKeyDown(_poofParticleKeyCode))
            {
                PlayParticle(_poofParticleKey);
            }
        }

        /// <summary>
        /// Plays the particle system associated with the given key at the origin (Vector3.zero).
        /// </summary>
        /// <param name="particleKey">The key for the particle system to play.</param>
        private void PlayParticle(string particleKey)
        {
            if (_particleManager != null)
            {
                _particleManager.PlayParticleAtPoint(particleKey, Vector3.zero);
            }
            else
            {
                Debug.LogWarning("ParticleManager reference is not set.");
            }
        }
    }
}