using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts._helpers.Particles
{
    /// <summary>
    /// Stores data for a specific particle type, including its name, count, and associated particle systems.
    /// </summary>
    [System.Serializable]
    public class ParticleData
    {
        [Header("Particle Settings")]
        [Tooltip("The name of the particle effect.")]
        public string ParticleName;

        [Tooltip("The number of particles to be generated.")]
        public int ParticleCount;

        [Tooltip("The list of particle system prefabs.")]
        [SerializeField]
        private List<ParticleSystem> _particleSystemList;

        /// <summary>
        /// Gets a random particle system from the list of particle systems.
        /// </summary>
        public ParticleSystem ParticleSystem => _particleSystemList[Random.Range(0, _particleSystemList.Count)];

        /// <summary>
        /// Initializes a new instance of the ParticleData class.
        /// </summary>
        /// <param name="particleSystems">The list of particle system prefabs.</param>
        /// <param name="count">The number of particles to be generated.</param>
        /// <param name="name">The name of the particle effect.</param>
        public ParticleData(List<ParticleSystem> particleSystems, int count, string name)
        {
            _particleSystemList = particleSystems ?? throw new System.ArgumentNullException(nameof(particleSystems));
            ParticleCount = count > 0 ? count : throw new System.ArgumentOutOfRangeException(nameof(count), "Particle count must be greater than zero.");
            ParticleName = name ?? throw new System.ArgumentNullException(nameof(name));
        }
    }
}