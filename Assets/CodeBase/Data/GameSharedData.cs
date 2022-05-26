namespace CodeBase.Data
{
  public class GameSharedData
  {
    public EnemiesData EnemiesData { get; }
    public TowersData TowersData { get; }
    
    public HouseDataDictionary HouseDataDictionary { get; }

    public GameSharedData()
    {
      EnemiesData = new EnemiesData();
      TowersData = new TowersData();
      HouseDataDictionary = new HouseDataDictionary();
    }

    public void Clear()
    {
      EnemiesData.Clear();
      TowersData.Clear();
      HouseDataDictionary.Clear();
    }
  }
}