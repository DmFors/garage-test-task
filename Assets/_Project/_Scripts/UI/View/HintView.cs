using TMPro;
using UnityEngine;

namespace Game.UI.View
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HintView : MonoBehaviour
    {
        private TextMeshProUGUI hintText;

        private void Awake() => hintText = GetComponent<TextMeshProUGUI>();

        public void ShowHint(string message) => hintText.text = message;

        public void HideHint() => hintText.text = string.Empty;
    }
}