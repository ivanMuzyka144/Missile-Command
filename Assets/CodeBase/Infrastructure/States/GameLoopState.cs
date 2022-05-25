using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Input;
using CodeBase.Services.SharedData;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState, IUpdatable
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IInputService _inputService;
    private readonly IGameFactory _factory;
    private readonly ISharedDataService _sharedDataService;

    private bool _shouldRecordInput;
    public GameLoopState(GameStateMachine stateMachine, 
      IInputService inputService, 
      IGameFactory factory, 
      ISharedDataService sharedDataService)
    {
      _stateMachine = stateMachine;
      _inputService = inputService;
      _factory = factory;
      _sharedDataService = sharedDataService;

      _factory.OnAmmoEnded += HandleAmmoEnded;
      _factory.OnHousesDestroyed += HandlePlayerHousesDestroyed;
      _sharedDataService.SharedData.EnemiesData.OnAllEnemiesDestroyed += HandleAllEnemiesDestroyed;
    
      _shouldRecordInput = true;
    }

    public void Enter()
    {
      StartEnemySpawning();
    }

    public void Exit() { }


    public void Tick()
    {
      if(_shouldRecordInput)
        RecordInput();

    }

    private void RecordInput()
    {
      _inputService.RecordMousePosition();
      _inputService.RecordMouseCLicked();
    }

    private void StartEnemySpawning()
    {
      Sequence spawnSequence = DOTween.Sequence();
      int howMany = 10;
      
      _sharedDataService.SharedData.EnemiesData.SetupEnemiesCount(howMany);

      for (int i = 0; i < howMany; i++)
      {
        spawnSequence.AppendInterval(Random.Range(0.5f, 1.5f));
        spawnSequence.AppendCallback(SpawnEnemy);  
      }
    }

    private void SpawnEnemy()
    {
      EnemySpawner randomSpawner = _factory.EnemySpawners.RandomItem();
      randomSpawner.SpawnEnemy();
    }

    private void HandleAmmoEnded()
    {
      Debug.Log("Ammo ended!");
    }

    private void HandlePlayerHousesDestroyed()
    {
      Debug.Log("PlayerHousesDestroyed");
    }

    private void HandleAllEnemiesDestroyed()
    {
      Debug.Log("AllEnemiesDestroyed");
    }
  }
}