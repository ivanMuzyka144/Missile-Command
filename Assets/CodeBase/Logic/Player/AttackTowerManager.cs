using System;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Logic.Player
{
  public class AttackTowerManager : MonoBehaviour
  {
    private IGameFactory _factory;
    private IInputService _inputService;
    private TowersData _towersData;


    public void Construct(IGameFactory factory, IInputService inputService, TowersData towersData)
    {
      _factory = factory;
      _inputService = inputService;
      _towersData = towersData;
      _inputService.OnMouseMoved += MouseMoved;
      _inputService.OnMouseClicked += MouseClicked;
    }

    private void OnDestroy()
    {
      _inputService.OnMouseMoved -= MouseMoved;
      _inputService.OnMouseClicked -= MouseClicked;
    }

    private void MouseMoved(Vector2 position)
    {
      foreach (AttackTower tower in _factory.AttackTowers) 
        tower.RotateCanonTo(position);
    }

    private void MouseClicked(Vector2 mousePosition)
    {
      AttackTower tower = GetAvailableTower(mousePosition.FromScreenToWorld());
      if (tower != null)
      {
        _towersData.RemoveAmmo(tower.TowerId);
        tower.Shoot(mousePosition);
      }
    }

    private AttackTower GetAvailableTower(Vector2 position)
    {
      float nearDistance = 0;
      AttackTower returnTower= null;

      AttackTower[] towersWithAmmo = _factory.AttackTowers.Where(x => _towersData.GetTowerAmmo(x.TowerId) > 0).ToArray();

      if (towersWithAmmo.Length == 0)
      {
        return null;
      }
      
      for (var i = 0; i < towersWithAmmo.Length; i++)
      {
        AttackTower attackTower = towersWithAmmo[i];
        float distance = Vector2.Distance(position, attackTower.transform.position);
        if (i == 0)
        {
          nearDistance = distance;
          returnTower = attackTower;
        }
        else
        {
          if (nearDistance > distance)
          {
            nearDistance = distance;
            returnTower = attackTower;
          }
        }
      }

/*
      for (int i = 0; i < _factory.AttackTowers.Count; i++)
      {
        AttackTower tower = _factory.AttackTowers[i];
        float distance = Vector2.Distance(position, tower.transform.position);
        if (i == 0)
        {
          nearDistance = distance;
          returnTower = tower;
        }
        else
        {
          if (nearDistance>distance)
          {
            nearDistance = distance;
            returnTower = tower;
          }
        }
      }*/
      return returnTower;
    }

    public void SetupTowers(Vector3[] towerPositions)
    {
      for (int i = 0; i < towerPositions.Length; i++)
      {
        AttackTower attackTower = _factory.CreateAttackTower(towerPositions[i], i);
      }
    }
    
    
  }
}
