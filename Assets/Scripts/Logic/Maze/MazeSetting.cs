using UnityEngine;

namespace Logic.Maze{
  [CreateAssetMenu(menuName = "Create MazeSetting", fileName = "MazeSetting", order = 0)]
  public class MazeSetting : ScriptableObject{
    public float generationStepDelay;
    public Vector2Int size;
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
  }
}