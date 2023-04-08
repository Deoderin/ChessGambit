using UnityEngine;

namespace GameElements{
  public class CellIdentity : MonoBehaviour{
    public Vector2Int PositionOnBoard {get;private set;}
    public ColorSide ColorSide {get;private set;}

    public void Construct(ColorSide _colorSide, Vector2Int _positionOnBoard){
      ColorSide = _colorSide;
      PositionOnBoard = _positionOnBoard;
    }
  }

  public enum ColorSide{
    Black = 0,
    White = 1
  }
}
