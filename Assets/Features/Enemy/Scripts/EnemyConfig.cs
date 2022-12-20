using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 2)]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float spawnDelay;
        [SerializeField] private Vector2 xBorders;
        [SerializeField] private Vector2 yBorders;
        [SerializeField] private Vector2 zBorders;

        public float LifeTime => lifeTime;
        public float SpawnDelay => spawnDelay;
        public Vector2 XBorders { get => xBorders; }
        public Vector2 YBorders { get => yBorders; }
        public Vector2 ZBorders { get => zBorders; }
    }
}