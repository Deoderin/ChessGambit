using UnityEngine;

namespace Logic.Maze{
  public class MazeCell : MonoBehaviour{
    public Vector2Int coordinates = Vector2Int.zero;
    private readonly MazeCellEdge[] _edges = new MazeCellEdge[MazeDirections.Count];
    private int _initializedEdgeCount;
    public bool IsFullyInitialized => _initializedEdgeCount == MazeDirections.Count;

    public MazeDirection RandomUninitializedDirection{
      get{
        var skips = Random.Range(0, MazeDirections.Count - _initializedEdgeCount);
        for(int i = 0; i < MazeDirections.Count; i++){
          if(_edges[i] != null) continue;
          if(skips == 0) return (MazeDirection)i;
          
          skips -= 1;
        }

        throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
      }
    }

    public void SetEdge(MazeDirection _direction, MazeCellEdge _edge){
      _edges[(int)_direction] = _edge;
      _initializedEdgeCount += 1;
    }
  }
}