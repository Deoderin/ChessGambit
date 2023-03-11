using UnityEngine;

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
  }
}