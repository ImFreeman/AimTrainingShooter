using Zenject;
using UnityEngine;

namespace Features.UIService
{
    public class UIService : IUIService
    {
        private readonly IInstantiator _instantiator;

        private UIRoot _uIRoot;
        private UIWindow _hud;
        private UIWindow _gameOver;

        public UIService(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        /// <summary>
        /// ������ ���� � ���������� �� � ���
        /// </summary>
        public void Init()
        {
            var uiRootPrefab = Resources.Load<UIRoot>("UI/UIRoot");
            _uIRoot = _instantiator.InstantiatePrefabForComponent<UIRoot>(uiRootPrefab);

            var hudPrefab = Resources.Load<UIHUD>("UI/HUD");
            var goPrefab = Resources.Load<UIGameOver>("UI/GameOverWindow");
            _hud = _instantiator.InstantiatePrefabForComponent<UIWindow>(hudPrefab);
            _gameOver = _instantiator.InstantiatePrefabForComponent<UIWindow>(goPrefab);

            _hud.RectTransform.SetParent(_uIRoot.PoolContainer);
            _gameOver.RectTransform.SetParent(_uIRoot.PoolContainer);
        }
        /// <summary>
        /// ��������� ��������� ���� �� �������
        /// </summary>
        /// <param name="window">����</param>
        /// <param name="parent">������</param>
        private void ShowWindow(UIWindow window, Transform parent)
        {
            window.RectTransform.SetParent(parent);
            window.RectTransform.localScale = Vector3.one;
            window.RectTransform.localRotation = Quaternion.identity;
            window.RectTransform.localPosition = Vector3.zero;
            window.RectTransform.offsetMax = Vector2.zero;
            window.RectTransform.offsetMin = Vector2.zero;
        }

        /// <summary>
        /// ���������� ���
        /// </summary>
        public void ShowHUD()
        {
            _gameOver.RectTransform.SetParent(_uIRoot.PoolContainer);
            Cursor.lockState = CursorLockMode.Locked;
            ShowWindow(_hud, _uIRoot.ActiveContainer);
        }
        /// <summary>
        /// ���������� ���� ���������
        /// </summary>
        public void ShowGameOverWindow()
        {
            _hud.RectTransform.SetParent(_uIRoot.PoolContainer);
            ShowWindow(_gameOver, _uIRoot.ActiveContainer);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}