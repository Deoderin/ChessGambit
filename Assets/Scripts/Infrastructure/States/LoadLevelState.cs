using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Level;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressServices _progressServices;

    private string _levelName;
    
    public LoadLevelState(GameStateMachine _stateMachine, SceneLoader _sceneLoader, IGameFactory _gameFactory,
      IPersistentProgressServices _progressServices)
    {
      this._stateMachine = _stateMachine;
      this._sceneLoader = _sceneLoader;
      this._gameFactory = _gameFactory;
      this._progressServices = _progressServices;
    }

    public void Enter(string sceneName)
    {
      _levelName = sceneName;
      
      _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {

    }

    private void OnLoaded()
    {
      InitWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
      foreach (var progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressServices.Progress);
    }
    
    private void InitWorld()
    {
      _gameFactory.CreateMatrixCell();
      
      var levelData = Resources.Load<LevelData>($"Levels/{AssetPath.Levels.Level}");

      if (levelData == null)
      {
        Debug.LogError($"LevelData not found for scene {_levelName} in Resources/Levels/");
        return;
      }
      
      _gameFactory.SpawnLevel(levelData);
    }
  }
}