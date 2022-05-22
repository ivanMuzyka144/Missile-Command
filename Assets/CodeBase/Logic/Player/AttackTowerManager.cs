using System;
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

    public void Construct(IGameFactory factory, IInputService inputService)
    {
      _factory = factory;
      _inputService = inputService;
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
      tower.Shoot(mousePosition);
    }

    private AttackTower GetAvailableTower(Vector2 position)
    {
      float nearDistance = 0;
      AttackTower returnTower= null;

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
      }
      return returnTower;
    }

    public void SetupTowers(Vector3[] towerPositions)
    {
      foreach (Vector3 position in towerPositions) 
        _factory.CreateAttackTower(position);
    }
    
    
  }
}