using System;

namespace CodeBase.Data
{
  public class EnemiesData
  {
    public int EnemiesCount { get; private set; }
    public int KilledEnemies { get; private set; }

    public Action OnAllEnemiesDestroyed;

    public void SetupEnemiesCount(int enemiesCount)
    {
      EnemiesCount = enemiesCount;
      KilledEnemies = 0;
    }

    public void RecordEnemyKilledByPlayer()
    {
      KilledEnemies++;
    }

    public void RecordEnemyDestroyed()
    {
      EnemiesCount--;
      if(EnemiesCount == 0)
        OnAllEnemiesDestroyed?.Invoke();
    }

    public void Clear()
    {
      EnemiesCount = 0;
      KilledEnemies = 0;
    }
  }
}