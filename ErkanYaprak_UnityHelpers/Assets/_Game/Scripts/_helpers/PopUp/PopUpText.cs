using TMPro;
using UnityEngine;
using System.Collections;

namespace _Game.Scripts._helpers.PopUp
{
    /// <summary>
    /// Manages pop-up text, including setting the text and animating its appearance using coroutines.
    /// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class PopUpText : MonoBehaviour
    {
        [Header("Animation Settings")]
        [Tooltip("Duration of the pop-up scaling animation.")]
        [SerializeField, Range(0.1f, 2f)]
        private float animationDuration = 0.5f;

        [Header("Scale Settings")]
        [Tooltip("The multiplier for the initial scale of the pop-up.")]
        [SerializeField, Range(0.1f, 3f)]
        private float scaleMultiplier = 1f;

        private TextMeshPro _textComponent;
        private Vector3 _initialScale;
        private Coroutine _scalingCoroutine;

        private void Awake()
        {
            CacheComponents();
            InitializeScale();
        }

        private void OnEnable()
        {
            ResetScale();
            StartPopUpAnimation();
        }

        /// <summary>
        /// Caches the required component references.
        /// </summary>
        private void CacheComponents()
        {
            _textComponent = GetComponent<TextMeshPro>();
        }

        /// <summary>
        /// Initializes the starting scale of the pop-up based on the configured multiplier.
        /// </summary>
        private void InitializeScale()
        {
            _initialScale = Vector3.one * scaleMultiplier;
        }

        /// <summary>
        /// Resets the pop-up's scale to zero in preparation for animation.
        /// </summary>
        private void ResetScale()
        {
            transform.localScale = Vector3.zero;
        }

        /// <summary>
        /// Starts the scaling animation for the pop-up.
        /// </summary>
        private void StartPopUpAnimation()
        {
            if (_scalingCoroutine != null)
            {
                StopCoroutine(_scalingCoroutine);
            }

            _scalingCoroutine = StartCoroutine(ScaleCoroutine(_initialScale, animationDuration));
        }

        /// <summary>
        /// Smoothly scales the pop-up to the target scale over the specified duration.
        /// </summary>
        /// <param name="targetScale">The final scale to reach.</param>
        /// <param name="duration">The duration of the scaling animation.</param>
        /// <returns>An enumerator for coroutine execution.</returns>
        private IEnumerator ScaleCoroutine(Vector3 targetScale, float duration)
        {
            float elapsedTime = 0f;
            Vector3 startingScale = transform.localScale;

            while (elapsedTime < duration)
            {
                transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = targetScale;
        }

        /// <summary>
        /// Sets the text content of the pop-up.
        /// </summary>
        /// <param name="text">The text to display in the pop-up.</param>
        public void SetText(string text)
        {
            if (_textComponent == null)
            {
                Debug.LogWarning("TextMeshPro component is not assigned.");
                return;
            }

            _textComponent.text = text;
        }
    }
}