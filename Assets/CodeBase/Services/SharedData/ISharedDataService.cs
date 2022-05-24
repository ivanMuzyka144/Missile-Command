using CodeBase.Data;

namespace CodeBase.Services.SharedData
{
  public interface ISharedDataService: IService
  {
    GameSharedData SharedData { get; set; }
  }
}