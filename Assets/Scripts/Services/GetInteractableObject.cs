using UnityEngine;

namespace Services{
  public class GetInteractableObject : IInteractableService{
    private static Camera _camera;
    
    public GameObject InteractableObject(string _target) => GetObjectWithScreen(_target);
    
    private static GameObject GetObjectWithScreen(string _target){
      if(Input.touchCount != 1) return null;
      
      var point = Input.GetTouch(index: 0).position;
      var ray = Camera.main.ScreenPointToRay(pos: point);

      if(!Physics.Raycast(origin: ray.origin, direction: ray.direction, hitInfo: out var hit)) return null;
      return hit.transform.CompareTag(tag: _target) ? hit.transform.gameObject : null;
    }
  }
}