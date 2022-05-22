using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.AttackTower;
using CodeBase.Logic.Enemy;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    
    public List<EnemySpawner> EnemySpawners { get; } = new List<EnemySpawner>();

    private readonly IAssetProvider _assets;
    private readonly IPersistentProgressService _persistentProgressService;
    public GameFactory(IAssetProvider assets, IPersistentProgressService persistentProgressService)
    {
      _assets = assets;
      _persistentProgressService = persistentProgressService;
    }

    public AttackTower CreateAttackTower(Vector3 at) => 
      _assets.Instantiate(path: AssetPath.AttackTowerPath, at: at)
             .GetComponent<AttackTower>();

    public Rocket CreateRocket(Vector3 at)
    {
      Rocket rocket = _assets.Instantiate(path: AssetPath.RocketPath, at: at)
        .GetComponent<Rocket>();
      rocket.Construct(this);
      return rocket;
    }

    public Explosion CreateExplosion(Vector3 at) =>
      _assets.Instantiate(path: AssetPath.ExplosionPath, at: at)
        .GetComponent<Explosion>();

    public EnemySpawner CreateEnemySpawner(Vector3 at)
    {
      EnemySpawner enemySpawner = _assets.Instantiate(path: AssetPath.EnemySpawnerPath, at: at)
        .GetComponent<EnemySpawner>();
      enemySpawner.Construct(this);
      EnemySpawners.Add(enemySpawner);
      return enemySpawner;
    }
    public Enemy CreateEnemy(Vector3 at) =>
      _assets.Instantiate(path: AssetPath.EnemyPath, at: at)
        .GetComponent<Enemy>();

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