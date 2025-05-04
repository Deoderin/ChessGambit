using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(menuName = "RogueChess/Level Data", fileName = "NewLevelData")]
    public class LevelData : ScriptableObject
    {
        [Header("ID")]
        public string levelId;
        public string displayName;

        [Header("Players figures")]
        public List<ChessPlacement> playerFigures;

        [Header("Enemy figures")]
        public List<ChessPlacement> enemyFigures;

        [Header("Boss (optional)")]
        public bool hasBoss;
        public ChessType bossType;
        public Vector2Int bossPosition = new Vector2Int(4, 7);

        [Header("Target")]
        public LevelGoal goal;
        public int surviveTurns;
        
        [Header("Obstacles")]
        public List<ObstaclePlacement> obstacles;
        
        public List<CellGoal> goalCells;
    }

    [System.Serializable]
    public class ChessPlacement
    {
        public ChessType type;
        public Vector2Int position;
        public bool isKillObjective;
    }
    
    [System.Serializable]
    public class ObstaclePlacement
    {
        public ObstacleType type;
        public Vector2Int position;
    }
    
    [System.Serializable]
    public class CellGoal
    {
        public Vector2Int position;
        public bool isLevelEndPoint;
    }

    public enum LevelGoal
    {
        DefeatAllEnemies,
        ReachTargetCell,
        SurviveTurns
    }
}