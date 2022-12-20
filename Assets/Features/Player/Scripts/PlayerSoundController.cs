using UnityEngine;
using Zenject;

namespace Features.Player
{
    public readonly struct PlayerSoundControllerProtocol
    {
        public readonly PlayerView PlayerView;
        public readonly AudioClip AudioClip;

        public PlayerSoundControllerProtocol(PlayerView playerView, AudioClip audioClip)
        {
            PlayerView = playerView;
            AudioClip = audioClip;
        }
    }

    public class PlayerSoundController
    {
        private readonly SignalBus _signalBus;
        private readonly PlayerView _playerView;
        private readonly AudioClip _audioClip;

        public PlayerSoundController(SignalBus signalBus, PlayerSoundControllerProtocol protocol)
        {
            _signalBus = signalBus;
            _audioClip = protocol.AudioClip;
            _playerView = protocol.PlayerView;
            _signalBus.Subscribe<WeaponFireMessage>(WeaponFireMessageHandler);
        }
        /// <summary>
        /// Проигрывает звук выстрела
        /// </summary>
        private void WeaponFireMessageHandler()
        {
            _playerView.PlaySound(_audioClip);
        }
    }
}