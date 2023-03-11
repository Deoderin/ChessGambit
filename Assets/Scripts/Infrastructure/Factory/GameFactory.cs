using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory{
  public class GameFactory : IGameFactory{
    private readonly IAsset _asset;

    public List<ISaveProgressReader> ProgressReaders{get;} = new();    
    public List<ISaveProgress> ProgressesWriters{get;} = new();
    
    public GameFactory(IAsset _asset){
      this._asset = _asset;
    }

    public void Cleanup(){
      ProgressReaders.Clear();
      ProgressesWriters.Clear();
    }

    public GameObject CreatePerson(GameObject _at) => InstantiateRegistered(AssetPath.HostPath, _at.transform.position);

    private GameObject InstantiateRegistered(string _prefabPath, Vector3 _at){
      GameObject gameObject = _asset.Instantiate(_prefabPath, _at);
      RegisterProgressWatcher(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string _prefabPath){
      GameObject gameObject = _asset.Instantiate(_prefabPath);
      RegisterProgressWatcher(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatcher(GameObject _gameObject){
      foreach(ISaveProgressReader progressReader in _gameObject.GetComponentsInChildren<ISaveProgressReader>()){
        Register(progressReader);
      }
    }

    private void Register(ISaveProgressReader _progressReader){
      if(_progressReader is ISaveProgress progressReader){
        ProgressesWriters.Add(progressReader);
      }

      ProgressReaders.Add(_progressReader);
    }
  }
}