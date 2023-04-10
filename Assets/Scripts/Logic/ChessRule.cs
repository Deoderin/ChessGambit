using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Logic{
  [System.Serializable]
  public struct ChessRule{
    public ChessType chessType;

    [Header("Only selected cells or direction")]
    public bool solo;
    public List<Vector2Int> direction;
  }
}