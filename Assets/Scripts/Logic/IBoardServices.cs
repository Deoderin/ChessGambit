using System.Collections.Generic;
using GameElements;
using Infrastructure.Services;
using UnityEngine;

namespace Logic{
  public interface IBoardServices : IService{
    const int HeightCell = 7;
    const int WidthCell = 7;
    int[,] InitialCellsColors();
    List<Vector2Int> AvailableCellForPawn(Vector2Int _currentCell);
    List<Vector2Int> AvailableCellForBishop(Vector2Int _currentCell);
    List<Vector2Int> AvailableCellForRock(Vector2Int _currentCell);
    List<Vector2Int> AvailableCellForKnight(Vector2Int _currentCell);
    List<Vector2Int> AvailableCellForQueen(Vector2Int _currentCell);
    List<Vector2Int> AvailableCellForKing(Vector2Int _currentCell);
    List<Vector2Int> AvailableCellForChess(Chess chess);
  }
}