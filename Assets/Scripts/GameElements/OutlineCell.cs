using UnityEngine;

namespace GameElements{
  public class OutlineCell : MonoBehaviour{
    [SerializeField]
    private GameObject outline;

    public void InitPoint(){
      InitialCorrectMesh();
      DisableOutline();
    }

    private void InitialCorrectMesh(){
      outline.GetComponent<MeshFilter>().mesh = GetComponentsInChildren<MeshFilter>()[1].mesh;
    }
    
    public void EnableOutline() => outline.SetActive(true);
    public void DisableOutline() => outline.SetActive(false);
  }
}