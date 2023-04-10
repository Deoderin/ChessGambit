using GameElements;
using UnityEngine;

namespace Logic{
  public interface ISetCellStatus{
    public void SetCell(CellIdentity _cell);
    public void SetChess(Chess _chess);
    public void SetObstacle(GameObject _obstacle);
  }
}