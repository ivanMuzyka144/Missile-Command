using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowConfig ForWindow(WindowId windowId);

  }
}