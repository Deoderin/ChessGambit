using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.PersistentProgress.SaveLoad;

namespace Infrastructure.States{
    public class GameStateMachine{
        private readonly Dictionary<Type, IExitableState> _state;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader _sceneLoader, AllServices _services){
            _state = new Dictionary<Type, IExitableState>(){
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader, _services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, _sceneLoader, _services.Single<IGameFactory>(), _services.Single<IPersistentProgressServices>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _services.Single<IPersistentProgressServices>(), _services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState{
            IState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload _payload) where TState : class, IPayloadedState<TPayload>{
            TState state = ChangeState<TState>();
            state.Enter(_payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState{
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState{
            return _state[typeof(TState)] as TState;
        }
    }
}