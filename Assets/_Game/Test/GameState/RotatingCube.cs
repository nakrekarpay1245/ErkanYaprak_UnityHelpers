using UnityEngine;

namespace _Game._helpers.GameState
{
    /// <summary>
    /// Rotates the cube in a random direction at a specified speed.
    /// Rotation stops when the game is paused or stopped, and resumes when the game is unpaused.
    /// </summary>
    public class RotatingCube : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [Tooltip("The speed at which the cube rotates.")]
        [SerializeField, Range(1f, 10000f)]
        private float _rotationSpeed = 2000f;

        [Tooltip("The random rotation axis generated on start.")]
        [SerializeField]
        private Vector3 _rotationAxis = Vector3.one;

        private bool _isRotating = true;
        /// <summary>
        /// Called when the script instance is being loaded.
        /// Generates a random rotation axis.
        /// </summary>
        private void Awake()
        {
            _rotationAxis = Random.onUnitSphere; // Generates a random direction vector.
        }

        /// <summary>
        /// Subscribes to the GameStateManager's OnGameStateChanged event.
        /// </summary>
        private void Start()
        {
            GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        }

        /// <summary>
        /// Unsubscribes from the GameStateManager's OnGameStateChanged event.
        /// </summary>
        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }

        /// <summary>
        /// Handles the rotation of the cube based on the game state.
        /// </summary>
        private void Update()
        {
            if (_isRotating)
            {
                RotateCube();
            }
        }

        /// <summary>
        /// Rotates the cube around the generated random axis at the specified speed.
        /// </summary>
        private void RotateCube()
        {
            transform.Rotate(_rotationAxis * _rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Handles the game state change and controls whether the cube should rotate or not.
        /// </summary>
        /// <param name="newGameState">The new game state provided by the GameStateManager.</param>
        private void HandleGameStateChanged(GameState newGameState)
        {
            switch (newGameState)
            {
                case GameState.Gameplay:
                    _isRotating = true;
                    break;
                case GameState.Pause:
                    _isRotating = false;
                    break;
            }
        }
    }
}