using UnityEngine;

namespace Enemy
{
    public interface IEnemyController
    {
        /// <summary>
        /// Спавнит врага по заданым координатам
        /// </summary>
        /// <param name="position">координаты</param>
        public void Spawn(Vector3 position);
        /// <summary>
        /// Убирает всех врагов с сцены
        /// </summary>
        public void ClearAll();
    }
}