using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Data
{
  public class HouseDataDictionary
  {
    public readonly Dictionary<int, HouseData> HouseDataDict = new Dictionary<int, HouseData>();
    public void InitHouse(int houseId)
    {
      Debug.Log("Add "+ houseId +" ToDict");
      HouseDataDict.Add(houseId, new HouseData(houseId));
    }

    public void InitFromArray(HouseData[] houseDataArray)
    {
      foreach (HouseData houseData in houseDataArray) 
        HouseDataDict.Add(houseData.HouseId, houseData);
    }
    
    public HouseData[] GetHouseDataArray()
    {
      return HouseDataDict.Values.ToArray();
    }

    public void DestroyHouse(int houseId) => 
      HouseDataDict[houseId].Destroyed = true;

    public void Clear()
    {
      if (HouseDataDict != null) 
        HouseDataDict.Clear();
    }
  }
}