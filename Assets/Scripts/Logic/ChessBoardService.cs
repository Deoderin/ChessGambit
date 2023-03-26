using System;

namespace Logic{
  public static class ChessBoardService{
    public static readonly int[,] CellBoard = new int[8, 8];
    
    public static void InitialCellsBoard(){
      var reverseCount = true;
      var blackColor = false;
      for(var j = 0; j < 8; j++){
        for(var i = reverseCount ? 0 : 7;i is >= 0 and < 8; i = reverseCount ? i + 1 : i - 1){
          CellBoard[i, j] = Convert.ToInt32(blackColor);
          blackColor = !blackColor;
        }

        reverseCount = !reverseCount;
      }
    } 
  }
}