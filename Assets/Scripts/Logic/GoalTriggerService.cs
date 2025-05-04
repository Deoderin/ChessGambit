using System.Collections.Generic;
using GameElements;
using Infrastructure.Factory;
using Logic.Goal;

namespace Logic
{
    public class GoalTriggerService : IGoalTriggerService
    {
        private readonly List<LevelGoalTrigger> _triggers = new();
        private readonly IGameFactory _factory;

        public GoalTriggerService(IGameFactory factory)
        {
            _factory = factory;
        }

        public void RegisterTrigger(LevelGoalTrigger trigger)
        {
            _triggers.Add(trigger);
        }

        public void ClearTriggers()
        {
            _triggers.Clear();
        }

        public void CheckAllTriggers()
        {
            foreach (var trigger in _triggers)
            {
                var pos = trigger.transform.GetComponent<CellIdentity>().PositionOnBoard;
                var cell = _factory.GetStatusCell(pos);
                
                if (cell.ThereChess())
                {
                    var chess = cell.GetChess();
                    if (chess.Side == ColorSide.White)
                        trigger.TryTrigger(chess);
                }
            }
        }
    }
}