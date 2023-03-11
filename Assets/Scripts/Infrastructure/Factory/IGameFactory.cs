using System.Collections.Generic;
using Infrastructure.Services;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory{
  public interface IGameFactory : IService{
    GameObject CreatePerson(GameObject _at);
    List<ISaveProgressReader> ProgressReaders{get;}
    List<ISaveProgress> ProgressesWriters{get;}
    void Cleanup();
  }
} 