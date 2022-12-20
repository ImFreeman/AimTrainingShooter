using UnityEngine;

namespace Features.UIService
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform activeContainer;
        [SerializeField] private Transform poolContainer;

        public Transform ActiveContainer { get => activeContainer; }
        public Transform PoolContainer { get => poolContainer; }
    }
}