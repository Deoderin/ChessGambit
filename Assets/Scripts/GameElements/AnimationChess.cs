using Data.Setting;
using DG.Tweening;
using UnityEngine;

namespace GameElements
{
  public class AnimationChess : MonoBehaviour
  {
    private ChessAnimationSetting _animSetting;
    public SpawnAnimationChessPiece typeSpawnAnimation;
    public Ease baseEase;
    public Ease easeForStartUpAnimation;
    public float startupTime;
    public float timeAnimation;

    public void SetDataSetting(ChessAnimationSetting _setting)
    {
      _animSetting = _setting;
    }

    public void StartupAnimation()
    {
      switch (typeSpawnAnimation)
      {
        case SpawnAnimationChessPiece.DropDown:
          DropDownAnimation();
          break;
        case SpawnAnimationChessPiece.ScaleUpOnSpot:
          ScaleUpAnimation();
          break;
        case SpawnAnimationChessPiece.AllTypesAnimation:
          DropDownAnimation();
          ScaleUpAnimation();
          break;
      }
    }

    private void DropDownAnimation()
    {
      float offsetY = 60;
      float basePosY = transform.position.y;

      transform.position = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
      transform.DOMoveY(basePosY, startupTime).SetEase(easeForStartUpAnimation);
    }

    private void ScaleUpAnimation()
    {
      transform.localScale = Vector3.zero;
      transform.DOScale(Vector3.one, startupTime).SetEase(easeForStartUpAnimation);
    }

    public void SelectedFigure()
    {

    }

    private void SelectedAnimationStarted()
    {

    }

    private void SelectedAnimationEnd()
    {

    }

    public void MoveTo(Vector3 pos, bool ignoreY = false)
    {
      BasicMove(pos, ignoreY);
    }

    private void BasicMove(Vector3 pos, bool ignoreY = false)
    {
      if (ignoreY)
        pos.y = transform.position.y;
      
      float jumpPower = 1;

      transform.DOMove(pos, timeAnimation).SetEase(baseEase);
      transform.DOJump(pos, jumpPower, 1, timeAnimation).SetEase(baseEase);
    }

    private void JumpingMove(Vector3 _pos, int _numJump)
    {
      float jumpPower = 1;

      transform.DOJump(_pos, jumpPower, _numJump, timeAnimation).SetEase(baseEase);
    }
  }

  public enum SpawnAnimationChessPiece
  {
    DropDown,
    ScaleUpOnSpot,
    AllTypesAnimation
  }
}