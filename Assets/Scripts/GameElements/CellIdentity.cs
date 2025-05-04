using UnityEngine;

namespace GameElements{
  public class CellIdentity : MonoBehaviour{
    public Vector2Int PositionOnBoard {get;private set;}
    public ColorSide ColorSide {get;private set;}
    public bool Available {get;private set;}
    private OutlineCell _outlineCell;

    private void Start(){
      _outlineCell = GetComponent<OutlineCell>();
    }

    public void EnableAvailableState(){
      Available = true;
      _outlineCell.EnableOutline();
    }
    
    public void DisableAvailableState(){
      Available = false;
      _outlineCell.DisableOutline();
    }

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