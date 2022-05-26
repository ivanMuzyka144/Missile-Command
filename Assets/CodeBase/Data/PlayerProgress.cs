using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public PlayerStats Stats;
    public HouseData[] HouseDataArray;

    public bool IsHouseDataInited() => 
      HouseDataArray != null && HouseDataArray.Length != 0;
  }
}