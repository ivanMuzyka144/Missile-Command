using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Input;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState, IUpdatable
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IInputService _inputService;
    private readonly IGameFactory _factory;

    public GameLoopState(GameStateMachine stateMachine, IInputService inputService, IGameFactory factory)
    {
      _stateMachine = stateMachine;
      _inputService = inputService;
      _factory = factory;
    }

    public void Enter()
    {
      StartEnemySpawning();
    }

    public void Exit() { }


    public void Tick()
    {
      _inputService.RecordMousePosition();
      _inputService.RecordMouseCLicked();
    }

    private void StartEnemySpawning()
    {
      Sequence spawnSequence = DOTween.Sequence();
      int howMany = 10;

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
  }
}