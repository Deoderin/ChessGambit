using UnityEngine;

namespace GameElements{
  public class TouchInteractorService : MonoBehaviour{
    private const string TagName = "Chess";
    private static GameObject _interactableObj;
    private Camera _camera;
    
    public static GameObject GetInteractableObj() => _interactableObj;

    private void Awake() => _camera = Camera.main;

    private void Update(){
      TouchToScreen();
      Debug.LogError(_interactableObj.name);
    }

    private void TouchToScreen(){
      if(Input.touchCount < 1) return;
      
      var point = Input.GetTouch(index: 0).position;
      var ray = _camera.ScreenPointToRay(pos: point);

      if(!Physics.Raycast(origin: ray.origin, direction: ray.direction, hitInfo: out var hit)) return;
      _interactableObj = hit.transform.CompareTag(tag: TagName) ? hit.transform.gameObject : null;
    }
  }
}