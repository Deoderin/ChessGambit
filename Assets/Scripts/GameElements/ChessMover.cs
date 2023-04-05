using UnityEngine;

namespace GameElements{
  public class ChessMover : MonoBehaviour{
    private AnimationChess _animator;
    
    private void Start(){
      _animator = GetComponent<AnimationChess>();
    }

    public void SetPosition(Vector3 _pos){
      transform.position = _pos;
    }
  }
}