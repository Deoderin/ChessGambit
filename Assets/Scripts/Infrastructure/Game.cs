using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure{
    public class Game{
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner _coroutineRunner){
            StateMachine = new GameStateMachine(new SceneLoader(_coroutineRunner), AllServices.Container);
        }
    }
}