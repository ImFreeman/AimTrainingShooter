using UnityEngine;

namespace Features.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform muzzleFlash;
        [SerializeField] private AudioSource audioSource;
        public Camera MainCamera => mainCamera;
        public Transform BodyTransform => bodyTransform;
        public void PlayShootAnim()
        {
            animator.SetTrigger("ShootTrigger");
        }
        public void PlayMuzzleFlash(bool value)
        {
            muzzleFlash.gameObject.SetActive(value);
        }
        public void PlaySound(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}