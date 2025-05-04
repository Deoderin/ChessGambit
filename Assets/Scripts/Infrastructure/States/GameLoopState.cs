using Logic.Goal;
using Main;
using Services;
using UnityEngine;

namespace Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly TurnStateMachine _turnMachine = new();
    
    private readonly ITurnService _turnService;

    public GameLoopState(GameStateMachine stateMachine, ITurnService turnService)
    {
      _stateMachine = stateMachine;
      _turnService = turnService;
    }

    public void Enter()
    {
      LevelEvents.OnLevelCompleted += CompleteLevel;
      _turnMachine.Enter<PlayerTurnState>();
    }

    public void Exit()
    {
      LevelEvents.OnLevelCompleted -= CompleteLevel;
      LevelEvents.Clear();
    }

    private void CompleteLevel()
    {
       Debug.LogError("Level completed");
    }

    public class TurnStateMachine
    {
      private ITurnState _activeState;

      public void Enter<TState>() where TState : ITurnState, new()
      {
        _activeState?.Exit();

        _activeState = new TState();
        _activeState.Enter(this);
      }

      public void ExitCurrent()
      {
        _activeState?.Exit();
        _activeState = null;
      }
      
      public interface ITurnState
      {
        void Enter(TurnStateMachine machine);
        void Exit();
      }
    }
  }
}