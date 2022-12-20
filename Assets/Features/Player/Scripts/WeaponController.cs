using DG.Tweening;
using Enemy;
using Features.Input;
using UnityEngine;
using Zenject;

namespace Features.Player
{
    public readonly struct WeaponControllerProtocol
    {
        public readonly Transform CameraTransform;

        public WeaponControllerProtocol(Transform cameraTransform)
        {
            CameraTransform = cameraTransform;
        }
    }

    public readonly struct WeaponFireMessage
    {
    }

    public readonly struct WeaponFireHitMessage
    {
        public readonly EnemyView EnemyView;

        public WeaponFireHitMessage(EnemyView enemyView)
        {
            EnemyView = enemyView;
        }
    }

    public readonly struct MissMessage
    {

    }

    public class WeaponController
    {
        private readonly Transform _cameraTransform;
        private readonly SignalBus _signalBus;

        private readonly float _range;
        private readonly float _delay;

        private bool _enable = true;

        public WeaponController(
            IFireButtonInput fireButtonInput,
            SignalBus signalBus,
            PlayerConfig config,
            WeaponControllerProtocol protocol)
        {
            _range = config.ShootRange;
            _delay = config.ShootDelay;
            fireButtonInput.FireButtonPressedEvent += Fire;
            _cameraTransform = protocol.CameraTransform;
            _signalBus = signalBus;
        }
        /// <summary>
        /// Производит выстрел по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fire(object sender, System.EventArgs e)
        {
            if (_enable)
            {
                _signalBus.Fire<WeaponFireMessage>(new WeaponFireMessage());
                _enable = false;
                RaycastHit hit;
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _range))
                {
                    var enemy = hit.transform.gameObject.GetComponent<EnemyView>();
                    if (enemy != null)
                    {
                        _signalBus.Fire<WeaponFireHitMessage>(new WeaponFireHitMessage(enemy));
                    }
                }
                DOVirtual.DelayedCall(_delay, () => { _enable = true; });
            }
        }
    }
}
