using UnityEngine;

namespace _Game._helpers.Particles
{
    public class AudioTestScript : MonoBehaviour
    {
        [Header("Particle Manager Reference")]
        [Tooltip("Reference to the ParticleManager component responsible for managing and playing particle effects.")]
        [SerializeField]
        private ParticleManager _particleManager;

        [Header("Particle System Keys")]
        [Tooltip("Key for the 'fire' particle system.")]
        [SerializeField]
        private string _fireParticleKey = "fire";
        [Tooltip("KeyCode to trigger the 'fire' particle effect.")]
        [SerializeField]
        private KeyCode _fireParticleKeyCode = KeyCode.LeftControl;
        [Space]
        [Tooltip("Key for the 'smoke' particle system.")]
        [SerializeField]
        private string _smokeParticleKey = "smoke";
        [Tooltip("KeyCode to trigger the 'smoke' particle effect.")]
        [SerializeField]
        private KeyCode _smokeParticleKeyCode = KeyCode.LeftShift;

        private void Update()
        {
            HandleParticleInput();
        }

        /// <summary>
        /// Handles user input to play particle effects based on configured key codes.
        /// </summary>
        private void HandleParticleInput()
        {
            if (Input.GetKeyDown(_fireParticleKeyCode))
            {
                PlayParticle(_fireParticleKey);
            }

            if (Input.GetKeyDown(_smokeParticleKeyCode))
            {
                PlayParticle(_smokeParticleKey);
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