using UnityEngine;

namespace Logic
{
    public struct AvailableCellInfo
    {
        public Vector2Int Position;
        public CellOccupantType OccupantType;

        public AvailableCellInfo(Vector2Int pos, CellOccupantType type)
        {
            Position = pos;
            OccupantType = type;
        }
    }
}