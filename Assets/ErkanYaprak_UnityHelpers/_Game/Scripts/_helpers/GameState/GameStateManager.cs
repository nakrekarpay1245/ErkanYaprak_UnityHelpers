using UnityEngine;

namespace _Game._helpers.GameState
{
    /// <summary>
    /// Manages the current state of the game and notifies listeners when the game state changes.
    /// Follows the Singleton design pattern to ensure only one instance exists.
    /// </summary>
    public class GameStateManager : MonoBehaviour
    {
        /// <summary>
        /// The single instance of GameStateManager (Singleton pattern).
        /// </summary>
        public static GameStateManager Instance { get; private set; }

        /// <summary>
        /// Event triggered whenever the game state changes.
        /// </summary>
        public event System.Action<GameState> OnGameStateChanged;

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        public GameState CurrentGameState { get; private set; } = GameState.Gameplay;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes the Singleton instance and ensures only one instance exists.
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Ensures there's only one instance
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }


        /// <summary>
        /// Sets the new game state and invokes the OnGameStateChanged event if the state changes.
        /// </summary>
        /// <param name="newGameState">The new game state to be set.</param>
        public void SetState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;
            CurrentGameState = newGameState;
            Debug.Log(newGameState.ToString());
            OnGameStateChanged?.Invoke(newGameState);
        }
    }
}