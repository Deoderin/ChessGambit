using UnityEngine;

namespace GameElements{
  public class ChessMover : MonoBehaviour{
    private AnimationChess _animator;
    
    private void Start(){
      _animator = GetComponent<AnimationChess>();
    }

    public void SetPosition(Vector3 _pos){
      _animator.MoveTo(_pos);
    }    
    
    public void SetPosition(Vector2Int _pos){
      _animator.MoveTo(new Vector3(_pos.x * 2, 0, _pos.y * 2), ignoreY: true);
    }
  }
}