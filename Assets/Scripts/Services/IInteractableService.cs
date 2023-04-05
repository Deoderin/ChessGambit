using Infrastructure.Services;
using UnityEngine;

namespace Services{
  public interface IInteractableService : IService{
    GameObject InteractableObject(string _tag);
  }
}