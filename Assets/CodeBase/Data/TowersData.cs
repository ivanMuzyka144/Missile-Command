using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace CodeBase.Data
{
  public class TowersData
  {
    private readonly Dictionary<int, int> _towerAmmoDict = new Dictionary<int, int>();

    public Action<int,int> OnTowerAmmoChanged;
    public void AddTower(int towerId, int ammoCount) => 
      _towerAmmoDict.Add(towerId, ammoCount);

    public void RemoveAmmo(int towerId)
    {
      int currAmmo = _towerAmmoDict[towerId];
      _towerAmmoDict[towerId] = currAmmo - 1;
      OnTowerAmmoChanged?.Invoke(towerId, _towerAmmoDict[towerId]);
    }

    public bool IsTowerHasAmmo(int towerId) => 
      _towerAmmoDict[towerId] != 0;

    public int GetTowerAmmo(int towerId) => 
      _towerAmmoDict[towerId];

    public bool IsAmmoEnded()
    {
      int allAmmo = 0;

      foreach (int ammo in _towerAmmoDict.Values) 
        allAmmo += ammo;

      return allAmmo == 0;
    }

    public void Clear()
    {
      if (_towerAmmoDict != null) 
        _towerAmmoDict.Clear();
    }
  }
}