using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
  public class EnemySpawner : MonoBehaviour
  {
    private IGameFactory _factory;

    public void Construct(IGameFactory factory)
    {
      _factory = factory;
    }
    public void SpawnEnemy()
    {
      EnemyBody enemyBody = _factory.CreateEnemy(transform.position);
      Vector3 rotationVector = new Vector3(0, 0, 180 + Random.Range(-10,10));
      Vector3 directionVector = GetDirectionVector(rotationVector);
      enemyBody.Construct(directionVector);
      enemyBody.transform.eulerAngles = rotationVector;
    }

    private Vector3 GetDirectionVector(Vector3 rotationVector)
    {
      Quaternion rotation = Quaternion.Euler(rotationVector);
      Vector3 myVector = new Vector3(0,1,0);
      return rotation * myVector;
    }
  }
}
