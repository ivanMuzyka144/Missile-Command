using CodeBase.Services;
using CodeBase.UI.Window;

namespace CodeBase.UI.Factory
{
  public interface IUiFactory : IService
  {
    void CreateUIRoot();
    WindowBase CreateLevelCompletedWindow();
    WindowBase CreateGameOverWindow();
  }
}