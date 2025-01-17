using Infrastructure.Factory;
using Services.PersistentProgress;

namespace Infrastructure.States{
  public class LoadLevelState : IPayloadedState<string>{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressServices _progressServices;

    public LoadLevelState(GameStateMachine _stateMachine, SceneLoader _sceneLoader, IGameFactory _gameFactory, IPersistentProgressServices _progressServices){
      this._stateMachine = _stateMachine;
      this._sceneLoader = _sceneLoader;
      this._gameFactory = _gameFactory;
      this._progressServices = _progressServices;
    }
        
    public void Enter(string _sceneName){
      _gameFactory.Cleanup();
      _sceneLoader.Load(_sceneName, OnLoaded);
    }

    public void Exit(){
      
    }

    private void OnLoaded(){
      InitWorld();
      InformProgressReaders();
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders(){
      foreach(var progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressServices.Progress);
    }

    private void InitWorld(){
      _gameFactory.CreateMatrixCell();
      _gameFactory.SpawnChess();
    }
  }
}