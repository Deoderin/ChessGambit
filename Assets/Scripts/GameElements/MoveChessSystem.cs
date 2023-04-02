using UnityEngine;

namespace GameElements{
  public abstract class MoveChessSystem : MonoBehaviour{
    private AnimationChess _animatorChess;

    public abstract void MoveTo(int _heightSpotCell, int _widthSpotCell);

    public void ConnectComponent(){
      _animatorChess = GetComponent<AnimationChess>();
    }
  }

  public class BishopMove : MoveChessSystem{
    public override void MoveTo(int _heightSpotCell, int _widthSpotCell){
      
    }
  }
}