using System.Collections.Generic;
using GameElements;
using Infrastructure.AssetManagement;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory{
  public class GameFactory : IGameFactory{
    private const int NumCell = 64;
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

    public GameObject Create(GameObject _at, string _path) => InstantiateRegistered(_path, _at.transform.position);

    public void CreateMatrixCell(){
      for(var i = 0; i < NumCell; i++){
        if((i / 2) % 1 == 0){
          InstantiateRegistered(AssetPath.BlackCellPath).GetComponent<CellIdentity>().Construct(i);
          Debug.LogError("AssetPath.BlackCellPath   " + i);
          continue;
        }
        
        Debug.LogError("AssetPath.WhiteCellPath   " + i);
        InstantiateRegistered(AssetPath.WhiteCellPath).GetComponent<CellIdentity>().Construct(i);
      }
    }
    
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