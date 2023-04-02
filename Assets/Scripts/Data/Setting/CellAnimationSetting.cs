using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Setting{
  [CreateAssetMenu(menuName = "CellAnimationSetting", fileName = "CellAnimationSetting", order = 0)]
  public class CellAnimationSetting : ScriptableObject{
    public TypeAnimationCell typeAnimationCell;
    public Ease animationTypeEase;
    [MinMaxSlider(0, 2, true)]
    public Vector2 delayRange;
    [Header("Position for dropdown animation")]
    public float dropDownPosition = -10f;
    public float baseMoveAnimTime = 1f;
    [Header("Start position for drop to spot animation")]
    public float startUpPos = 40;
    [Header("spot position")]
    public float spotPosition = 1.6f;
  }

  public enum TypeAnimationCell{
    FallFromAbove,
    ScaleUpOnSpot,
    AllTypesAnimation
  }
}