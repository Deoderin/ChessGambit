using UnityEngine;

namespace GameElements
{
  public class OutlineCell : MonoBehaviour
  {
    [SerializeField] private MeshFilter outline;
    [SerializeField] private MeshFilter targetOutline;
    
    public void InitPoint()
    {
      InitialCorrectMesh();
      DisableOutline();
    }

    public void EnableOutline()
    {
      outline.gameObject.SetActive(true);
      targetOutline.gameObject.SetActive(false);
    }

    public void DisableOutline()
    {
      outline.gameObject.SetActive(false);
      targetOutline.gameObject.SetActive(false);
    }

    public void EnableTargetOutline()
    {
      targetOutline.gameObject.SetActive(true);
      outline.gameObject.SetActive(false);
    }

    private void InitialCorrectMesh()
    {
      outline.GetComponent<MeshFilter>().mesh = GetComponentsInChildren<MeshFilter>()[2].mesh;
      targetOutline.GetComponent<MeshFilter>().mesh = GetComponentsInChildren<MeshFilter>()[2].mesh;
    }
  }
}