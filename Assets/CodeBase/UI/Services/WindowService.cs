using System;
using CodeBase.UI.Factory;
using CodeBase.UI.Window;

namespace CodeBase.UI.Services
{

  public class WindowService : IWindowService
  {
    private readonly IUiFactory _uiFactory;

    public WindowService(IUiFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }

    public WindowBase Open(WindowId windowId)
    {
      switch (windowId)
      {
        case WindowId.Unknown:
          break;
        case WindowId.LevelCompleted:
          return _uiFactory.CreateLevelCompletedWindow();
        case WindowId.GameOver:
          return _uiFactory.CreateGameOverWindow();
      }
      return null;
    }
  }
}