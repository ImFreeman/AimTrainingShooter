using UnityEngine;

namespace Features.UIService
{
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        public RectTransform RectTransform { get => rectTransform; }
    }
}