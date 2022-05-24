using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private const string EnemySpawnerTag = "EnemySpawner";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IInputService _inputService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
      IGameFactory gameFactory, IPersistentProgressService progressService, IInputService inputService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _inputService = inputService;
    }

    public void Enter(string sceneName)
    {
      _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() {}

    private void OnLoaded()
    {
      SetBorderToInputService();
      InitGameWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private void SetBorderToInputService()
    {
      Vector3 mouseRecordLine = GameObject.Find("MouseRecordLine").transform.position;
      _inputService.SetBorder(mouseRecordLine);
    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
      CreateAttackTowerManager();
      CreatePlayerHouses();
      CreateEnemySpawners();
    }

    private void CreatePlayerHouses()
    {
      var spawnPositions = GameObject.FindGameObjectsWithTag("PlayerHousePoint")
                                                       .Select(x => x.transform.position);
      foreach (var positions in spawnPositions)
      {
        PlayerHouse house = _gameFactory.CreatePlayerHouse(positions);
      }
    }

    private void CreateAttackTowerManager()
    {
      Vector3[] attackTowerPoints = GameObject.FindGameObjectsWithTag("AttackTowerSpawnPoint")
                                              .Select(x => x.transform.position)
                                              .ToArray();
      
      AttackTowerManager attackTowerManager = _gameFactory.CreateAttackTowerManager();
      attackTowerManager.SetupTowers(attackTowerPoints);
    }

    private void CreateEnemySpawners()
    {
      Vector3 minEnemySpawnerPosition = GameObject.Find("MinEnemySpawnPoint").transform.position;
      Vector3 maxEnemySpawnerPosition = GameObject.Find("MaxEnemySpawnPoint").transform.position;
      Vector3 enemyDeadLinePosition = GameObject.Find("EnemyDeadLine").transform.position;
  
      float howManySpawners = 8;
      float positionStep = (maxEnemySpawnerPosition.x - minEnemySpawnerPosition.x) / howManySpawners;

      for (int i = 0; i <= howManySpawners; i++)
      {
        Vector3 spawnerPosition = minEnemySpawnerPosition + new Vector3(positionStep * i, 0, 0);
        _gameFactory.CreateEnemySpawner(spawnerPosition, enemyDeadLinePosition);
      }
    }
  }
}