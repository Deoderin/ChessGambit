using System.Collections.Generic;
using Common.Extensions;
using Data.Setting;
using GameElements;
using Infrastructure.AssetManagement;
using Logic;
using Services.PersistentProgress;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Factory{
  public class GameFactory : IGameFactory{
    private readonly IAsset _asset;
    private readonly IBoardServices _boardServices;
    private Dictionary<Vector2Int, GameObject> _cells = new();
    public List<ISaveProgressReader> ProgressReaders{get;} = new();    
    public List<ISaveProgress> ProgressesWriters{get;} = new();
    
    public GameFactory(IAsset _asset, IBoardServices _boardServices){
      this._asset = _asset;
      this._boardServices = _boardServices;
    }

    public void Cleanup(){
      ProgressReaders.Clear();
      ProgressesWriters.Clear();
    }

    public GameObject Create(GameObject _at, string _path) => InstantiateRegistered(_path, _at.transform.position);

    public void CreateMatrixCell(){
      GenerateCells();
    }

    private void GenerateCells(){
      var cells = _boardServices.InitialCellsColors();
      
      for(int x = 0; x < 8; x++){
        for(int y = 0; y < 8; y++){
          var colorCell =  cells[x, y] == 0 ? ColorSide.Black : ColorSide.White;
          var cell = InstantiateRegistered(colorCell == ColorSide.Black ? AssetPath.BlackCellPathData : AssetPath.WhiteCellPathData);

          SetupPositionCell(cell, x, y);
          SetIdentityInformation(cell, colorCell, x, y);
          SetupAnimationComponent(cell);
          _cells.Add(new Vector2Int(x, y), cell);
        }
      }
    }

    private void SetupPositionCell(GameObject _cell, int _x, int _y){
      const float positionOffset = 2;
      _cell.transform.position = new Vector3(_x * positionOffset, 0, _y * positionOffset);
    }

    private void SetIdentityInformation(GameObject _cell,ColorSide _colorCell, int _i, int _j) =>
      _cell.GetComponent<CellIdentity>().Construct(_colorCell, new Vector2Int(_i, _j));

    private void SetupAnimationComponent(GameObject _cell){
      var animSetting = LoadSettingData<CellAnimationSetting>(AssetPath.SettingAnimationCell);

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