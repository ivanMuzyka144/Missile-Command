using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.SharedData;
using CodeBase.Services.StaticData;
using CodeBase.UI.Factory;
using CodeBase.UI.Services;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      InputService inputService = new InputService();
      StaticDataService staticDataService = new StaticDataService();
      AssetProvider assetProvider = new AssetProvider();
      PersistentProgressService progressService = new PersistentProgressService();
      
      _services.RegisterSingle<IInputService>(inputService);
      _services.RegisterSingle<IStaticDataService>(staticDataService);
      _services.RegisterSingle<IAssetProvider>(assetProvider);
      _services.RegisterSingle<IPersistentProgressService>(progressService);

      _services.RegisterSingle(SharedDataService());
      _services.RegisterSingle<IUiFactory>(new UiFactory(assetProvider,staticDataService, progressService));
      _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUiFactory>()));
      _services.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, progressService, 
        _services.Single<ISharedDataService>(), inputService));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(progressService, _services.Single<IGameFactory>()));
    }
    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadProgressState>();
    
    private ISharedDataService SharedDataService()
    {
      ISharedDataService sharedDataService = new SharedDataService();
      sharedDataService.SharedData = new GameSharedData();
      return sharedDataService;
    }

  }
}