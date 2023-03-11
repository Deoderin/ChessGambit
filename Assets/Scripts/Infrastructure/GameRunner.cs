using UnityEngine;

namespace Infrastructure{
  public class GameRunner : MonoBehaviour{
    public GameBootstrapper bootstrapperPrefab;
    
    private void Awake(){
      var bootstrapper = FindObjectOfType<GameBootstrapper>();

      if(!bootstrapper){
        Instantiate(bootstrapperPrefab);
      }
    }
  }
}