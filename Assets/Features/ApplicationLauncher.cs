using Zenject;
using UnityEngine;
using Features.UIService;
using Features.Input;
using Features.Player;

/// <summary>
/// Запуск приложения
/// </summary>
public class ApplicationLauncher
{
    private readonly IUIService _uIService;
    private readonly GameTimeHandler _gameTimeHandler;
    private readonly ICameraRotationInput _cameraRotationInput;
    private readonly IFireButtonInput _fireButtonInput;

    public ApplicationLauncher(
        IInstantiator instantiator,
        ICameraRotationInput cameraRotationInput,
        IFireButtonInput fireButtonInput,
        IUIService uiSevice,
        SignalBus signalBus,
        GameTimeHandler timeHandler)
    {        
        instantiator.Instantiate<CreatePlayerCommand>();
        _gameTimeHandler = timeHandler;
        _cameraRotationInput = cameraRotationInput;
        _fireButtonInput = fireButtonInput;

        _uIService = uiSevice;
        _uIService.Init();

        StartGame();

        signalBus.Subscribe<GameOverMessage>(GameOverHandler);
        signalBus.Subscribe<RestartMessage>(RestartGameHandler);

        Resources.UnloadUnusedAssets();
    }

    private void StartGame()
    {
        _uIService.ShowHUD();
        _gameTimeHandler.StartGame();
        _cameraRotationInput.IsActive = true;
        _fireButtonInput.IsActive = true;
    }

    private void GameOverHandler()
    {
        _uIService.ShowGameOverWindow();
        _cameraRotationInput.IsActive = false;
        _fireButtonInput.IsActive = false;
    }

    private void RestartGameHandler()
    {
        _uIService.ShowHUD();
        StartGame();
    }
}
