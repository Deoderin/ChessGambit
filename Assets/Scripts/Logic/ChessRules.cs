using System.Collections.Generic;
using System.Linq;
using GameElements;
using UnityEngine;

namespace Logic{
  [CreateAssetMenu(menuName = "Create ChessRule", fileName = "ChessRule", order = 0)]
  public class ChessRules : ScriptableObject{
    public List<ChessRule> chessRules;

    public ChessRule? GetSettingChess(ChessType _chessType){
      foreach(var rule in chessRules.Where(_rule => _rule.chessType == _chessType)){
        return rule;
      }
      
      return null;
    }
  }
}