using System.Collections.Generic;
using Common.Extensions;
using Data;
using GameElements;
using Infrastructure.AssetManagement;
using Logic;
using Services.PersistentProgress;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Factory{
  public class GameFactory : IGameFactory{
    private readonly IAsset _asset;
    private List<GameObject> _cells = new List<GameObject>();
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
      ChessBoardService.InitialCellsBoard();
      GenerateCells();
    }

    private void GenerateCells(){
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          var colorCell = ChessBoardService.CellBoard[i, j] == 0 ? CellColor.Black : CellColor.White;
          var cell = InstantiateRegistered(colorCell == CellColor.Black ? AssetPath.BlackCellPathData : AssetPath.WhiteCellPathData);

          SetupPositionCell(cell, i, j);
          SetupAnimationComponent(cell);
          _cells.Add(cell);
        }
      }
    }

    private void SetupPositionCell(GameObject _cell, int _x, int _y){
      const float positionOffset = 2;
      _cell.transform.position = new Vector3(_x * positionOffset, 0, _y * positionOffset);
    }

    private void SetupAnimationComponent(GameObject _cell){
      var animSetting = LoadSettingData<CallAnimationSetting>(AssetPath.SettingAnimationCell);

      _cell.GetComponent<AnimationCell>()
           .With(_aCell => _aCell.SetDataSetting(animSetting))
           .With(_aCell => _aCell.StartUpAnimation());
    }

    private GameObject InstantiateRegistered(string _prefabPath, Vector3 _at){
      GameObject gameObject = _asset.Instantiate(_prefabPath, _at);
      RegisterProgressWatcher(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string _prefabPath){
      var cell = CreateCell(_prefabPath);
      RegisterProgressWatcher(cell);
      return cell;
    }

    private T LoadSettingData<T>(string _path) => (T)_asset.InstantiateData(_path);
    
    private GameObject CreateCell(string _prefabPath){
      var randomCell = Random.Range(0, 32);      
      var randomCellRotation = Random.Range(0, 4);
      
      var gameObject = (CellPasses)_asset.InstantiateData(_prefabPath);
      var cell = _asset.Instantiate(AssetPath.CellPath);
      var meshCell
        = _asset.Instantiate(gameObject.GmCell[randomCell], Vector3.zero, Quaternion.Euler(0,90 * randomCellRotation,0));

      meshCell.transform.parent = cell.transform;
      return cell;
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