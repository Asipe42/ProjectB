using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modin
{
    public class DialogueUI : BaseUI
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private Button nextButton;

        private DialogueUIModel model;

        private void Awake()
        {
            nextButton.onClick.AddListener(OnClickedNext);
        }
        
        public override void Open(BaseUIModel model)
        {
            this.model = model as DialogueUIModel;
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
        
        public void UpdateViews(DialogueLine line)
        {
            UpdateMessage(line.message);
            UpdateSpeaker(line.speaker);
        }

        private void UpdateMessage(string message)
        {
            messageText.text = message;
        }

        private void UpdateSpeaker(string speaker)
        {
            speakerText.text = speaker;
        }

        private void OnClickedNext()
        {
            model.OnNext.Invoke();
        }
    }
}