using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Input;
using CodeBase.Services.SharedData;
using CodeBase.UI;
using CodeBase.UI.Services;
using CodeBase.UI.Window;
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
    private readonly IWindowService _windowService;

    private WindowBase _currWindow;
    private bool _shouldRecordInput;
    public GameLoopState(GameStateMachine stateMachine, 
      IInputService inputService, 
      IGameFactory factory, 
      ISharedDataService sharedDataService,
      IWindowService windowService)
    {
      _stateMachine = stateMachine;
      _inputService = inputService;
      _factory = factory;
      _sharedDataService = sharedDataService;
      _windowService = windowService;

      _factory.OnAmmoEnded += HandleAmmoEnded;
      _factory.OnHousesDestroyed += HandlePlayerHousesDestroyed;
      _sharedDataService.SharedData.EnemiesData.OnAllEnemiesDestroyed += HandleAllEnemiesDestroyed;
    
      _shouldRecordInput = true;
    }

    public void Enter()
    {
      _shouldRecordInput = true;
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

    private void HandleAmmoEnded() => 
      SpeedUpGame();

    private void HandleAllEnemiesDestroyed()
    {
      PauseGame();
      LevelCompletedWindow window = _windowService.Open(WindowId.LevelCompleted) as LevelCompletedWindow;
      window.Construct(Next,Restart);
      _currWindow = window;
    }

    private void HandlePlayerHousesDestroyed()
    {
     // PauseGame();
     // _windowService.Open(WindowId.LevelCompleted);
    }

    private void Next()
    {
      GameObject.Destroy(_currWindow.gameObject);
      _stateMachine.Enter<DisposableState, bool>(true);
    }

    private void Restart()
    {
      GameObject.Destroy(_currWindow.gameObject);
      _stateMachine.Enter<DisposableState, bool>(false);
    }

    private void SpeedUpGame() => Time.timeScale = 10;
    private void PauseGame()
    {
      Time.timeScale = 1;
      _shouldRecordInput = false;
    }
  }
}