using System;

namespace Data{
  [Serializable]
  public class Vector3Data{
    public float x;
    public float y;
    public float z;

    public Vector3Data(float _x, float _y, float _z){
      x = _x;
      y = _y;
      z = _z;
    }
  }
}