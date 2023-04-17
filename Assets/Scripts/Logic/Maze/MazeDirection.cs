using UnityEngine;

namespace Logic.Maze{
  public enum MazeDirection{
    North,
    East,
    South,
    West
  }

  public static class MazeDirections{
    public const int Count = 4;

    public static MazeDirection GetOpposite(this MazeDirection _direction) => Opposites[(int)_direction];
    public static Vector2Int ToIntVector2(this MazeDirection _direction) => Vectors[(int)_direction];
    public static Quaternion ToRotation(this MazeDirection _direction) => Rotations[(int)_direction];

    private static readonly Vector2Int[] Vectors ={new(0, 1), new(1, 0), new(0, -1), new(-1, 0)};

    private static readonly MazeDirection[] Opposites ={
      MazeDirection.South, MazeDirection.West, MazeDirection.North, MazeDirection.East};

    private static readonly Quaternion[] Rotations ={
      Quaternion.identity, Quaternion.Euler(0f, 90f, 0f), Quaternion.Euler(0f, 180f, 0f), Quaternion.Euler(0f, 270f, 0f)};
  }
}