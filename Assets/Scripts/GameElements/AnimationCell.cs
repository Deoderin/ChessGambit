using DG.Tweening;
using UnityEngine;

namespace GameElements{
  public class AnimationCell : MonoBehaviour{
    public void InitPosition(Vector3 _pos) => transform.position = _pos;

    public void MoveDown(){
      const float downTime = 1f;

      transform.DOMoveY(0, downTime).SetEase(Ease.OutQuad);
    }
  }
}
