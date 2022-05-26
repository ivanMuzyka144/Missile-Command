using System;

namespace CodeBase.Data
{
  [Serializable]
  public class HouseData
  {
    public int HouseId;
    public bool Destroyed;

    public HouseData(int houseId)
    {
      HouseId = houseId;
    }
  }
}