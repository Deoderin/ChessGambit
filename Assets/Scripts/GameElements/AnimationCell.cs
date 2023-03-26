using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameElements{
  public class AnimationCell : MonoBehaviour{
    [SerializeField]
    private Ease animationType;
    [SerializeField, MinMaxSlider(0, 2, true)]
    private Vector2 delayRange;
    private const float InitPos = 1.6f;    
    private const float DropDown = -10f;
    private const float AnimTime = 1f;
    
    public void InitPosition(Vector3 _pos) => transform.position = _pos;

    public void MoveDropDown() => transform.DOMoveY(DropDown, AnimTime).SetEase(animationType);

    public void MoveStartPosition() => StartCoroutine(MoveStartPositionDelay());

    private IEnumerator MoveStartPositionDelay(){
      var delay = Random.Range(delayRange.x, delayRange.y);
      yield return new WaitForSeconds(delay);
      transform.DOMoveY(InitPos, AnimTime).SetEase(animationType);;
    }
  }
}
