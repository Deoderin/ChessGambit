using GameElements;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement{
  public interface IAsset : IService{
    object InstantiateData(string _path);
    GameObject Instantiate(string _path);
    GameObject Instantiate(string _path, Vector3 _at);
    GameObject Instantiate(GameObject _obj);
    GameObject Instantiate(GameObject _obj, Vector3 _at);
    GameObject Instantiate(GameObject _obj, Vector3 _at, Quaternion _quaternion);
  }
}