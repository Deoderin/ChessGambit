using System;
using Data;
using Services.PersistentProgress;
using Services.PersistentProgress.SaveLoad;

namespace Infrastructure.States{
  public class LoadProgressState : IState{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressServices _persistentProgress;
    private readonly ISaveLoadService _saveLOadService;

    public LoadProgressState(GameStateMachine _gameStateMachine, IPersistentProgressServices _persistentProgress, ISaveLoadService _saveLOadService){
      this._gameStateMachine = _gameStateMachine;
      this._persistentProgress = _persistentProgress;
      this._saveLOadService = _saveLOadService;
    }

    public void Enter(){
      LoadProgressOrInitNew();
      _gameStateMachine.Enter<LoadLevelState, string>(_persistentProgress.Progress.worldData.PositionOnLevel.Level);
    }

    public void Exit(){
    }

    private void LoadProgressOrInitNew(){
      _persistentProgress.Progress = _saveLOadService.LoadProgress() ?? NewProgress();
    }

    private PlayerProgress NewProgress(){
      return new PlayerProgress("Game");
    }
  }
}