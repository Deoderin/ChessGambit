using UnityEngine;

namespace GameElements{
  public class OutlineCell : MonoBehaviour{
    [SerializeField]
    private GameObject outline;
    
    public void EnableOutline() => outline.SetActive(true);
    public void DisableOutline() => outline.SetActive(false);
  }
}