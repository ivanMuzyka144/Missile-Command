using System.Collections.Generic;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.Player;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    List<EnemySpawner> EnemySpawners { get; }
    List<AttackTower> AttackTowers { get; }
    void Cleanup();
    AttackTowerManager CreateAttackTowerManager();
    AttackTower CreateAttackTower(Vector3 at);
    Rocket CreateRocket(Vector3 at);
    Explosion CreatePlayerExplosion(Vector3 at);
    Explosion CreateEnemyExplosion(Vector3 at);
    EnemySpawner CreateEnemySpawner(Vector3 at);
    EnemyBody CreateEnemy(Vector3 at);
    PlayerHouse CreatePlayerHouse(Vector3 at);
  }
}