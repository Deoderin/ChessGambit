using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic{
  public class ChessBoardService : IBoardServices{
    private const int HeightCell = 7;
    private const int WidthCell = 7;

    public int[,] InitialCellsColors(){
      var reverseCount = true;
      var blackColor = false;
      var cellBoard = new int[8, 8];
      
      for(var j = 0; j <= 7; j++){
        for(var i = reverseCount ? 0 : 7; i is >= 0 and <= 7; i = reverseCount ? i + 1 : i - 1){
          cellBoard[i, j] = Convert.ToInt32(blackColor);
          blackColor = !blackColor;
        }

        reverseCount = !reverseCount;
      }
      
      return cellBoard;
    }

    private List<Vector2Int> AvailableCellForPawn(Vector2Int _currentCell){
      List<Vector2Int> availableCell = new List<Vector2Int>();

      if(_currentCell.x + 1 < HeightCell)
        availableCell.Add(new Vector2Int(_currentCell.x + 1, _currentCell.y));

      return availableCell;
    }    
    
    private List<Vector2Int> AvailableCellForBishop(Vector2Int _currentCell){
      List<Vector2Int> availableCell = new List<Vector2Int>();
      int counter = 0;

      while(_currentCell.x + counter < HeightCell){
        availableCell.Add(new Vector2Int(_currentCell.x + counter, _currentCell.y));
        counter++;
      }
      
      counter = 0;
      while(_currentCell.x - counter > 0){
        availableCell.Add(new Vector2Int(_currentCell.x - counter, _currentCell.y));
        counter++;
      }
      
      counter = 0;
      while(_currentCell.y + counter < WidthCell){
        availableCell.Add(new Vector2Int(_currentCell.x, _currentCell.y + counter));
        counter++;
      }
      
      counter = 0;
      while(_currentCell.y - counter > 0){
        availableCell.Add(new Vector2Int(_currentCell.x, _currentCell.y - counter));
        counter++;
      }
      
      return availableCell;
    }

    private List<Vector2Int> AvailableCellForRock(Vector2Int _currentCell){
      List<Vector2Int> availableCell = new List<Vector2Int>();
      int counter = 0;

      while(_currentCell.x + counter < HeightCell && _currentCell.y + counter < WidthCell){
        availableCell.Add(new Vector2Int(_currentCell.x + counter, _currentCell.y + counter));
        counter++;
      }
      
      counter = 0;
      while(_currentCell.x - counter > 0 && _currentCell.y - counter > 0){
        availableCell.Add(new Vector2Int(_currentCell.x - counter, _currentCell.y - counter));
        counter++;
      }
      
      counter = 0;
      while(_currentCell.x + counter < HeightCell && _currentCell.y - counter > 0){
        availableCell.Add(new Vector2Int(_currentCell.x + counter, _currentCell.y - counter));
        counter++;
      }
      
      counter = 0;
      while(_currentCell.x - counter > 0 && _currentCell.y + counter < WidthCell){
        availableCell.Add(new Vector2Int(_currentCell.x - counter, _currentCell.y + counter));
        counter++;
      }

      return availableCell;
    }     
    
    private List<Vector2Int> AvailableCellForKnight(Vector2Int _currentCell){
      List<Vector2Int> availableCell = new List<Vector2Int>();
      
      if(_currentCell.x + 1 < HeightCell) 
        availableCell.Add(new Vector2Int(_currentCell.x + 1, _currentCell.y));
      
      if(_currentCell.x - 1 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x - 1, _currentCell.y));
      
      if(_currentCell.y + 1 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x, _currentCell.y + 1));
      
      if(_currentCell.y - 1 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x, _currentCell.y - 1));
      
      if(_currentCell.x + 1 < HeightCell && _currentCell.y + 1 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x + 1, _currentCell.y + 1));
      
      if(_currentCell.x - 1 > 0 && _currentCell.y - 1 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x - 1, _currentCell.y - 1));
      
      if(_currentCell.x + 1 < HeightCell && _currentCell.y - 1 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x + 1, _currentCell.y - 1));
      
      if(_currentCell.x - 1 > 0 && _currentCell.y + 1 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x - 1, _currentCell.y + 1));

      return availableCell;
    }     
    
    private List<Vector2Int> AvailableCellForQueen(Vector2Int _currentCell){
      List<Vector2Int> availableCell = new List<Vector2Int>();

      AvailableCellForBishop(_currentCell).ForEach(_a => availableCell.Add(_a));      
      AvailableCellForRock(_currentCell).ForEach(_a => availableCell.Add(_a));

      return availableCell;
    }    
    
    private List<Vector2Int> AvailableCellForKing(Vector2Int _currentCell){
      List<Vector2Int> availableCell = new List<Vector2Int>();

      if(_currentCell.x + 2 < HeightCell && _currentCell.y + 1 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x + 2, _currentCell.y + 1));
      
      if(_currentCell.x - 2 > 0 && _currentCell.y - 1 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x - 2, _currentCell.y - 1));
      
      if(_currentCell.x + 2 < HeightCell && _currentCell.y - 1 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x + 2, _currentCell.y - 1));
      
      if(_currentCell.x - 2 > 0 && _currentCell.y + 1 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x - 2, _currentCell.y + 1)); 
      
      if(_currentCell.x + 1 < HeightCell && _currentCell.y + 2 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x + 1, _currentCell.y + 2));
      
      if(_currentCell.x - 1 > 0 && _currentCell.y - 2 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x - 1, _currentCell.y - 2));
      
      if(_currentCell.x + 1 < HeightCell && _currentCell.y - 2 > 0) 
        availableCell.Add(new Vector2Int(_currentCell.x + 1, _currentCell.y - 2));
      
      if(_currentCell.x - 1 > 0 && _currentCell.y + 2 < WidthCell) 
        availableCell.Add(new Vector2Int(_currentCell.x - 1, _currentCell.y + 2));

      return availableCell;
    }
  }
}