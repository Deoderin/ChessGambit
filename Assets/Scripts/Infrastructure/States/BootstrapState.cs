using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Services;
using Services.PersistentProgress;
using Services.PersistentProgress.SaveLoad;

namespace Infrastructure.States
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

    public void Enter()
    {
      _sceneLoader.Load(Initial, _onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {

    }

    private void EnterLoadLevel()
    {
      _stateMachine.Enter<LoadProgressState>();
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IAsset>(new AssetProvider());
      _services.RegisterSingle<IPersistentProgressServices>(new PersistentProgressServices());
      _services.RegisterSingle<IBoardServices>(new ChessBoardService(AllServices.Container.Single<IAsset>()));
      _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAsset>(), AllServices.Container.Single<IBoardServices>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(AllServices.Container.Single<PersistentProgressServices>(), AllServices.Container.Single<IGameFactory>()));
      _services.RegisterSingle<IInteractableService>(new GetInteractableObject());
      _services.RegisterSingle<IGoalTriggerService>(new GoalTriggerService(AllServices.Container.Single<IGameFactory>()));
      _services.RegisterSingle<ITurnService>(new TurnService());
      _services.RegisterSingle<ICombatService>(new CombatService(AllServices.Container.Single<IGameFactory>()));
      _services.RegisterSingle<ISelectionService>(new SelectionService());
    }
  }
}