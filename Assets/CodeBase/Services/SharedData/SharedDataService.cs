using CodeBase.Data;

namespace CodeBase.Services.SharedData
{
  public class SharedDataService : ISharedDataService
  {
    public GameSharedData SharedData { get; set; }
  }
}