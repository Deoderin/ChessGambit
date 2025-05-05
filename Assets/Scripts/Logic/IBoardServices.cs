using System.Collections.Generic;
using GameElements;
using Infrastructure.Services;
using UnityEngine;

namespace Logic{
  public interface IBoardServices : IService{
    const int HeightCell = 7;
    const int WidthCell = 7;
    int[,] InitialCellsColors();
    List<AvailableCellInfo> AvailableCellForPawn(Vector2Int _currentCell);
    List<AvailableCellInfo> AvailableCellForBishop(Vector2Int _currentCell);
    List<AvailableCellInfo> AvailableCellForRock(Vector2Int _currentCell);
    List<AvailableCellInfo> AvailableCellForKnight(Vector2Int _currentCell);
    List<AvailableCellInfo> AvailableCellForQueen(Vector2Int _currentCell);
    List<AvailableCellInfo> AvailableCellForKing(Vector2Int _currentCell);
    List<AvailableCellInfo> AvailableCellForChess(Chess chess);
  }
}