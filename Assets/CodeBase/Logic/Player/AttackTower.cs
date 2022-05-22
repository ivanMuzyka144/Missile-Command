using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Logic.Player
{
   public class AttackTower : MonoBehaviour, IHittable
   {
      [SerializeField] private Transform _firePoint;
      [SerializeField] private Transform _canonTransform;

      private IGameFactory _factory;
      private IInputService _inputService;

      private float _reloadTime = 0.25f;
      private bool _canFire;
   
   
      public void Construct(IGameFactory factory)
      {
         _factory = factory;
         _canFire = true;
      }
      
      public void Shoot(Vector2 mousePosition)
      {
         if (!_canFire)
            return;
      
         Rocket rocket = _factory.CreateRocket(_firePoint.position);
         rocket.SetAim(_firePoint.position, mousePosition.FromScreenToWorld());
      
         StartCoroutine(CO_ReloadTimer());
      }

      private IEnumerator CO_ReloadTimer()
      {
         _canFire = false;
         yield return new WaitForSeconds(_reloadTime);
         _canFire = true;
      }

      public void RotateCanonTo(Vector2 mousePosition)
      {
         Vector3 canonPosInScreen = Camera.main.WorldToScreenPoint(_canonTransform.position);
         Vector3 lookPos = mousePosition - canonPosInScreen.ToVector2();
         float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
         _canonTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0,0,-90);
      }

      public void Hit()
      {
      
      }
   }
}