using Main;
using UnityEngine;

namespace Infrastructure.States
{
    public class PlayerTurnState : GameLoopState.TurnStateMachine.ITurnState
    {
        private GameLoopState.TurnStateMachine _machine;

        public void Enter(GameLoopState.TurnStateMachine machine)
        {
            _machine = machine;
            Debug.Log("Ход игрока начался");
            
            PlayerActionEvents.OnTurnEnded += OnTurnEnded;
        }

        public void Exit()
        {
            Debug.Log("Ход игрока завершён");
            PlayerActionEvents.OnTurnEnded -= OnTurnEnded;
        }

        private void OnTurnEnded()
        {
            _machine.Enter<EnemyTurnState>();
        }
    }
}