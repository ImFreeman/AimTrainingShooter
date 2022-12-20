using Features.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UIService
{
    public class UIGameOver : UIWindow
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private TMP_Text scoreText;

        private SignalBus _signalBus;
        private PlayerDataHandler _playerDataHandler;

        [Inject]
        private void Inject(SignalBus signalBus, PlayerDataHandler playerDataHandler)
        {
            _signalBus = signalBus;
            _playerDataHandler = playerDataHandler;
            _signalBus.Subscribe<GameOverMessage>(GameOverHandler);
            restartButton.onClick.AddListener(OnRestartButtonClickHandler);
        }

        private void GameOverHandler()
        {
            scoreText.text = _playerDataHandler.CurrentScore.ToString();
        }

        private void OnRestartButtonClickHandler()
        {
            _signalBus.Fire(new RestartMessage());
        }
    }
}