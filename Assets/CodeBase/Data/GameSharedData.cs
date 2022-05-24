namespace CodeBase.Data
{
  public class GameSharedData
  {
    public EnemiesData EnemiesData { get; }
    public TowersData TowersData { get; }

    public GameSharedData()
    {
      EnemiesData = new EnemiesData();
      TowersData = new TowersData();
    }
  }
}