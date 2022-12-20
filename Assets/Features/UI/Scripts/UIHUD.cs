using Features.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Features.UIService
{
    public class UIHUD : UIWindow
    {

        [SerializeField] private TMP_Text scoreValue;
        [SerializeField] private TMP_Text firstTimeValue;
        [SerializeField] private TMP_Text secondTimeValue;
        private SignalBus _signalBus;
        private bool _isFirstTimerActive;
        private TMP_Text _currentTimer;

        [Inject]
        private void Inject(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _currentTimer = firstTimeValue;
            _isFirstTimerActive = true;

            _signalBus.Subscribe<PlayerScoreChangeMessage>(ScoreChangeHandler);
            _signalBus.Subscribe<SetTimeMessage>(SetTimeHandler);
        }

        private void ScoreChangeHandler(PlayerScoreChangeMessage message)
        {
            scoreValue.text = message.Value.ToString();
        }

        private void ChangeActiveTimer(bool isFirst)
        {
            _isFirstTimerActive = isFirst;
            if(_isFirstTimerActive)
            {
                _currentTimer = firstTimeValue;
                firstTimeValue.gameObject.SetActive(true);
                secondTimeValue.gameObject.SetActive(false);
            }
            else
            {
                _currentTimer = secondTimeValue;
                firstTimeValue.gameObject.SetActive(false);
                secondTimeValue.gameObject.SetActive(true);
            }
        }

        private void SetTimeHandler(SetTimeMessage message)
        {
            if(_isFirstTimerActive != message.IsFirst)
            {
                ChangeActiveTimer(message.IsFirst);
            }
            var minutes = (int)((message.MaxTime - message.ValueCurrent) / 60f);
            var secs = (int)((message.MaxTime - message.ValueCurrent) % 60f);
            if (secs > 9)
            {
                _currentTimer.text = $"{minutes}:{secs}";
            }
            else
            {
                _currentTimer.text = $"{minutes}:0{secs}";
            }
        }
    }
}