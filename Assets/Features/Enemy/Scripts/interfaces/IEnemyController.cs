using UnityEngine;

namespace Enemy
{
    public interface IEnemyController
    {
        /// <summary>
        /// ������� ����� �� ������� �����������
        /// </summary>
        /// <param name="position">����������</param>
        public void Spawn(Vector3 position);
        /// <summary>
        /// ������� ���� ������ � �����
        /// </summary>
        public void ClearAll();
    }
}