using GameElements;
using UnityEngine;

namespace Logic
{
  public class CellEntity : IGetStatus, ISetCellStatus, IGetEntity
  {
    private CellIdentity _cell;
    private Chess _chess;
    private GameObject _obstacle;

    public CellIdentity GetCell() => _cell;
    public Chess GetChess() => _chess;
    public GameObject GetObstacle() => _obstacle;

    public void SetCell(CellIdentity cell) => _cell = cell;
    public void SetChess(Chess chess) => _chess = chess;
    public void SetObstacle(GameObject obstacle) => _obstacle = obstacle;

    public bool ThereAreObstacles() => _obstacle != null;
    public bool ThereChess() => _chess != null;
  }
}