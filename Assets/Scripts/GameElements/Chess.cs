using UnityEngine;

namespace GameElements
{
  public class Chess : MonoBehaviour
  {
    [SerializeField]
    private GameObject _blackChessObject;
    [SerializeField]
    private GameObject _whiteChessObject;
    
    public ChessType ChessType;

    public Vector2Int PositionOnBoard { get; set; }
    public ColorSide Side { get; set; }

    public void SetupChess()
    {
      if (Side == ColorSide.White)
      {
        _blackChessObject.SetActive(false);
        _whiteChessObject.SetActive(true);
      }
      else
      {
        _whiteChessObject.SetActive(false);
        _blackChessObject.SetActive(true);
      }
    }
  }

  public enum ChessType
  {
    King,
    Rook,
    Bishop,
    Queen,
    Knight,
    Pawn
  }
}