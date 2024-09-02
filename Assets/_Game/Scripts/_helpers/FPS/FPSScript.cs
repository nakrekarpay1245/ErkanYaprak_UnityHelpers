using TMPro;
using UnityEngine;

namespace _Game._helpers.FPS
{
    /// <summary>
    /// FPSScript is responsible for calculating and displaying the frames per second (FPS) in a UI TextMeshPro element.
    /// It uses a smoothing algorithm to avoid large fluctuations in FPS display.
    /// </summary>
    public class FPSScript : MonoBehaviour
    {
        [Header("UI Component")]
        [Tooltip("TextMeshProUGUI component to display the FPS.")]
        [SerializeField]
        private TextMeshProUGUI fpsText;

        [Header("Performance Settings")]
        [Tooltip("Determines the smoothing factor for the FPS calculation. Higher values result in smoother FPS readings.")]
        [Range(0.01f, 1f)]
        [SerializeField]
        private float smoothingFactor = 0.1f;

        private float _deltaTime = 0.0f;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes the fpsText component if not set in the Inspector.
        /// </summary>
        private void Awake()
        {
            if (fpsText == null)
            {
                fpsText = GetComponent<TextMeshProUGUI>();
                if (fpsText == null)
                {
                    Debug.LogError("TextMeshProUGUI component is missing. Please assign it in the Inspector.");
                }
            }
        }

        /// <summary>
        /// Update is called once per frame.
        /// Calculates the FPS and updates the fpsText with the current FPS value.
        /// </summary>
        private void Update()
        {
            CalculateFPS();
            DisplayFPS();
        }

        /// <summary>
        /// Calculates the FPS using a smoothing algorithm to avoid large fluctuations.
        /// </summary>
        private void CalculateFPS()
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * smoothingFactor;
        }

        /// <summary>
        /// Displays the current FPS on the assigned TextMeshProUGUI component.
        /// </summary>
        private void DisplayFPS()
        {
            if (fpsText != null)
            {
                float fps = 1.0f / _deltaTime;
                fpsText.text = $"FPS: {Mathf.Round(fps)}";
            }
        }
    }
}