using UnityEngine;

namespace GameElements
{
    public class Obstacle : MonoBehaviour
    {
        public ObstacleType Type;
        public Vector2Int PositionOnBoard;
    }

    public enum ObstacleType
    {
        Rock
    }
}