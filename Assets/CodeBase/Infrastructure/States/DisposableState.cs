using System.Linq;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class DisposableState : IPayloadedState<bool> 
  {
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;
    private readonly IPersistentProgressService _persistentProgressService;
    private readonly ISharedDataService _sharedDataService;
    private readonly ISaveLoadService _saveLoadService;

    public DisposableState(GameStateMachine stateMachine, 
      AllServices services, 
      IPersistentProgressService persistentProgressService,
      ISharedDataService sharedDataService,
      ISaveLoadService saveLoadService)
    {
      _stateMachine = stateMachine;
      _services = services;
      _persistentProgressService = persistentProgressService;
      _sharedDataService = sharedDataService;
      _saveLoadService = saveLoadService;
    }

    public void Enter(bool shouldClearProgress)
    {
      if (shouldClearProgress)
        ClearProgress();
      else
        SaveProgress();

      DisposeServices();

      _stateMachine.Enter<LoadProgressState>();
    }

    private void DisposeServices()
    {
      foreach (IService service in _services.Services)
      {
        if (service is IDisposable)
          ((IDisposable)service).Dispose();
      }
    }

    public void Exit()
    {
      
    }

    private void ClearProgress() => 
      _saveLoadService.ClearProgress();

    private void SaveProgress()
    {
      HouseData[] houseDataArray = _sharedDataService.SharedData.HouseDataDictionary.HouseDataDict.Values.ToArray();
      _persistentProgressService.Progress.HouseDataArray = houseDataArray;
      _saveLoadService.SaveProgress();
    }
  }
}