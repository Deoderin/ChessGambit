using System.Collections;
using System.Collections.Generic;
using Common.Extensions;
using UnityEngine;

namespace Logic.Maze{
  public class Maze : MonoBehaviour{
    public MazeSetting mazeSetting;

    private MazeCell[,] _cells;

    private Vector2Int RandomCoordinates =>
      new(Random.Range(0, mazeSetting.size.x), Random.Range(0, mazeSetting.size.y));

    private bool ContainsCoordinates(Vector2Int _coordinate) =>
      _coordinate.x >= 0 && _coordinate.x < mazeSetting.size.x && _coordinate.y >= 0 &&
      _coordinate.y < mazeSetting.size.y;
    private MazeCell GetCell(Vector2Int _coordinates) => _cells[_coordinates.x, _coordinates.y];

    public IEnumerator Generate(){
      WaitForSeconds delay = new WaitForSeconds(mazeSetting.generationStepDelay);
      _cells = new MazeCell[mazeSetting.size.x, mazeSetting.size.y];
      List<MazeCell> activeCells = new List<MazeCell>();
      DoFirstGenerationStep(activeCells);
      while(activeCells.Count > 0){
        yield return delay;
        DoNextGenerationStep(activeCells);
      }
    }

    private void DoFirstGenerationStep(List<MazeCell> _activeCells) =>
      _activeCells.Add(CreateCell(RandomCoordinates));

    private void DoNextGenerationStep(List<MazeCell> activeCells){
      int currentIndex = activeCells.Count - 1;
      MazeCell currentCell = activeCells[currentIndex];
      if(currentCell.IsFullyInitialized){
        activeCells.RemoveAt(currentIndex);
        return;
      }

      MazeDirection direction = currentCell.RandomUninitializedDirection;
      Vector2Int coordinates = currentCell.coordinates + direction.ToIntVector2();
      if(ContainsCoordinates(coordinates)){
        MazeCell neighbor = GetCell(coordinates);
        if(neighbor == null){
          neighbor = CreateCell(coordinates);
          CreatePassage(currentCell, neighbor, direction);
          activeCells.Add(neighbor);
        } else{
          CreateWall(currentCell, neighbor, direction);
        }
      } else{
        CreateWall(currentCell, null, direction);
      }
    }

    private MazeCell CreateCell(Vector2Int _coordinates) =>
      _cells[_coordinates.x, _coordinates.y] =
        Instantiate(mazeSetting.cellPrefab)
         .With(_a => _a.coordinates = _coordinates)
         .With(_a => _a.name = "Maze Cell " + _coordinates.x + ", " + _coordinates.y)
         .With(_a => _a.transform.parent = transform)
         .With(_a => _a.transform.localPosition 
                 = new Vector3(_coordinates.x - mazeSetting.size.x * 0.5f + 0.5f, 0f, _coordinates.y - mazeSetting.size.y * 0.5f + 0.5f));

    private void CreatePassage(MazeCell _cell, MazeCell _otherCell, MazeDirection _direction){
      Instantiate(mazeSetting.passagePrefab).Initialize(_cell, _otherCell, _direction);
      Instantiate(mazeSetting.passagePrefab).Initialize(_otherCell, _cell, _direction.GetOpposite());
    }

    private void CreateWall(MazeCell _cell, MazeCell _otherCell, MazeDirection _direction){
      Instantiate(mazeSetting.wallPrefab).Initialize(_cell, _otherCell, _direction);
      if(_otherCell == null) return;
      Instantiate(mazeSetting.wallPrefab).Initialize(_otherCell, _cell, _direction.GetOpposite());
    }
  }
}
