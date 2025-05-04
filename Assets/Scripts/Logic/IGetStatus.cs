using GameElements;

namespace Logic{
  public interface IGetStatus{
    public bool ThereAreObstacles();
    public bool ThereChess(); 
    public Chess GetChess();
  }
}