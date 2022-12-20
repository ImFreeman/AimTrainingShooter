using DG.Tweening;
using Features.Player;
using UnityEngine;
using Zenject;

public readonly struct SetTimeMessage
{
    public readonly float ValueCurrent;
    public readonly float MaxTime;
    public readonly bool IsFirst;

    public SetTimeMessage(
        float value,
        float maxTime, 
        bool isFirst)
    {
        ValueCurrent = value;
        MaxTime = maxTime;
        IsFirst = isFirst;
    }
}

public readonly struct RestartMessage
{

}

public readonly struct GameOverMessage
{

}

public readonly struct StartGameMessage
{

}
/// <summary>
/// Отвечает за время игры
/// </summary>
public class GameTimeHandler : ITickable
{
    private readonly float _levelDuration;
    private readonly float _beginDelay = 3f;
    private readonly SignalBus _signalBus;
    private readonly TickableManager _tickableManager;

    private float _currentTime;

    public GameTimeHandler(
        SignalBus signalBus,
        TickableManager tickableManager,
        PlayerConfig config)
    {
        _levelDuration = config.GameDuration;
        _beginDelay = config.GameStartDelay;
        _signalBus = signalBus;
        _tickableManager = tickableManager;
    }
    /// <summary>
    /// Старт игры
    /// </summary>
    public void StartGame()
    {
        DOVirtual.DelayedCall(_beginDelay, () => 
        {
            _currentTime = 0f;
            _tickableManager.Add(this);
            _signalBus.Fire(new StartGameMessage());
        })
            .OnUpdate(()=> 
            {
                _currentTime += Time.deltaTime;
                Debug.Log($"Time :{_currentTime}");
                _signalBus.Fire(new SetTimeMessage(_currentTime, _beginDelay, true));
            });
    }
    /// <summary>
    /// Вызывается каждый кадр
    /// </summary>
    public void Tick()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _levelDuration)
        {
            _signalBus.Fire(new SetTimeMessage(_currentTime, _levelDuration, false));
            _signalBus.Fire(new GameOverMessage());
            _currentTime = 0;
            _tickableManager.Remove(this);
            return;
        }
        _signalBus.Fire(new SetTimeMessage(_currentTime, _levelDuration, false));
    }
}
   