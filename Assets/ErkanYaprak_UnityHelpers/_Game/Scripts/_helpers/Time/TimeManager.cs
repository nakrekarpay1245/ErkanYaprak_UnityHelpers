using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace _Game._helpers.TimeManagement
{
    /// <summary>
    /// Manages the game timer, including starting, stopping, adding extra time, and freezing the timer.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        [Header("TimeManager Parameters")]
        [SerializeField, Tooltip("Initial time for the level in seconds.")]
        private float _levelTime = 180f;

        [SerializeField, Tooltip("The time threshold considered critical (e.g., low time warning).")]
        private float _criticalTimeThreshold = 15f;

        [SerializeField, Tooltip("Time interval for updating the timer.")]
        private float _updateInterval = 15f;

        private float _currentLevelTime;
        private bool _isTimerRunning;
        private Coroutine _freezeCoroutine;

        public UnityAction<float, float> OnTimerUpdated; // Event triggered when the timer is updated
        public UnityAction OnTimeFinished; // Event triggered when the time runs out

        private void Start()
        {
            StartTimer(_levelTime);
        }

        /// <summary>
        /// Starts the timer with a specified duration.
        /// </summary>
        /// <param name="timeInSeconds">The time to start the timer with, in seconds.</param>
        public void StartTimer(float timeInSeconds)
        {
            _currentLevelTime = timeInSeconds;
            _isTimerRunning = true;

            OnTimerUpdated?.Invoke(_currentLevelTime, _criticalTimeThreshold);

            ScheduleTimerUpdate();
        }

        /// <summary>
        /// Schedules the timer update at regular intervals.
        /// </summary>
        private void ScheduleTimerUpdate()
        {
            InvokeRepeating(nameof(UpdateTimer), _updateInterval, _updateInterval);
        }

        /// <summary>
        /// Updates the timer, reducing the remaining time and handling timer completion.
        /// </summary>
        private void UpdateTimer()
        {
            if (!_isTimerRunning) return;

            _currentLevelTime -= _updateInterval;

            if (_currentLevelTime <= 0)
            {
                HandleTimeExpired();
            }
            else
            {
                OnTimerUpdated?.Invoke(_currentLevelTime, _criticalTimeThreshold);
            }
        }

        /// <summary>
        /// Handles the scenario when the time has expired.
        /// </summary>
        private void HandleTimeExpired()
        {
            _currentLevelTime = 0;
            _isTimerRunning = false;
            CancelInvoke(nameof(UpdateTimer));
            OnTimeFinished?.Invoke();
        }

        /// <summary>
        /// Adds extra time to the current timer.
        /// </summary>
        /// <param name="extraTimeInSeconds">The additional time to add, in seconds.</param>
        public void AddExtraTime(float extraTimeInSeconds)
        {
            _currentLevelTime += extraTimeInSeconds;

            if (!_isTimerRunning)
            {
                ResumeTimer();
            }

            OnTimerUpdated?.Invoke(_currentLevelTime, _criticalTimeThreshold);
        }

        /// <summary>
        /// Resumes the timer if it was not running.
        /// </summary>
        private void ResumeTimer()
        {
            _isTimerRunning = true;
            ScheduleTimerUpdate();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            _isTimerRunning = false;
            CancelInvoke(nameof(UpdateTimer));
        }

        /// <summary>
        /// Resets the timer to the initial level time.
        /// </summary>
        public void ResetTimer()
        {
            StopTimer();
            StartTimer(_levelTime);
        }

        /// <summary>
        /// Sets the time scale, controlling the flow of time in the game.
        /// </summary>
        /// <param name="scale">The scale at which time passes. 1 is normal speed.</param>
        public void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        /// <summary>
        /// Freezes the timer for a specified duration. After the duration, the timer resumes.
        /// </summary>
        /// <param name="duration">The duration for which to freeze the timer, in seconds.</param>
        public void FreezeTimer(float duration)
        {
            if (!_isTimerRunning || _freezeCoroutine != null) return;

            StopTimer(); // Pause the timer

            _freezeCoroutine = StartCoroutine(ResumeTimerAfterDelay(duration));
        }

        /// <summary>
        /// Coroutine that resumes the timer after a specified delay.
        /// </summary>
        /// <param name="duration">The delay duration in seconds.</param>
        private IEnumerator ResumeTimerAfterDelay(float duration)
        {
            yield return new WaitForSeconds(duration);

            if (_currentLevelTime > 0)
            {
                ResumeTimer();
            }

            _freezeCoroutine = null;
        }
    }
}