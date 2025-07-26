using TMPro;
using UnityEngine;

namespace Modin
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private TextMeshProUGUI speakerText;

        public void Initialize()
        {
            gameObject.SetActive(true);
        }

        public void CleanUp()
        {
            gameObject.SetActive(false);
        }
        
        public void UpdateViews(DialogueLine line)
        {
            UpdateMessage(line.Message);
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