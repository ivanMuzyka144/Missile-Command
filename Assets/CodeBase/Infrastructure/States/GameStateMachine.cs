using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
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
  public class GameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
        [typeof(LoadSharedDataState)] = new LoadSharedDataState(this, services.Single<ISharedDataService>(), services.Single<IPersistentProgressService>()),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, 
          services.Single<IGameFactory>(), 
          services.Single<IPersistentProgressService>(),
          services.Single<IInputService>(), 
          services.Single<IUiFactory>(), 
          services.Single<IStaticDataService>(),
          services.Single<ISharedDataService>()),
        [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IInputService>(), 
          services.Single<IGameFactory>(), 
          services.Single<ISharedDataService>(),
          services.Single<IWindowService>()),
        [typeof(DisposableState)] = new DisposableState(this, services, services.Single<IPersistentProgressService>(), 
          services.Single<ISharedDataService>(), services.Single<ISaveLoadService>()),
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Tick()
    {
      if (_activeState is IUpdatable) 
        ((IUpdatable)_activeState).Tick();
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}