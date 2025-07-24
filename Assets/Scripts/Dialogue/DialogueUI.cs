using TMPro;
using UnityEngine;

namespace Modin
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private TextMeshProUGUI speakerText;

        public void UpdateDialogue(DialogueLine line)
        {
            UpdateMessage(line.Text);
            UpdateSpeaker(line.Speaker);
        }

        private void UpdateMessage(string message)
        {
            messageText.text = message;
        }

        private void UpdateSpeaker(string speaker)
        {
            speakerText.text = speaker;
        }
    }
}