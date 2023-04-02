using System.Collections.Generic;
using GameElements;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.AssetManagement{
  [CreateAssetMenu(fileName = "Cell", menuName = "GameObjects/Cell", order = 1)]
  public class CellPasses : ScriptableObject{
    [FormerlySerializedAs("CellColor")] public ColorSide colorSide;
    public List<GameObject> GmCell;
  }
}