using System;
using System.Collections.Generic;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.Player;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    List<EnemySpawner> EnemySpawners { get; }
    List<AttackTower> AttackTowers { get; }
    List<PlayerHouse> PlayerHouses { get; }
    event Action OnAmmoEnded;
    event Action OnHousesDestroyed;
    void Cleanup();
    AttackTowerManager CreateAttackTowerManager();
    AttackTower CreateAttackTower(Vector3 at, int towerId);
    Rocket CreateRocket(Vector3 at);
    Explosion CreatePlayerExplosion(Vector3 at);
    Explosion CreateEnemyExplosion(Vector3 at);
    EnemySpawner CreateEnemySpawner(Vector3 at, Vector3 deadLinePosition);
    EnemyBody CreateEnemy(Vector3 at, Vector3 deadLinePosition);
    PlayerHouse CreatePlayerHouse(Vector3 at);
    void DestroyPlayerHouse(PlayerHouse playerHouse);
  }
}