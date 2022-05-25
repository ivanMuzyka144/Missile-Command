using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Player;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
  public class EnemyBody : MonoBehaviour, IHittable
  {
    [SerializeField] private float _velocity;

    private IGameFactory _factory;
    private EnemiesData _enemiesData;
    
    private Vector2 _direction;
    private Vector3 _deadLinePosition;
    private bool _setup;

    public void Construct(IGameFactory factory, Vector2 direction, 
      Vector3 deadLinePosition, EnemiesData enemiesData)
    {
      _factory = factory;
      _direction = direction;
      _deadLinePosition = deadLinePosition;
      _enemiesData = enemiesData;
      _setup = true;
    }
    private void Update()
    {
      if (_setup)
      {
        Move();
        TryToKill();
      }
    }

    private void Move() => 
      transform.position += (Vector3)(_direction * _velocity * Time.deltaTime);

    private void TryToKill()
    {
      if (transform.position.y <= _deadLinePosition.y) 
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.TryGetComponent(out IHittable hittable)) 
        Explode();
    }

    private void Explode()
    {
      Explosion explosion = _factory.CreateEnemyExplosion(transform.position);
      explosion.PerformExplosion();
      DestroyEnemy();
    }

    public void Hit()
    {
      _enemiesData.RecordEnemyKilledByPlayer();
      DestroyEnemy();
    }

    private void DestroyEnemy()
    {
      _enemiesData.RecordEnemyDestroyed();
      Destroy(gameObject);
    }
  }
}
