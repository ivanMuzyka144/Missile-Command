using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;
using CodeBase.UI.Window;
using UnityEngine;

namespace CodeBase.UI.Factory
{
  public class UiFactory : IUiFactory
  {
    private const string UiRootPath = "UI/UIRoot";
    
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;

    private Transform _uiRoot;
    private readonly IPersistentProgressService _progressService;

    public UiFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService)
    {
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
    }
    
    public void CreateUIRoot()
    {
      _uiRoot = _assets.Instantiate(UiRootPath).transform;
    }

    public WindowBase CreateLevelCompletedWindow()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.LevelCompleted);
      return Object.Instantiate(config.Prefab, _uiRoot).GetComponent<WindowBase>();
    }

    public WindowBase CreateGameOverWindow()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.GameOver);
      return Object.Instantiate(config.Prefab, _uiRoot).GetComponent<WindowBase>();
    }
  }
}