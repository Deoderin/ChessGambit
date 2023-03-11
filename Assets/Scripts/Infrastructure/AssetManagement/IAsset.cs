using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement{
  public interface IAsset : IService{
    GameObject Instantiate(string _path);
    GameObject Instantiate(string _path, Vector3 _at);
  }
}