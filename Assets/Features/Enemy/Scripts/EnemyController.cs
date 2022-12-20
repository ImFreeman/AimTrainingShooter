using DG.Tweening;
using Features.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyController : IEnemyController
    {
        private readonly SignalBus _signalBus;
        private readonly EnemyView.Pool _pool;
        private readonly float _lifeTime;

        private Dictionary<int, Tween> _tweens = new Dictionary<int, Tween>();
        private Dictionary<int, EnemyView> _views = new Dictionary<int, EnemyView>();

        public EnemyController(
            SignalBus signalBus,
            EnemyView.Pool pool,
            EnemyConfig config)
        {
            _lifeTime = config.LifeTime;
            _signalBus = signalBus;
            _pool = pool;
            _signalBus.Subscribe<WeaponFireHitMessage>(OnHit);
        }

        /// <summary>
        /// ������� ����� �� ���� �� �������� �����������
        /// </summary>
        /// <param name="position">����� ��������� �����</param>
        public void Spawn(Vector3 position)
        {
            var enemy = _pool.Spawn(new EnemyViewProtocol(position));
            var tween = DOVirtual.DelayedCall(_lifeTime, () =>
            {
                _signalBus.Fire<MissMessage>();
                Despawn(enemy);
            });
            var id = enemy.GetInstanceID();
            if (!_views.ContainsKey(id))
            {
                _views.Add(id, enemy);
            }
            _tweens.Add(enemy.GetInstanceID(), tween);
        }

        /// <summary>
        /// ������� ����� ������� � ��� �����������
        /// </summary>
        /// <param name="enemy">���������</param>
        private void Despawn(EnemyView enemy)
        {
            _pool.Despawn(enemy);
            _tweens.Remove(enemy.GetInstanceID());
        }

        /// <summary>
        /// ������� ���� ������ �� ������ ������� � ���
        /// </summary>
        public void ClearAll()
        {
            var keys = new List<int>(_tweens.Keys);
            foreach (var key in keys)
            {
                if (_tweens[key] != null)
                {
                    _tweens[key].Kill();
                    Despawn(_views[key]);
                }
            }
        }

        /// <summary>
        /// ��������� ��������� � ��������� � �����
        /// </summary>
        /// <param name="message">��������� � ��������� � �����</param>
        private void OnHit(WeaponFireHitMessage message)
        {
            _tweens[message.EnemyView.GetInstanceID()].Kill();
            Despawn(message.EnemyView);
        }

    }
}