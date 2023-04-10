using UnityEngine;

namespace GameElements{
  public class Chess : MonoBehaviour{
    public ChessType ChessType;
    public ColorSide ChessColor;

    public Vector2Int PositionOnBoard{get;set;}
  }

  public enum ChessType{
    King,
    Rook,
    Bishop,
    Queen,
    Knight,
    Pawn
  }
}