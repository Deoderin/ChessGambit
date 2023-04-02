using GameElements;
using UnityEngine;

namespace Services{
  public class GetInteractableObject : IInteractableService{
    public GameObject InteractableObject() => TouchInteractorService.GetInteractableObj();
  }
}