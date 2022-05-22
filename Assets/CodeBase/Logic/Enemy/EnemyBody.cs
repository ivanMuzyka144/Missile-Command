using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
  public class EnemyBody : MonoBehaviour, IHittable
  {
    [SerializeField] private float _velocity;

    private IGameFactory _factory;
    
    private Vector2 _direction;
    private bool _setup;

    public void Construct(IGameFactory factory, Vector2 direction)
    {
      _factory = factory;
      _direction = direction;
      _setup = true;
    }
    private void Update()
    {
      if(_setup)
        Move();
    }

    private void Move() => 
      transform.position += (Vector3)(_direction * _velocity * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.TryGetComponent(out IHittable hittable))
      {
        Explosion explosion = _factory.CreateEnemyExplosion(transform.position);
        explosion.PerformExplosion();
        
        DestroyEnemy();
      }
    }

    public void Hit() => DestroyEnemy();

    private void DestroyEnemy() => Destroy(gameObject);
  }
}
