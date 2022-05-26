using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class DisposableState : IPayloadedState<bool> 
  {
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;

    public DisposableState(GameStateMachine stateMachine, AllServices services)
    {
      _stateMachine = stateMachine;
      _services = services;
    }

    public void Enter(bool shouldClearProgress)
    {
      foreach (IService service in _services.Services)
      {
        if (service is IDisposable)
          ((IDisposable)service).Dispose();
      }

      _stateMachine.Enter<LoadLevelState, string>("Main");
    }

    public void Exit()
    {
      
    }
  }
}