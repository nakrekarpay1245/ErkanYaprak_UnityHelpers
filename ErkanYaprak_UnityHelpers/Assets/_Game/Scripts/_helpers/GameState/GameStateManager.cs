using UnityEngine;

namespace _Game.Scripts._helpers
{
    /// <summary>
    /// Manages the current state of the game and notifies listeners when the game state changes.
    /// Follows the Singleton design pattern to ensure only one instance exists.
    /// </summary>
    public class GameStateManager : MonoBehaviour
    {
        /// <summary>
        /// Event triggered whenever the game state changes.
        /// </summary>
        public event System.Action<GameState> OnGameStateChanged;

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        public GameState CurrentGameState { get; private set; } = GameState.Gameplay; 

        /// <summary>
        /// Private constructor to prevent external instantiation (Singleton Pattern).
        /// </summary>
        private GameStateManager() { }

        /// <summary>
        /// Sets the new game state and invokes the OnGameStateChanged event if the state changes.
        /// </summary>
        /// <param name="newGameState">The new game state to be set.</param>
        public void SetState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;

            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }
    }
}