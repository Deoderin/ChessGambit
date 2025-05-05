using System;
using System.Collections.Generic;
using Common.Extensions;
using GameElements;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Logic{
  public class ChessBoardService : IBoardServices
  {
    private readonly ChessRules _chessRules;

    public ChessBoardService(IAsset _asset)
    {
      _chessRules = (ChessRules)_asset.InstantiateData(AssetPath.ChessRules);
    }

    public int[,] InitialCellsColors()
    {
      var reverseCount = true;
      var blackColor = false;
      var cellBoard = new int[8, 8];

      for (var j = 0; j <= 7; j++)
      {
        for (var i = reverseCount ? 0 : 7; i is >= 0 and <= 7; i = reverseCount ? i + 1 : i - 1)
        {
          cellBoard[i, j] = Convert.ToInt32(blackColor);
          blackColor = !blackColor;
        }

        reverseCount = !reverseCount;
      }

      return cellBoard;
    }

    public List<AvailableCellInfo> AvailableCellForPawn(Vector2Int _currentCell)
    {
      var setting = _chessRules.GetSettingChess(ChessType.Pawn);
      return Rule(_currentCell, setting?.direction.ConvertToIntMass(), (bool)setting?.solo);
    }

    public List<AvailableCellInfo> AvailableCellForBishop(Vector2Int _currentCell)
    {
      var setting = _chessRules.GetSettingChess(ChessType.Bishop);
      return Rule(_currentCell, setting?.direction.ConvertToIntMass(), (bool)setting?.solo);
    }

    public List<AvailableCellInfo> AvailableCellForRock(Vector2Int _currentCell)
    {
      var setting = _chessRules.GetSettingChess(ChessType.Rook);
      return Rule(_currentCell, setting?.direction.ConvertToIntMass(), (bool)setting?.solo);
    }

    public List<AvailableCellInfo> AvailableCellForKing(Vector2Int _currentCell)
    {
      List<AvailableCellInfo> availableCell = new List<AvailableCellInfo>();
      var setting = _chessRules.GetSettingChess(ChessType.King);

      Rule(_currentCell, _chessRules.GetSettingChess(ChessType.Bishop)?.direction.ConvertToIntMass(),
        (bool)setting?.solo).ForEach(_a => availableCell.Add(_a));
      Rule(_currentCell, _chessRules.GetSettingChess(ChessType.Rook)?.direction.ConvertToIntMass(), (bool)setting?.solo)
        .ForEach(_a => availableCell.Add(_a));

      return availableCell;
    }

    public List<AvailableCellInfo> AvailableCellForChess(Chess chess)
    {
      return chess.ChessType switch
      {
        ChessType.King => AvailableCellForKing(chess.PositionOnBoard),
        ChessType.Rook => AvailableCellForRock(chess.PositionOnBoard),
        ChessType.Bishop => AvailableCellForBishop(chess.PositionOnBoard),
        ChessType.Queen => AvailableCellForQueen(chess.PositionOnBoard),
        ChessType.Knight => AvailableCellForKnight(chess.PositionOnBoard),
        ChessType.Pawn => AvailableCellForPawn(chess.PositionOnBoard),
        _ => new List<AvailableCellInfo>()
      };
    }

    public List<AvailableCellInfo> AvailableCellForQueen(Vector2Int _currentCell)
    {
      List<AvailableCellInfo> availableCell = new List<AvailableCellInfo>();
      var setting = _chessRules.GetSettingChess(ChessType.Queen);

      Rule(_currentCell, _chessRules.GetSettingChess(ChessType.Bishop)?.direction.ConvertToIntMass(),
        (bool)setting?.solo).ForEach(_a => availableCell.Add(_a));
      Rule(_currentCell, _chessRules.GetSettingChess(ChessType.Rook)?.direction.ConvertToIntMass(), (bool)setting?.solo)
        .ForEach(_a => availableCell.Add(_a));

      return availableCell;
    }

    public List<AvailableCellInfo> AvailableCellForKnight(Vector2Int _currentCell)
    {
      var setting = _chessRules.GetSettingChess(ChessType.Knight);
      return Rule(_currentCell, setting?.direction.ConvertToIntMass(), (bool)setting?.solo);
    }

    private List<AvailableCellInfo> Rule(Vector2Int _currentCell, int[][] _direction, bool _solo = false)
    {
      List<AvailableCellInfo> availableCells = new();

      var factory = AllServices.Container.Single<IGameFactory>();
      var selected = AllServices.Container.Single<ISelectionService>().Selected;
      
      if (selected == null)
        return availableCells;
      
      foreach (var dir in _direction)
      {
        int x = _currentCell.x + dir[0];
        int y = _currentCell.y + dir[1];

        while (x is >= 0 and < IBoardServices.HeightCell && y is >= 0 and < IBoardServices.WidthCell)
        {
          var pos = new Vector2Int(x, y);
          var cell = factory.GetStatusCell(pos);
          
          if (cell.ThereAreObstacles())
          {
            availableCells.Add(new AvailableCellInfo(pos, CellOccupantType.Obstacle));
            break;
          }
          
          if (cell.ThereChess())
          {
            var target = cell.GetChess();
            var type = target.Side == selected.Side ? CellOccupantType.Ally : CellOccupantType.Enemy;
            availableCells.Add(new AvailableCellInfo(pos, type));
            break;
          }
          
          availableCells.Add(new AvailableCellInfo(pos, CellOccupantType.Empty));

          if (_solo)
            break;

          x += dir[0];
          y += dir[1];
        }
      }

      return availableCells;
    }
  }
}