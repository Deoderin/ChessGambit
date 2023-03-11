using System;
using UnityEngine.Serialization;

namespace Data{
  [Serializable]
  public class PlayerProgress{
    public PlayerProgress(string _initialLevel){
      worldData = new WorldData(_initialLevel);
    }
    
    public WorldData worldData;
  }
}