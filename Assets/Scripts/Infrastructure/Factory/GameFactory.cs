using System;
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

    public List<GameObject> GetAvailableCell(Vector2Int _pos, ChessType _type){
      var availableCellObj = new List<GameObject>();
      List<Vector2Int> availableCell = new List<Vector2Int>();
        
      switch(_type){
        case ChessType.King:
          availableCell = _boardServices.AvailableCellForKing(_pos);
          break;
        case ChessType.Rook:
          availableCell = _boardServices.AvailableCellForRock(_pos);
          break;
        case ChessType.Bishop:
          availableCell = _boardServices.AvailableCellForBishop(_pos);
          break;
        case ChessType.Queen:
          availableCell = _boardServices.AvailableCellForQueen(_pos);
          break;
        case ChessType.Knight:
          availableCell = _boardServices.AvailableCellForKnight(_pos);
          break;
        case ChessType.Pawn:
          availableCell = _boardServices.AvailableCellForPawn(_pos);
          break;
      }
      
      availableCell.ForEach(_a => availableCellObj.Add(_cells[_a]));
      return availableCellObj;
    }

    public void Cleanup(){
      ProgressReaders.Clear();
      ProgressesWriters.Clear();
    }

    public GameObject Create(GameObject _at, string _path) => InstantiateRegistered(_path, _at.transform.position);

    public void CreateMatrixCell(){
      GenerateCells();
    }

    public void SpawnChess(){
      SpawnChessSetup(Vector2Int.zero, ChessType.King);      
      SpawnChessSetup(new Vector2Int(1, 0), ChessType.Bishop);      
      SpawnChessSetup(new Vector2Int(2, 0), ChessType.Rook);      
      SpawnChessSetup(new Vector2Int(3, 0), ChessType.Knight);      
      SpawnChessSetup(new Vector2Int(4, 0), ChessType.Queen);
    }

    private void SpawnChessSetup(Vector2Int _startPosition, ChessType _chessType){
      var chess = CreateChessPiece(_startPosition);
      SetupAnimationComponentChess(chess);
      InitChessSetting(chess, _startPosition, _chessType);
    }

    private void SetupAnimationComponentChess(GameObject _chess){
      _chess.GetComponent<AnimationChess>().StartupAnimation();
    }

    private GameObject CreateChessPiece(Vector2Int _initialPos) =>
      _asset.Instantiate(AssetPath.ChessPiecePath, _cells[_initialPos].transform.position);

    private void InitChessSetting(GameObject _chess, Vector2Int _initialPos, ChessType _chessType) =>
      _chess.GetComponent<Chess>()
            .With(_a => _a.ChessType = _chessType)
            .With(_a => _a.PositionInBoard = _initialPos);
    
    
    private void GenerateCells(){
      var cells = _boardServices.InitialCellsColors();
      
      for(int x = 0; x <= IBoardServices.HeightCell; x++){
        for(int y = 0; y <= IBoardServices.WidthCell; y++){
          var colorCell =  cells[x, y] == 0 ? ColorSide.Black : ColorSide.White;
          var cell = InstantiateRegistered(colorCell == ColorSide.Black ? AssetPath.BlackCellPathData : AssetPath.WhiteCellPathData);

          SetupPositionCell(cell, x, y);
          SetIdentityInformation(cell, colorCell, x, y);
          SetupAnimationComponent(cell);
          SetupOutlineSetting(cell);
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

    private void SetupOutlineSetting(GameObject _cell) => _cell.GetComponent<OutlineCell>().InitPoint();
    

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