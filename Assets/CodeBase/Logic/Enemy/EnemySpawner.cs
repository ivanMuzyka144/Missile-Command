using CodeBase.Infrastructure.Factory;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
  public class EnemySpawner : MonoBehaviour
  {
    private IGameFactory _factory;
    private ISharedDataService _sharedDataService;
    private Vector3 _deadLinePosition;

    public void Construct(IGameFactory factory, Vector3 deadLinePosition, ISharedDataService sharedDataService)
    {
      _factory = factory;
      _sharedDataService = sharedDataService;
      _deadLinePosition = deadLinePosition;
    }
    public void SpawnEnemy()
    {
      EnemyBody enemyBody = _factory.CreateEnemy(transform.position, _deadLinePosition);
      Vector3 rotationVector = new Vector3(0, 0, 180 + Random.Range(-10,10));
      Vector3 directionVector = GetDirectionVector(rotationVector);
      enemyBody.Construct(_factory,directionVector, _deadLinePosition, _sharedDataService);
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
