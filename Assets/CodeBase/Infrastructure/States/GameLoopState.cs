using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState, IUpdatable
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IInputService _inputService;

    public GameLoopState(GameStateMachine stateMachine, IInputService inputService)
    {
      _stateMachine = stateMachine;
      _inputService = inputService;
    }

    public void Enter() { }
    public void Exit() { }


    public void Tick()
    {
      _inputService.RecordMousePosition();
      _inputService.RecordMouseCLicked();
    }
  }
}