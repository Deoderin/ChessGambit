using GameElements;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class EnemyTurnState : GameLoopState.TurnStateMachine.ITurnState
    {
        private GameLoopState.TurnStateMachine _machine;
        
        private readonly IGameFactory _factory = AllServices.Container.Single<IGameFactory>();
        private readonly IBoardServices _board = AllServices.Container.Single<IBoardServices>();
        private readonly ISelectionService _selectionService = AllServices.Container.Single<ISelectionService>();

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
            var combat = AllServices.Container.Single<ICombatService>();

            foreach (var kv in factory.GetAllCells)
            {
                var cell = kv.Value;
                if (!cell.ThereChess()) continue;

                var chess = cell.GetChess();
                if (chess.Side != ColorSide.Black) continue;

                var available = board.AvailableCellForChess(chess);

                foreach (var info in available)
                {
                    var entity = factory.GetEntityInCell(info.Position);
                    
                    if (entity.GetChess())
                    {
                        var target = entity.GetChess();
                        if (target.Side == ColorSide.White)
                        {
                            combat.Attack(chess, target);
                            return;
                        }
                    }
                    
                    if (!entity.GetChess() && !entity.GetObstacle())
                    {
                        factory.SetEntityCell(chess.PositionOnBoard).SetChess(null);
                        chess.PositionOnBoard = info.Position;
                        factory.SetEntityCell(info.Position).SetChess(chess);
                        chess.GetComponent<ChessMover>().SetPosition(info.Position);
                        return;
                    }
                }
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