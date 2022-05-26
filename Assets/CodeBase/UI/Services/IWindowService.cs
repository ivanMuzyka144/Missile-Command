using CodeBase.Services;
using CodeBase.UI.Window;

namespace CodeBase.UI.Services
{
  public interface IWindowService : IService
  {
    WindowBase Open(WindowId windowId);
  }

}