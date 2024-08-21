using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts._helpers.GameState
{
    public class GameStateTest : MonoBehaviour
    {
        [Header("PauseController Params")]
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private Button _resumeButton;
        [SerializeField]
        private Image _pauseMenu;

        [SerializeField]
        private GameStateManager _gameStateManager;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(() =>
            {
                PauseGame();
            });

            _resumeButton.onClick.AddListener(() =>
            {
                ResumeGame();
            });
        }

        public void PauseGame()
        {
            _pauseMenu.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);

            GameState newGameState = GameState.Pause;
            _gameStateManager.SetState(newGameState);
        }

        public void ResumeGame()
        {
            _pauseMenu.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(true);

            GameState newGameState = GameState.Gameplay;
            _gameStateManager.SetState(newGameState);
        }
    }
}