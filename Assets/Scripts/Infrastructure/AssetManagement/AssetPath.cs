namespace Infrastructure.AssetManagement
{
  public static class AssetPath
  {
    public const string CellPath = "GameComponent/Cell";
    public const string BlackCellPathData = "BlackCell";
    public const string WhiteCellPathData = "WhiteCell";
    public const string ChessRules = "Setting/ChessRule";
    public const string SettingAnimationCell = "Setting/CallAnimationSetting";

    public static class ChessPath
    {
      public const string KingPath = "GameComponent/King";
      public const string PawnPath = "GameComponent/Pawn";
      public const string BishopPath = "GameComponent/Bishop";
      public const string KnightPath = "GameComponent/Knight";
      public const string QueenPath = "GameComponent/Queen";
      public const string RockPath = "GameComponent/Rock";
    }

    public static class Levels
    {
      public const string Level = "Level_2";
    }

    public class ObstaclePath
    {
      public const string Rock = "Environment/Rock";
    }
  }
}