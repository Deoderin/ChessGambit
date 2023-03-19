using UnityEngine;
using UnityEngine.Serialization;

namespace GameElements{
  public class CellIdentity : MonoBehaviour{
    public void Construct(int _id, CellColor _cellColor){
      this.GetId = _id;
      this.cellColor = _cellColor;
    }

    public int GetId{get;private set;}
    public CellColor cellColor;
  }

  public enum CellColor{
    Black,
    White
  }
}
