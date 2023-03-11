using Data;
using Infrastructure.Services;

namespace Services.PersistentProgress.SaveLoad{
  public interface ISaveLoadService : IService{
    PlayerProgress LoadProgress();
    
    void SaveProgress();
  }
}