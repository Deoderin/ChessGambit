using Data.Setting;
using UnityEngine;

namespace GameElements{
  public class AnimationChess : MonoBehaviour{
    private ChessAnimationSetting _animSetting;
    
    public void SetDataSetting(ChessAnimationSetting _setting){
      _animSetting = _setting;
    }
  }
}