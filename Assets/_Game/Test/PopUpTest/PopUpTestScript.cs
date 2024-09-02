using System.Collections.Generic;
using UnityEngine;

namespace _Game._helpers.PopUp
{
    /// <summary>
    /// Demonstrates the usage of the PopUpTextManager by showing random text when a specified key is pressed.
    /// </summary>
    public class PopUpTestScript : MonoBehaviour
    {
        [Header("Pop-Up Text Manager")]
        [Tooltip("The PopUpTextManager instance used to display pop-ups.")]
        [SerializeField] private PopUpTextManager _popUpTextManager;

        [Header("Pop-Up Text Settings")]
        [Tooltip("A list of possible texts that will be shown randomly.")]
        [SerializeField] private List<string> _popUpTextOptions = new List<string>();

        [Tooltip("The position where the pop-up text will appear.")]
        [SerializeField] private Vector3 _popUpPosition = Vector3.zero;

        [Tooltip("The duration for which the pop-up text will be shown.")]
        [SerializeField, Range(0.1f, 2f)] private float _popUpDuration = 0.5f;

        private System.Random _random;

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the necessary components and values.
        /// </summary>
        private void Initialize()
        {
            if (_popUpTextManager == null)
            {
                Debug.LogError("PopUpTextManager is not assigned. Please assign it in the inspector.");
            }

            if (_popUpTextOptions == null || _popUpTextOptions.Count == 0)
            {
                Debug.LogError("Pop-up text options are not set. Please add some text options in the inspector.");
            }

            _random = new System.Random();
        }

        private void Update()
        {
            HandlePopUpTextTrigger();
        }

        /// <summary>
        /// Checks for the trigger key press and displays a random pop-up text.
        /// </summary>
        private void HandlePopUpTextTrigger()
        {
            if (Input.anyKeyDown && _popUpTextManager != null && _popUpTextOptions.Count > 0)
            {
                string randomText = GetRandomText();
                _popUpTextManager.ShowPopUpText(_popUpPosition, randomText, _popUpDuration);
            }
        }

        /// <summary>
        /// Returns a random text from the available pop-up text options.
        /// </summary>
        /// <returns>A random string from the list of pop-up texts.</returns>
        private string GetRandomText()
        {
            int randomIndex = _random.Next(0, _popUpTextOptions.Count);
            return _popUpTextOptions[randomIndex];
        }
    }
}