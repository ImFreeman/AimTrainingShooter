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
        /// Грузит окна и засовывает их в пул
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
        /// Правильно размещает окно на канвасе
        /// </summary>
        /// <param name="window">Окно</param>
        /// <param name="parent">Канвас</param>
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
        /// Показывает худ
        /// </summary>
        public void ShowHUD()
        {
            _gameOver.RectTransform.SetParent(_uIRoot.PoolContainer);
            Cursor.lockState = CursorLockMode.Locked;
            ShowWindow(_hud, _uIRoot.ActiveContainer);
        }
        /// <summary>
        /// Показывает окно проигрыша
        /// </summary>
        public void ShowGameOverWindow()
        {
            _hud.RectTransform.SetParent(_uIRoot.PoolContainer);
            ShowWindow(_gameOver, _uIRoot.ActiveContainer);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}