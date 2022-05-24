using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace CodeBase.Data
{
  public class TowersData
  {
    private readonly Dictionary<int, int> _towerAmmoDict = new Dictionary<int, int>();

    public Action<int> OnTowerAmmoChanged;
    public void AddTower(int towerId, int ammoCount) => 
      _towerAmmoDict.Add(towerId, ammoCount);

    public void RemoveAmmo(int towerId)
    {
      int currAmmo = _towerAmmoDict[towerId];
      _towerAmmoDict[towerId] = currAmmo - 1;
      OnTowerAmmoChanged?.Invoke(towerId);
    }

    public bool IsTowerHasAmmo(int towerId) => 
      _towerAmmoDict[towerId] != 0;

    public int GetTowerAmmo(int towerId) => 
      _towerAmmoDict[towerId];
  }
}