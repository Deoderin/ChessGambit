using Infrastructure.Services;

namespace Logic{
  public interface IBoardServices : IService{
    const int HeightCell = 7;
    const int WidthCell = 7;
    int[,] InitialCellsColors();
  }
}