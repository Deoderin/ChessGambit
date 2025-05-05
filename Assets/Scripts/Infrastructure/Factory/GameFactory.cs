using System.Collections.Generic;
using Common.Extensions;
using Data.Setting;
using GameElements;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Level;
using Logic;
using Logic.Goal;
using Services.PersistentProgress;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAsset _asset;
    private readonly IBoardServices _boardServices;
    private Dictionary<Vector2Int, CellEntity> _cells = new();
    public List<ISaveProgressReader> ProgressReaders { get; } = new();
    public List<ISaveProgress> ProgressesWriters { get; } = new();
    Dictionary<Vector2Int, CellEntity> IGameFactory.GetAllCells => _cells;

    public GameFactory(IAsset asset, IBoardServices boardServices)
    {
      _asset = asset;
      _boardServices = boardServices;
    }

    public IGetStatus GetStatusCell(Vector2Int _pos) => _cells[_pos];
    public ISetCellStatus SetEntityCell(Vector2Int _pos) => _cells[_pos];
    public IGetEntity GetEntityInCell(Vector2Int _pos) => _cells[_pos];

    public List<CellIdentity> GetAvailableCell(Vector2Int _pos, ChessType _type)
    {
      var availableCellObj = new List<CellIdentity>();
      foreach (var a in GetAvailableCellPosition(_pos, _type))
      {
        if (a.OccupantType == CellOccupantType.Enemy || a.OccupantType == CellOccupantType.Empty)
        {
          availableCellObj.Add(_cells[a.Position].GetCell()); 
        }
      }
      return availableCellObj;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressesWriters.Clear();
    }

    public GameObject Create(GameObject _at, string _path) => InstantiateRegistered(_path, _at.transform.position);

    public void CreateMatrixCell()
    {
      GenerateCells();
    }
    
    public void SpawnLevel(LevelData data)
    {
      foreach (var unit in data.playerFigures)
      {
        CreateFigure(unit.position, unit.type, ColorSide.White);
      }
      
      foreach (var obs in data.obstacles)
      {
        CreateObstacle(obs.position, obs.type);
      }

      if (data.hasBoss)
      {
        CreateFigure(new Vector2Int(4, 7), data.bossType, ColorSide.Black);
      }

      SetupKillGoal(data);
      SetupCellGoal(data);
    }

    private void SetupKillGoal(LevelData data)
    {
      foreach (var figure in data.enemyFigures)
      {
        var enemyGO = CreateChessPiece(figure.position, figure.type);
        
        SetupAnimationComponentChess(enemyGO);
        InitChessSetting(enemyGO, figure.position, figure.type, ColorSide.Black);
        SetStatusCell(figure.position, enemyGO);

        if (!figure.isKillObjective) continue;

        if (!enemyGO.TryGetComponent(out KillToComplete kill))
          enemyGO.AddComponent<KillToComplete>();
        LevelEvents.RegisterRequiredEnemy(enemyGO.GetComponent<KillToComplete>());
      }
    }


    private void SetupCellGoal(LevelData data)
    {
      foreach (var goal in data.goalCells)
      {
        var cell = _cells[goal.position].GetCell();
        if (!goal.isLevelEndPoint) continue;
        
        if (!cell.TryGetComponent(out LevelGoalTrigger trigger))
          trigger = cell.gameObject.AddComponent<LevelGoalTrigger>();

        AllServices.Container.Single<IGoalTriggerService>().RegisterTrigger(trigger);
      }
    }

    private void CreateObstacle(Vector2Int _position, ObstacleType _type)
    {
      string prefabPath = _type switch
      {
        ObstacleType.Rock => AssetPath.ObstaclePath.Rock,

        _ => null
      };

      if (string.IsNullOrEmpty(prefabPath))
      {
        Debug.LogError($"Path for obstacle type {_type} not found");
        return;
      }

      GameObject obstacleGO = InstantiateRegistered(prefabPath, _cells[_position].GetCell().transform.position);
      Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
      obstacle.PositionOnBoard = _position;
      obstacle.Type = _type;

      SetEntityCell(_position).SetObstacle(obstacleGO);
    }

    private void CreateFigure(Vector2Int position, ChessType type, ColorSide side)
    {
      GameObject chessGO = CreateChessPiece(position, type);
      if (chessGO == null)
      {
        Debug.LogError($"Не найден префаб для фигуры {type} в позиции {position}");
        return;
      }
      
      SetupAnimationComponentChess(chessGO);
      InitChessSetting(chessGO, position, type, side);
      SetStatusCell(position, chessGO);
    }

    private void InitChessSetting(GameObject chess, Vector2Int initialPos, ChessType chessType, ColorSide side)
    {
      var chessObject = chess.GetComponent<Chess>();
      
      chessObject
        .With(_a => _a.ChessType = chessType)
        .With(_a => _a.PositionOnBoard = initialPos)
        .With(_a => _a.Side = side);
      
      chessObject.SetupChess();
    }


    private void SetupAnimationComponentChess(GameObject _chess)
    {
      _chess.GetComponent<AnimationChess>().StartupAnimation();
    }

    private void SetStatusCell(Vector2Int _startPosition, GameObject _cell)
    {
      SetEntityCell(_startPosition).SetChess(_cell.GetComponent<Chess>());
    }

    private List<AvailableCellInfo> GetAvailableCellPosition(Vector2Int _pos, ChessType _type) => _type switch
    {
      ChessType.King => _boardServices.AvailableCellForKing(_pos),
      ChessType.Rook => _boardServices.AvailableCellForRock(_pos),
      ChessType.Bishop => _boardServices.AvailableCellForBishop(_pos),
      ChessType.Queen => _boardServices.AvailableCellForQueen(_pos),
      ChessType.Knight => _boardServices.AvailableCellForKnight(_pos),
      ChessType.Pawn => _boardServices.AvailableCellForPawn(_pos),
      _ => null
    };

    private GameObject CreateChessPiece(Vector2Int _initialPos, ChessType _type) => _type switch
    {
      ChessType.King => _asset.Instantiate(AssetPath.ChessPath.KingPath,
        _cells[_initialPos].GetCell().transform.position),
      ChessType.Rook => _asset.Instantiate(AssetPath.ChessPath.RockPath,
        _cells[_initialPos].GetCell().transform.position),
      ChessType.Bishop => _asset.Instantiate(AssetPath.ChessPath.BishopPath,
        _cells[_initialPos].GetCell().transform.position),
      ChessType.Queen => _asset.Instantiate(AssetPath.ChessPath.QueenPath,
        _cells[_initialPos].GetCell().transform.position),
      ChessType.Knight => _asset.Instantiate(AssetPath.ChessPath.KnightPath,
        _cells[_initialPos].GetCell().transform.position),
      ChessType.Pawn => _asset.Instantiate(AssetPath.ChessPath.PawnPath,
        _cells[_initialPos].GetCell().transform.position),
      _ => null
    };

    private void GenerateCells()
    {
      var cells = _boardServices.InitialCellsColors();

      for (int x = 0; x <= IBoardServices.HeightCell; x++)
      {
        for (int y = 0; y <= IBoardServices.WidthCell; y++)
        {
          CellEntity cellEntity = new CellEntity();
          var colorCell = cells[x, y] == 0 ? ColorSide.Black : ColorSide.White;
          var cell = InstantiateRegistered(colorCell == ColorSide.Black
            ? AssetPath.BlackCellPathData
            : AssetPath.WhiteCellPathData);

          SetupPositionCell(cell, x, y);
          SetIdentityInformation(cell, colorCell, x, y);
          SetCellInEntity(cellEntity, cell);
          SetupAnimationComponent(cell);
          SetupOutlineSetting(cell);

          _cells.Add(new Vector2Int(x, y), cellEntity);
        }
      }
    }

    private void SetupPositionCell(GameObject _cell, int _x, int _y)
    {
      const float positionOffset = 2;
      _cell.transform.position = new Vector3(_x * positionOffset, 0, _y * positionOffset);
    }

    private void SetIdentityInformation(GameObject _cell, ColorSide _colorCell, int _i, int _j) =>
      _cell.GetComponent<CellIdentity>().Construct(_colorCell, new Vector2Int(_i, _j));

    private void SetCellInEntity(CellEntity _cellEntity, GameObject _cell) =>
      _cellEntity.SetCell(_cell.GetComponent<CellIdentity>());

    private void SetupAnimationComponent(GameObject _cell)
    {
      var animSetting = LoadSettingData<CellAnimationSetting>(AssetPath.SettingAnimationCell);

      _cell.GetComponent<AnimationCell>()
        .With(_aCell => _aCell.SetDataSetting(animSetting))
        .With(_aCell => _aCell.StartUpAnimation());
    }

    private void SetupOutlineSetting(GameObject _cell) => _cell.GetComponent<OutlineCell>().InitPoint();


    private GameObject InstantiateRegistered(string _prefabPath, Vector3 _at)
    {
      GameObject gameObject = _asset.Instantiate(_prefabPath, _at);
      RegisterProgressWatcher(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string _prefabPath)
    {
      var cell = CreateCell(_prefabPath);
      RegisterProgressWatcher(cell);
      return cell;
    }

    private T LoadSettingData<T>(string _path) => (T)_asset.InstantiateData(_path);

    private GameObject CreateCell(string _prefabPath)
    {
      var randomCell = Random.Range(0, 32);
      var randomCellRotation = Random.Range(0, 4);
      var gameObject = (CellPasses)_asset.InstantiateData(_prefabPath);
      var cell = _asset.Instantiate(AssetPath.CellPath);
      var meshCell
        = _asset.Instantiate(gameObject.GmCell[randomCell], Vector3.zero,
          Quaternion.Euler(0, 90 * randomCellRotation, 0));

      meshCell.transform.parent = cell.transform;
      return cell;
    }

    private void RegisterProgressWatcher(GameObject _gameObject)
    {
      foreach (ISaveProgressReader progressReader in _gameObject.GetComponentsInChildren<ISaveProgressReader>())
      {
        Register(progressReader);
      }
    }

    private void Register(ISaveProgressReader _progressReader)
    {
      if (_progressReader is ISaveProgress progressReader)
      {
        ProgressesWriters.Add(progressReader);
      }

      ProgressReaders.Add(_progressReader);
    }
  }
}