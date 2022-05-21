using System.Collections.Generic;
using CodeBase.Logic.AttackTower;
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
    void Cleanup();

    AttackTower CreateAttackTower(Vector3 at);
    Rocket CreateRocket(Vector3 at);
    Explosion CreateExplosion(Vector3 at);
    EnemySpawner CreateEnemySpawner(Vector3 at);
  }
}