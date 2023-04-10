using GameElements;
using UnityEngine;

namespace Logic{
  public class GetCellEntity : IGetStatus, ISetCellStatus, IGetEntity{
    private CellIdentity _cell;
    private Chess _chess;
    private GameObject _obstacle;

    public CellIdentity GetCell() => _cell;
    public Chess GetChess() => _chess;
    public GameObject GetObstacle() => _obstacle;

    public void SetCell(CellIdentity _cell) => this._cell = _cell;
    public void SetChess(Chess _chess) => this._chess = _chess;
    public void SetObstacle(GameObject _obstacle) => this._obstacle = _obstacle;

    public bool ThereAreObstacles() => _obstacle != null;
    public bool ThereChess() => _chess != null;
  }
}