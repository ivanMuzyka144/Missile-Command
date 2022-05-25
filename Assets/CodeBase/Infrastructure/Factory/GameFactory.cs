using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    public List<EnemySpawner> EnemySpawners { get; } = new List<EnemySpawner>();
    public List<AttackTower> AttackTowers { get; }= new List<AttackTower>();
    

    private readonly IAssetProvider _assets;
    private readonly IPersistentProgressService _persistentProgressService;
    private readonly ISharedDataService _sharedDataService;
    private readonly IInputService _inputService;

    private const string PlayerExplosionLayerName = "PlayerExplosion";
    private const string EnemyExplosionLayerName = "EnemyExplosion";
    
    public GameFactory(IAssetProvider assets, 
      IPersistentProgressService persistentProgressService, 
      ISharedDataService sharedDataService,
      IInputService inputService)
    {
      _assets = assets;
      _persistentProgressService = persistentProgressService;
      _sharedDataService = sharedDataService;
      _inputService = inputService;
    }

    public AttackTowerManager CreateAttackTowerManager()
    {
      AttackTowerManager attackTowerManager = _assets.Instantiate(AssetPath.AttackTowerManagerPath)
        .GetComponent<AttackTowerManager>();
      attackTowerManager.Construct(this, _inputService, _sharedDataService.SharedData.TowersData);
      return attackTowerManager;
    }

    public AttackTower CreateAttackTower(Vector3 at, int towerId)
    {
      AttackTower attackTower = _assets.Instantiate(path: AssetPath.AttackTowerPath, at: at)
        .GetComponent<AttackTower>();
      AttackTowers.Add(attackTower);

      int ammmmmo = 10;
      _sharedDataService.SharedData.TowersData.AddTower(towerId, 10);
      attackTower.Construct(this, towerId);
      attackTower.GetComponent<AmmunitionPresenter>().Construct(towerId, _sharedDataService.SharedData.TowersData);

      return attackTower;
    }

    public Rocket CreateRocket(Vector3 at)
    {
      Rocket rocket = _assets.Instantiate(path: AssetPath.RocketPath, at: at)
        .GetComponent<Rocket>();
      rocket.Construct(this);
      return rocket;
    }

    public Explosion CreatePlayerExplosion(Vector3 at)
    {
      GameObject playerExplosion = _assets.Instantiate(path: AssetPath.ExplosionPath, at: at);
      playerExplosion.layer = LayerMask.NameToLayer(PlayerExplosionLayerName);
      return playerExplosion.GetComponent<Explosion>();
    }
    public Explosion CreateEnemyExplosion(Vector3 at)
    {
      GameObject enemyExplosion = _assets.Instantiate(path: AssetPath.ExplosionPath, at: at);
      enemyExplosion.layer = LayerMask.NameToLayer(EnemyExplosionLayerName);
      return enemyExplosion.GetComponent<Explosion>();
    }

    public EnemySpawner CreateEnemySpawner(Vector3 at, Vector3 deadLinePosition)
    {
      EnemySpawner enemySpawner = _assets.Instantiate(path: AssetPath.EnemySpawnerPath, at: at)
        .GetComponent<EnemySpawner>();
      enemySpawner.Construct(this, deadLinePosition, _sharedDataService);
      EnemySpawners.Add(enemySpawner);
      return enemySpawner;
    }
    public EnemyBody CreateEnemy(Vector3 at, Vector3 deadLinePosition) =>
      _assets.Instantiate(path: AssetPath.EnemyPath, at: at)
        .GetComponent<EnemyBody>();

    public PlayerHouse CreatePlayerHouse(Vector3 at) =>
      _assets.Instantiate(path: AssetPath.PlayerHousePath, at: at)
        .GetComponent<PlayerHouse>();

    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
      EnemySpawners.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }
  }
}