using Data.Setting;
using DG.Tweening;
using UnityEngine;

namespace GameElements{
  public class AnimationChess : MonoBehaviour{
    private ChessAnimationSetting _animSetting;
    public Ease ease;
    public float timeAnimation;
    
    public void SetDataSetting(ChessAnimationSetting _setting){
      _animSetting = _setting;
    }

    public void SelectedFigure(){

    }

    private void SelectedAnimationStarted(){
      
    }

    private void SelectedAnimationEnd(){
      
    }

    public void MoveTo(Vector3 pos){
      BasicMove(pos);
    }

    private void BasicMove(Vector3 pos){
      float jumpPower = 1;
      
      transform.DOMove(pos, timeAnimation).SetEase(ease);
      transform.DOJump(pos,jumpPower, 1, timeAnimation).SetEase(ease);
    }

    private void JumpingMove(Vector3 _pos, int _numJump){
      float jumpPower = 1;
      
      transform.DOJump(_pos,jumpPower, _numJump, timeAnimation).SetEase(ease);
    }
  }
}