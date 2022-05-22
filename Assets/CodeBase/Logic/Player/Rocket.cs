using CodeBase.Infrastructure.Factory;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class Rocket : MonoBehaviour, IHittable
    {
        public float Velocity;
    
        private IGameFactory _factory;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        public void SetAim(Vector3 startPos, Vector3 targetPos)
        { 
            transform.DOMove(targetPos, AnimTime(startPos, targetPos))
                .SetEase(Ease.Linear)
                .OnComplete(Explode);
            RotateRocketTo(targetPos - startPos);
        }

        private void Explode()
        {
            Explosion explosion = _factory.CreatePlayerExplosion(transform.position);
            explosion.PerformExplosion();
        
            transform.DOKill();
            Destroy(gameObject);
        }

        private float AnimTime(Vector3 startPos, Vector3 targetPos)
        {
            float distance = Vector3.Distance(startPos, targetPos);
            return distance / Velocity;
        }
    
        public void RotateRocketTo(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0,0,-90);
        }

        public void Hit() => 
            Destroy(gameObject);
    }
}
