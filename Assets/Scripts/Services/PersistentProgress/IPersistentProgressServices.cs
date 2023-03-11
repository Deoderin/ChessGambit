using Data;
using Infrastructure.Services;

namespace Services.PersistentProgress{
  public interface IPersistentProgressServices : IService{
    public PlayerProgress Progress{get;set;}
  }
}