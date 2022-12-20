using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyWaveController : IEnemyWaveController, ITickable
    {
        private readonly float _spawnDelay;
        private readonly Vector2 _xBorders;
        private readonly Vector2 _yBorders;
        private readonly Vector2 _zBorders;

        private readonly IEnemyController _enemyController;
        private readonly TickableManager _tickableManager;
        private readonly SignalBus _signalBus;
        private readonly System.Random _random = new System.Random();       

        private float _currentTime;

        public EnemyWaveController(
            IEnemyController enemyController,
            TickableManager tickableManager,
            SignalBus signalBus,
            EnemyConfig config)
        {
            _tickableManager = tickableManager;
            _enemyController = enemyController;
            _signalBus = signalBus;

            _spawnDelay = config.SpawnDelay;
            _xBorders = config.XBorders;
            _yBorders = config.YBorders;
            _zBorders = config.ZBorders;

            _signalBus.Subscribe<StartGameMessage>(StartWave);
        }
        /// <summary>
        /// Запускает волну врагов
        /// </summary>
        public void StartWave()
        {
            _tickableManager.Add(this);
            _signalBus.Subscribe<GameOverMessage>(GameOverHandler);
        }
        /// <summary>
        /// Обработчик сообщения об конце игры
        /// </summary>
        private void GameOverHandler()
        {
            _tickableManager.Remove(this);
            _enemyController.ClearAll();
            _signalBus.Unsubscribe<GameOverMessage>(GameOverHandler);
        }
        /// <summary>
        /// Вызывается каждый кадр
        /// </summary>
        public void Tick()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _spawnDelay)
            {
                _currentTime = 0f;
                _enemyController.Spawn(GetRandomPoint());
            }
        }
        /// <summary>
        /// Генерирует рандомную точку в границах из конфига
        /// </summary>
        /// <returns>Рандомная точка в границах из конфига(возьмите меня на работу пожалуйста мне не хватает стипендии)</returns>
        private Vector3 GetRandomPoint()
        {
            var x = (float)_random.NextDouble() * (_xBorders.y - _xBorders.x) + _xBorders.x;
            var y = (float)_random.NextDouble() * (_yBorders.y - _yBorders.x) + _yBorders.x;
            var z = (float)_random.NextDouble() * (_zBorders.y - _zBorders.x) + _zBorders.x;
            return new Vector3(x, y, z);
        }
    }
}