using GameElements;
using Infrastructure.Factory;
using UnityEngine;

namespace Services
{
    public class CombatService : ICombatService
    {
        private readonly IGameFactory _factory;

        public CombatService(IGameFactory factory)
        {
            _factory = factory;
        }

        public void Attack(Chess attacker, Chess defender)
        {
            Vector2Int defenderPos = defender.PositionOnBoard;
            Vector2Int attackerPos = attacker.PositionOnBoard;
            
            Object.Destroy(defender.gameObject);
            _factory.SetEntityCell(defenderPos).SetChess(null);
            
            _factory.SetEntityCell(attackerPos).SetChess(null);
            attacker.PositionOnBoard = defenderPos;
            _factory.SetEntityCell(defenderPos).SetChess(attacker);
            
            attacker.GetComponent<ChessMover>().SetPosition(defenderPos);
        }
    }
}