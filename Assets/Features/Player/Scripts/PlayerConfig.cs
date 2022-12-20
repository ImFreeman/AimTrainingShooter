using UnityEngine;

namespace Features.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float sensivity;
        [SerializeField] private AudioClip fireAudioClip;
        [SerializeField] private float shootDelay;
        [SerializeField] private float shootRange;
        [SerializeField] private float gameStartDelay;
        [SerializeField] private float gameDuration;

        public float Sensivity => sensivity;
        public AudioClip FireSound => fireAudioClip;
        public float ShootDelay => shootDelay;
        public float ShootRange => shootRange;

        public float GameStartDelay => gameStartDelay; 
        public float GameDuration => gameDuration;
    }
}