using System;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
  public class EnemyBody : MonoBehaviour
  {
    [SerializeField] private float _velocity;

    private Vector2 _direction;
    private bool _setup;

    public void Construct(Vector2 direction)
    {
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

    public void DestroyEnemy()
    {
      Destroy(gameObject);
    }
  }
}
