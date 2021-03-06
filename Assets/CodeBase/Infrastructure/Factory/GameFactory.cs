using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
using CodeBase.UI;
using UnityEngine;
using IDisposable = CodeBase.Services.IDisposable;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory, IDisposable
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    public List<EnemySpawner> EnemySpawners { get; } = new List<EnemySpawner>();
    public List<AttackTower> AttackTowers { get; } = new List<AttackTower>();
    public List<PlayerHouse> PlayerHouses { get; } = new List<PlayerHouse>();
    public event Action OnAmmoEnded;
    public event Action OnHousesDestroyed;

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
      Explosion playerExplosion = _assets.Instantiate(path: AssetPath.ExplosionPath, at: at).GetComponent<Explosion>();
      playerExplosion.gameObject.layer = LayerMask.NameToLayer(PlayerExplosionLayerName);
      playerExplosion.OnExplosionEnded += CheckIsAmmoEnded;
      return playerExplosion;
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
      enemySpawner.Construct(this, deadLinePosition, _sharedDataService.SharedData.EnemiesData);
      EnemySpawners.Add(enemySpawner);
      return enemySpawner;
    }
    public EnemyBody CreateEnemy(Vector3 at, Vector3 deadLinePosition) =>
      _assets.Instantiate(path: AssetPath.EnemyPath, at: at)
        .GetComponent<EnemyBody>();

    public PlayerHouse CreatePlayerHouse(int houseId, Vector3 at)
    {
      PlayerHouse playerHouse = _assets.Instantiate(path: AssetPath.PlayerHousePath, at: at)
        .GetComponent<PlayerHouse>();
      playerHouse.Construct(houseId,this);
      PlayerHouses.Add(playerHouse);
      return playerHouse;
    }

    public void DestroyPlayerHouse(PlayerHouse playerHouse)
    {
      PlayerHouses.Remove(playerHouse);
      _sharedDataService.SharedData.HouseDataDictionary.DestroyHouse(playerHouse.HouseId);
      GameObject.Destroy(playerHouse.gameObject);
      
      if (PlayerHouses.Count == 0) 
        OnHousesDestroyed?.Invoke();
    }

    private void CheckIsAmmoEnded()
    {
      if (_sharedDataService.SharedData.TowersData.IsAmmoEnded()) 
        OnAmmoEnded?.Invoke();
    }

    
    public void Cleanup()
    {
      for (int i = 0; i < EnemySpawners.Count; i++) 
        GameObject.Destroy(EnemySpawners[i].gameObject);
      
      for (int i = 0; i < AttackTowers.Count; i++) 
        GameObject.Destroy(AttackTowers[i].gameObject);
      
      for (int i = 0; i < PlayerHouses.Count; i++) 
        GameObject.Destroy(PlayerHouses[i].gameObject);

      ProgressReaders.Clear();
      ProgressWriters.Clear();
      EnemySpawners.Clear();
      AttackTowers.Clear();
      PlayerHouses.Clear();
      OnAmmoEnded = null;
      OnHousesDestroyed = null;
    }
    public void Dispose() => 
      Cleanup();
  }
}