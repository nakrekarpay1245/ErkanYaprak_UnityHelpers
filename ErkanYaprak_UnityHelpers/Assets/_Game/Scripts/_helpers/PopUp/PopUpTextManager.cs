using UnityEngine;
using System.Collections;

namespace _Game.Scripts._helpers.PopUp
{
    /// <summary>
    /// Manages a pool of PopUpText instances and controls their animation and display.
    /// </summary>
    public class PopUpTextManager : MonoBehaviour
    {
        [Header("Pop Up Text Manager Settings")]
        [Tooltip("Prefab for the PopUpText.")]
        [SerializeField] private PopUpText _popUpTextPrefab;

        [Tooltip("Size of the object pool.")]
        [SerializeField, Range(1, 50)] private int _poolSize = 10;

        [Tooltip("Randomizes the position of the pop-up text within this range.")]
        [SerializeField, Range(0f, 1f)] private float _positionRandomizer = 0.1f;

        [Tooltip("Vertical offset applied to the pop-up text when animating.")]
        [SerializeField, Range(0f, 5f)] private float _verticalPositionOffset = 0.25f;

        [Tooltip("Scale applied to the pop-up text during the animation.")]
        [SerializeField] private Vector3 _popUpTextScale = Vector3.one;

        private ObjectPool<PopUpText> _popUpTextPool;

        [Header("Animation Settings")]
        [Tooltip("Duration for the pop-up scaling animation.")]
        [SerializeField, Range(0.1f, 2f)] private float _animationDuration = 0.25f;

        [Tooltip("Time to wait before starting to hide the pop-up text.")]
        [SerializeField, Range(0f, 2f)] private float _pauseDuration = 0.5f;

        [Tooltip("Delay before hiding the pop-up text.")]
        [SerializeField, Range(0f, 1f)] private float _hideDelay = 0.125f;

        private void Awake()
        {
            InitializeObjectPool();
        }

        /// <summary>
        /// Initializes the object pool with the specified size.
        /// </summary>
        private void InitializeObjectPool()
        {
            _popUpTextPool = new ObjectPool<PopUpText>(InstantiatePopUpText, _poolSize);
        }

        /// <summary>
        /// Instantiates a new PopUpText instance and adds it to the pool.
        /// </summary>
        /// <returns>A new PopUpText instance.</returns>
        private PopUpText InstantiatePopUpText()
        {
            PopUpText popUpTextInstance = Instantiate(_popUpTextPrefab, transform);
            popUpTextInstance.gameObject.SetActive(false);
            return popUpTextInstance;
        }

        /// <summary>
        /// Displays a pop-up text at the specified position with optional custom duration.
        /// </summary>
        /// <param name="position">The position where the pop-up text will appear.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="duration">The duration before the pop-up text is hidden.</param>
        public void ShowPopUpText(Vector3 position, string text, float duration = 0.25f)
        {
            PopUpText popUpText = _popUpTextPool.GetObject();
            SetPopUpTextPosition(popUpText, position);
            popUpText.SetText(text);

            popUpText.gameObject.SetActive(true);
            AnimatePopUpText(popUpText, duration);
        }

        /// <summary>
        /// Sets the position of the pop-up text with a randomized offset.
        /// </summary>
        /// <param name="popUpText">The PopUpText instance.</param>
        /// <param name="position">The target position.</param>
        private void SetPopUpTextPosition(PopUpText popUpText, Vector3 position)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-_positionRandomizer, _positionRandomizer),
                Random.Range(-_positionRandomizer, _positionRandomizer),
                0f);

            popUpText.transform.position = position + randomOffset;
        }

        /// <summary>
        /// Animates the pop-up text and hides it after the specified duration.
        /// </summary>
        /// <param name="popUpText">The PopUpText instance.</param>
        /// <param name="duration">The duration before hiding the pop-up text.</param>
        private void AnimatePopUpText(PopUpText popUpText, float duration)
        {
            Vector3 endPosition = popUpText.transform.position + Vector3.up * _verticalPositionOffset;
            StartCoroutine(PopUpAnimationCoroutine(popUpText, endPosition, duration));
        }

        /// <summary>
        /// Coroutine that handles the pop-up text animation, pausing, moving, shrinking, and hiding.
        /// </summary>
        /// <param name="popUpText">The PopUpText instance.</param>
        /// <param name="endPosition">The target position after the animation.</param>
        /// <param name="duration">The duration before hiding the pop-up text.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private IEnumerator PopUpAnimationCoroutine(PopUpText popUpText, Vector3 endPosition, float duration)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = popUpText.transform.position;
            Vector3 initialScale = Vector3.zero;

            // Scale up and move the pop-up text to the final position
            while (elapsedTime < _animationDuration)
            {
                float t = elapsedTime / _animationDuration;
                popUpText.transform.localScale = Vector3.Lerp(initialScale, _popUpTextScale, t);
                popUpText.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            popUpText.transform.localScale = _popUpTextScale;
            popUpText.transform.position = endPosition;

            // Pause for the specified duration
            yield return new WaitForSeconds(_pauseDuration);

            // Wait for the delay before starting the hide animation
            yield return new WaitForSeconds(_hideDelay);

            elapsedTime = 0f;
            Vector3 moveUpPosition = endPosition + Vector3.up * _verticalPositionOffset;

            // First move the pop-up text upwards
            while (elapsedTime < _animationDuration / 2)  // Half of the animation duration for moving up
            {
                float t = elapsedTime / (_animationDuration / 2);
                popUpText.transform.position = Vector3.Lerp(endPosition, moveUpPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            popUpText.transform.position = moveUpPosition;

            elapsedTime = 0f;

            // Then scale down and fade out the pop-up text
            while (elapsedTime < _animationDuration / 2)  // The remaining half for scaling down
            {
                float t = elapsedTime / (_animationDuration / 2);
                popUpText.transform.localScale = Vector3.Lerp(_popUpTextScale, Vector3.zero, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Hide and return the pop-up text to the pool
            popUpText.transform.localScale = Vector3.zero;
            popUpText.gameObject.SetActive(false);
            _popUpTextPool.ReturnObject(popUpText);
        }
    }
}