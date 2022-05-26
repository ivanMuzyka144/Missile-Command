using CodeBase.Data;

namespace CodeBase.Services.SharedData
{
  public class SharedDataService : ISharedDataService, IDisposable
  {
    public GameSharedData SharedData { get; set; }


    public void Dispose() => 
      SharedData.Clear();
  }
}