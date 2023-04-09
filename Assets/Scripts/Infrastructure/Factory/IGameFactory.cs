using System.Collections.Generic;
using GameElements;
using Infrastructure.Services;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory{
  public interface IGameFactory : IService{
    GameObject Create(GameObject _at, string _path);
    List<ISaveProgressReader> ProgressReaders{get;}
    List<ISaveProgress> ProgressesWriters{get;}
    List<GameObject> GetAvailableCell(Vector2Int _pos, ChessType _type);
    void Cleanup();
    void CreateMatrixCell();
    void SpawnChess();
  }
} 