using System;

namespace Data{
  [Serializable]
  public class WorldData{
    public WorldData(string _initializeLevel){
      PositionOnLevel = new PositionOnLevel(_initializeLevel);
    }
    
    public PositionOnLevel PositionOnLevel;
  }
}