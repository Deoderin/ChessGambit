using UnityEngine;
using UnityEngine.Serialization;

namespace GameElements{
  public class CellIdentity : MonoBehaviour{
    public void Construct(CellColor _cellColor){
      this.cellColor = _cellColor;
    }

    public int GetId{get;private set;}
    public CellColor cellColor;
  }

  public enum CellColor{
    Black = 0,
    White = 1
  }
}
