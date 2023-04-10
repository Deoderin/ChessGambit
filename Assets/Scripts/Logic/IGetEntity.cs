using GameElements;
using UnityEngine;

namespace Logic{
  public interface IGetEntity{
    public CellIdentity GetCell();
    public Chess GetChess();
    public GameObject GetObstacle();
  }
}