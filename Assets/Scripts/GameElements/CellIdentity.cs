using UnityEngine;
using UnityEngine.Serialization;

namespace GameElements{
  public class CellIdentity : MonoBehaviour{
    public void Construct(ColorSide _colorSide){
      this.colorSide = _colorSide;
    }

    public int GetId{get;private set;}
    [FormerlySerializedAs("cellColor")] public ColorSide colorSide;
  }

  public enum ColorSide{
    Black = 0,
    White = 1
  }
}
