using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.AssetManagement{
  public class AssetProvider : IAsset{
    public GameObject Instantiate(string _path){
      var prefab = Resources.Load<GameObject>(_path);
      return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string _path, Vector3 _at){
      var prefab = Resources.Load<GameObject>(_path);
      return Object.Instantiate(prefab, _at, Quaternion.identity);
    }

    public object InstantiateData(string _path) => Resources.Load(_path);
    public GameObject Instantiate(GameObject _obj) => Object.Instantiate(_obj);    
    public GameObject Instantiate(GameObject _obj, Vector3 _at) => Object.Instantiate(_obj, _at, Quaternion.identity);    
    public GameObject Instantiate(GameObject _obj, Vector3 _at, Quaternion _quaternion) => Object.Instantiate(_obj, _at, _quaternion);
  }
}