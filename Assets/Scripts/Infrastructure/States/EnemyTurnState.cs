using GameElements;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using UnityEngine;

namespace Infrastructure.States
{
    public class EnemyTurnState : GameLoopState.TurnStateMachine.ITurnState
    {
        private GameLoopState.TurnStateMachine _machine;
        
        private readonly IGameFactory _factory = AllServices.Container.Single<IGameFactory>();
        private readonly IBoardServices _board = AllServices.Container.Single<IBoardServices>();

        public void Enter(GameLoopState.TurnStateMachine machine)
        {
            _machine = machine;
            Debug.Log("Ход врага");

            // Простейший AI
            ExecuteOneMove();

            _machine.Enter<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Конец хода врага");

        private void ExecuteOneMove()
        {
            var factory = AllServices.Container.Single<IGameFactory>();
            var board = AllServices.Container.Single<IBoardServices>();

            foreach (var cellEnemy in factory.GetAllCells)
            {
                var fromCell = cellEnemy.Value;

                if (!fromCell.ThereChess()) continue;

                var chess = fromCell.GetChess();
                if (chess.Side != ColorSide.Black) continue;
                
                
                var targets = board.AvailableCellForChess(chess);
                if (targets.Count == 0) continue;

                var toPos = targets[0];

                MoveEnemy(chess, toPos);

                break;
            }
        }
        
        private void MoveEnemy(Chess chess, Vector2Int target)
        {
            _factory.SetEntityCell(chess.PositionOnBoard).SetChess(null);
            chess.PositionOnBoard = target;
            
            _factory.SetEntityCell(target).SetChess(chess);
            chess.GetComponent<ChessMover>().SetPosition(target);
        }
    }
}