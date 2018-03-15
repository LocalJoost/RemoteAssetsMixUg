
using HoloToolkit.Unity;
using HoloToolkitExtensions.Messaging;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkitExtensions.Audio
{
    public class SpokenFeedbackManager : MonoBehaviour
    {
        private Queue<string> _messages = new Queue<string>();
        private void Start()
        {
            Messenger.Instance.AddListener<SpokenFeedbackMessage>(AddTextToQueue);
            _ttsManager = GetComponent<TextToSpeech>();
        }

        private void AddTextToQueue(SpokenFeedbackMessage msg)
        {
            _messages.Enqueue(msg.Message);
        }

        private TextToSpeech _ttsManager;

        private void Update()
        {
            SpeakText();
        }

        private void SpeakText()
        {
            if (_ttsManager != null && _messages.Count > 0)
            {
                if(! (_ttsManager.SpeechTextInQueue() || _ttsManager.IsSpeaking()))
                {
                    _ttsManager.StartSpeaking(_messages.Dequeue());
                }
            }
        }
    }
}

