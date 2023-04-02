using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services;
using Services.PersistentProgress;
using Services.PersistentProgress.SaveLoad;
using UnityEngine;

namespace Infrastructure.States{
  public class BootstrapState : IState{
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
    public BootstrapState(GameStateMachine _stateMachine, SceneLoader _sceneLoader, AllServices _services){
      this._stateMachine = _stateMachine;
      this._sceneLoader = _sceneLoader;
      this._services = _services;
      
      RegisterServices();
    }

    public void Enter(){
      _sceneLoader.Load(Initial, _onLoaded: EnterLoadLevel);
    }

    public void Exit(){

    }

    private void EnterLoadLevel(){
      _stateMachine.Enter<LoadProgressState>();
    }

    private void RegisterServices(){
      _services.RegisterSingle<IInputService>(InputService());
      _services.RegisterSingle<IAsset>(new AssetProvider());
      _services.RegisterSingle<IPersistentProgressServices>(new PersistentProgressServices());
      _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAsset>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(AllServices.Container.Single<PersistentProgressServices>(), AllServices.Container.Single<IGameFactory>()));
      _services.RegisterSingle<IInteractableService>(new GetInteractableObject());
    }

    private static IInputService InputService(){
      if(Application.isEditor){
        return new InputService();
      } else{
        return new InputService();
      }
    }
  }
}