using System;
using CodeBase.UI.Services;
using CodeBase.UI.Window;
using CodeBase.UI.Window;

namespace CodeBase.StaticData.Windows
{
  [Serializable]
  public class WindowConfig
  {
    public WindowId WindowId;
    public WindowBase Prefab;
  }
}