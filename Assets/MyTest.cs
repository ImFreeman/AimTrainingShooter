using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MyTest : MonoBehaviour
{
    private GameTimeHandler _timeHandler;
    private EnemyWaveController _enemyController;
    private SignalBus _signalBus;
    [Inject]
    public void Inject(EnemyWaveController enemyController, SignalBus signalBus, GameTimeHandler timeHandler)
    {
        _enemyController = enemyController;
        _signalBus = signalBus;
        _timeHandler = timeHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _timeHandler.StartGame();
        }              
    }
}
