using System.Collections;
using Data;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameElements{
  public class AnimationCell : MonoBehaviour{
    private CallAnimationSetting _animSetting;
    
    public void SetDataSetting(CallAnimationSetting _setting) => _animSetting = _setting;

    public void StartUpAnimation(){
      switch(_animSetting.typeAnimationCell){
        case TypeAnimationCell.FallFromAbove:
          HeightPos(_animSetting.startUpPos);
          MoveStartPosition();
          break;
        case TypeAnimationCell.ScaleUpOnSpot:
          HeightPos(_animSetting.spotPosition);
          InitScale(Vector3.zero);
          ScaleUp();
          break;
        case TypeAnimationCell.AllTypesAnimation:
          InitScale(Vector3.zero);
          HeightPos(_animSetting.startUpPos);
          ScaleUp();
          MoveStartPosition();
          break;
      }
    }

    public void MoveDropDown()
      => transform
        .DOMoveY(_animSetting.dropDownPosition, _animSetting.baseMoveAnimTime)
        .SetEase(_animSetting.animationTypeEase);

    private void InitScale(Vector3 _scale) => transform.localScale = _scale;

    private void HeightPos(float _posY) =>
      transform.position = new Vector3(transform.position.x, _posY, transform.position.z);

    private void ScaleUp(){
      StartCoroutine(ScaleStartDelay());
    }

    private void MoveStartPosition(){
      StartCoroutine(MoveStartPositionDelay());
    }

    private IEnumerator MoveStartPositionDelay(){
      var delay = Random.Range(_animSetting.delayRange.x, _animSetting.delayRange.y);
      yield return new WaitForSeconds(delay);
      transform.DOMoveY(_animSetting.spotPosition, _animSetting.baseMoveAnimTime).SetEase(_animSetting.animationTypeEase);
    }

    private IEnumerator ScaleStartDelay(){
      var delay = Random.Range(_animSetting.delayRange.x, _animSetting.delayRange.y);
      yield return new WaitForSeconds(delay);
      transform.DOScale(1, _animSetting.baseMoveAnimTime).SetEase(_animSetting.animationTypeEase);
    }
  }
}
