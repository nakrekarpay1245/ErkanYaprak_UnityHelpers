using TMPro;
using UnityEngine;

namespace _Game._helpers.PopUp
{
    /// <summary>
    /// Manages pop-up text, including setting the text and animating its appearance using coroutines.
    /// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class PopUpText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _textComponent;

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