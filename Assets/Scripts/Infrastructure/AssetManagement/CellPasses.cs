using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Infrastructure.AssetManagement{
  [CreateAssetMenu(fileName = "Cell", menuName = "GameObjects/Cell", order = 1)]
  public class CellPasses : ScriptableObject{
    public CellColor CellColor;
    public List<GameObject> GmCell;
  }
}