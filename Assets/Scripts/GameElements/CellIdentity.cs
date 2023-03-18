using UnityEngine;

namespace GameElements{
  public class CellIdentity : MonoBehaviour{
    public void Construct(int _id){
      this.GetId = _id;
    }

    public int GetId{get;private set;}
  }
}
