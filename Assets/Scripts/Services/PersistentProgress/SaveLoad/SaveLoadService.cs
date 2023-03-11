using Data;
using Infrastructure.Factory;
using UnityEngine;

namespace Services.PersistentProgress.SaveLoad{
  public class SaveLoadService : ISaveLoadService{
    private readonly IPersistentProgressServices _progressServices;
    private readonly IGameFactory _gameFactory;
    private const string ProgressKey = "Progress";

    public SaveLoadService(IPersistentProgressServices _progressServices, IGameFactory _gameFactory){
      this._progressServices = _progressServices;
      this._gameFactory = _gameFactory;
    }
    
    public PlayerProgress LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    public void SaveProgress(){
      foreach(var progressesWriter in _gameFactory.ProgressesWriters){
        progressesWriter.UpdateProgress(_progressServices.Progress);
      }  
      
      PlayerPrefs.SetString(ProgressKey, _progressServices.Progress.ToJson());
    }
  }
}