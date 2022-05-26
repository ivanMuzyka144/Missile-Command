using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;

namespace CodeBase.Infrastructure.States
{
  public class LoadSharedDataState : IState
  {
    private const string MainSceneName = "Main";
    
    private readonly GameStateMachine _gameStateMachine;
    private readonly ISharedDataService _sharedDataService;
    private readonly IPersistentProgressService _progressService;

    public LoadSharedDataState( GameStateMachine gameStateMachine, 
      ISharedDataService sharedDataService, 
      IPersistentProgressService progressService)
    {
      _gameStateMachine = gameStateMachine;
      _sharedDataService = sharedDataService;
      _progressService = progressService;
    }

    public void Enter()
    {
      SetupSharedDataFromPersitent();
      _gameStateMachine.Enter<LoadLevelState, string>(MainSceneName);
    }

    public void Exit() { }

    private void SetupSharedDataFromPersitent()
    {
      InitHouseData();
    }

    private void InitHouseData()
    {
      int howManyHouses = 4; // from shared dara

      if (_progressService.Progress.IsHouseDataInited())
        CopyHouseData(_progressService.Progress.HouseDataArray);
      else
        InitHouseData(howManyHouses);
    }

    private void InitHouseData(int howManyHouses)
    {
      for (int i = 0; i < howManyHouses; i++) 
        _sharedDataService.SharedData.HouseDataDictionary.InitHouse(i);
    }

    private void CopyHouseData(HouseData[] houseDataArray) => 
      _sharedDataService.SharedData.HouseDataDictionary.InitFromArray(houseDataArray);
  }
}