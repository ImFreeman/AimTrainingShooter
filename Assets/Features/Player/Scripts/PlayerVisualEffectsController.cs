using DG.Tweening;
using Zenject;

namespace Features.Player
{
    public readonly struct PlayerAnimationProtocol
    {
        public readonly PlayerView PlayerView;

        public PlayerAnimationProtocol(PlayerView playerView)
        {
            PlayerView = playerView;
        }
    }

    public class PlayerVisualEffectsController
    {
        private readonly SignalBus _signalBus;
        private readonly PlayerView _playerView;

        public PlayerVisualEffectsController(SignalBus signalBus, PlayerAnimationProtocol protocol)
        {
            _signalBus = signalBus;
            _playerView = protocol.PlayerView;
            _signalBus.Subscribe<WeaponFireMessage>(WeaponFireMessageHandler);
        }
        /// <summary>
        /// Запускает анимацию выстрела
        /// </summary>
        private void WeaponFireMessageHandler()
        {
            _playerView.PlayShootAnim();
            _playerView.PlayMuzzleFlash(true);
            DOVirtual.DelayedCall(0.05f, () => { _playerView.PlayMuzzleFlash(false); });
        }
    }
}