using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure{
  public class SceneLoader{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner _coroutineRunner) => 
      this._coroutineRunner = _coroutineRunner;

    public void Load(string _name, Action _onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(_name, _onLoaded));

    private IEnumerator LoadScene(string _nextScene, Action _onLoaded = null){
      if(SceneManager.GetActiveScene().name == _nextScene){
        _onLoaded?.Invoke();
        yield break;
      }
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(_nextScene);

      while(!waitNextScene.isDone)
        yield return null;

      _onLoaded?.Invoke();
    }
  }
}