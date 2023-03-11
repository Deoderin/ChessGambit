using UnityEngine;

namespace Data{
  public static class DataExtensions{
    public static Vector3Data AsVectorData(this Vector3 _vector) => 
      new Vector3Data(_vector.x, _vector.y, _vector.z);

    public static Vector3 AsUnityVector(this Vector3Data _vector3Data) =>
      new Vector3(_vector3Data.x, _vector3Data.y, _vector3Data.z);

    public static string ToJson(this object _obj) => JsonUtility.ToJson(_obj);

    public static Vector3 AddY(this Vector3 _vector, float _y){
      _vector.y += _y;
      return _vector;
    }
    
    public static T ToDeserialized<T>(this string _json) => JsonUtility.FromJson<T>(_json);
  }
}