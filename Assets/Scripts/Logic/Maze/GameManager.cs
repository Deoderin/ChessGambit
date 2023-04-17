using UnityEngine;

namespace Logic.Maze{
  public class GameManager : MonoBehaviour{
    public Maze mazePrefab;

    private Maze _mazeInstance;

    private void Start(){
      BeginGame();
    }

    private void Update(){
      if(Input.GetKeyDown(KeyCode.Space)){
        RestartGame();
      }
    }

    private void BeginGame(){
      _mazeInstance = Instantiate(mazePrefab);
      StartCoroutine(_mazeInstance.Generate());
    }

    private void RestartGame(){
      StopAllCoroutines();
      Destroy(_mazeInstance.gameObject);
      BeginGame();
    }
  }
}