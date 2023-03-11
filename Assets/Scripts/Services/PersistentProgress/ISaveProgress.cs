using Data;

namespace Services.PersistentProgress{
  public interface ISaveProgress : ISaveProgressReader{
    void UpdateProgress(PlayerProgress _progress);
  }

  public interface ISaveProgressReader{
    void LoadProgress(PlayerProgress _progress);
  }
}