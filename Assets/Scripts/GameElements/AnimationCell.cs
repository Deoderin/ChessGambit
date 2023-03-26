using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameElements{
  public class AnimationCell : MonoBehaviour{
    private const float InitPos = 1.6f;    
    private const float DropDown = -10f;
    private const float AnimTime = 1f;
    
    public void InitPosition(Vector3 _pos) => transform.position = _pos;

    public void MoveDropDown() => transform.DOMoveY(DropDown, AnimTime).SetEase(Ease.OutQuad);

    public void MoveStartPosition() => StartCoroutine(MoveStartPositionDelay());

    private IEnumerator MoveStartPositionDelay(){
      var delay = Random.Range(0.01f, 1.2f);
      yield return new WaitForSeconds(delay);
      transform.DOMoveY(InitPos, AnimTime).SetEase(Ease.OutQuad);
    }
  }
}
