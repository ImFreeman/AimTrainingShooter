using System;
using Zenject;

namespace Features.Player
{
    public readonly struct PlayerScoreChangeMessage
    {
        public readonly int Value;

        public PlayerScoreChangeMessage(int value)
        {
            Value = value;
        }
    }

    public class PlayerDataHandler
    {
        public int CurrentScore => _currentScore;
        public event EventHandler<int> ScoreChangedEvent;

        private readonly SignalBus _signalBus;

        private int _currentScore;

        public PlayerDataHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<MissMessage>(MissHandler);
            _signalBus.Subscribe<WeaponFireHitMessage>(HitHandler);
            _signalBus.Subscribe<StartGameMessage>(StartGameHandler);
        }

        private void ChangeScore(int delta)
        {
            _currentScore = Math.Max(0, _currentScore + delta);
            _signalBus.Fire<PlayerScoreChangeMessage>(new PlayerScoreChangeMessage(_currentScore));
        }

        private void MissHandler()
        {
            ChangeScore(-1);
        }

        private void HitHandler()
        {
            ChangeScore(1);
        }

        private void StartGameHandler()
        {
            ChangeScore(-_currentScore);
        }
    }
}