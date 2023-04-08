using Infrastructure.Services;

namespace Logic{
  public interface IBoardServices : IService{
    int[,] InitialCellsColors();
  }
}