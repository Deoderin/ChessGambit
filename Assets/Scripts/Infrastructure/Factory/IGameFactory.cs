using System.Collections.Generic;
using GameElements;
using Infrastructure.Services;
using Level;
using Logic;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory{
  public interface IGameFactory : IService{
    GameObject Create(GameObject _at, string _path);
    List<ISaveProgressReader> ProgressReaders{get;}
    List<ISaveProgress> ProgressesWriters{get;}
    Dictionary<Vector2Int, CellEntity> GetAllCells { get; }
    List<CellIdentity> GetAvailableCell(Vector2Int _pos, ChessType _type);

    void Cleanup();
    void CreateMatrixCell();

    IGetStatus GetStatusCell(Vector2Int _pos);
    ISetCellStatus SetEntityCell(Vector2Int _pos);
    IGetEntity GetEntityInCell(Vector2Int _pos);
    void SpawnLevel(LevelData data);
  }
} 