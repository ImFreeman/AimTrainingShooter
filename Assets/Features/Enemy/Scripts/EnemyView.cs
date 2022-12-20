using UnityEngine;
using Zenject;

namespace Enemy
{
    public readonly struct EnemyViewProtocol
    {
        public readonly Vector3 Position;

        public EnemyViewProtocol(Vector3 position)
        {
            Position = position;
        }
    }

    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform bodyTransform;
        /// <summary>
        /// Инициализация врага
        /// </summary>
        /// <param name="position">Точка появления</param>
        private void Init(Vector3 position)
        {
            bodyTransform.position = position;
        }

        public class Pool : MonoMemoryPool<EnemyViewProtocol, EnemyView>
        {
            protected override void Reinitialize(EnemyViewProtocol p1, EnemyView item)
            {
                item.Init(p1.Position);
            }
        }
    }
}