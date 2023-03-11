namespace Data{
  public class PositionOnLevel{
    public readonly string Level;
    public Vector3Data position;
    
    public PositionOnLevel(string _level, Vector3Data _position){
      Level = _level;
      position = _position;
    }
    
    public PositionOnLevel(string _initializeLevel){
      Level = _initializeLevel;
    }
  }
}