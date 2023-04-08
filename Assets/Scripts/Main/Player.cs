using GameElements;
using Infrastructure.Services;
using Services;
using UnityEngine;

namespace Main{
  public class Player : MonoBehaviour{
    private const string TagChess = "Chess";
    private const string TagCell = "Cell";

    private IInteractableService _interactorService;
    private GameObject _currentFigure;
    private GameObject _currentCell;

    private bool _isTap;
    
    private void Start(){
      _interactorService = AllServices.Container.Single<IInteractableService>();
    }

    private void Update(){
      if(Input.touchCount == 1 && !_isTap){
        _isTap = true;

        ChooseFigure();
        SetTargetFigureCell();
      } else if(Input.touchCount == 0){
        _isTap = false;
      }
    }

    private void ChooseFigure(){
      if(_interactorService.InteractableObject(TagChess) != null)
        _currentFigure = _interactorService.InteractableObject(TagChess);
    }

    private void SetTargetFigureCell(){
      if(_interactorService.InteractableObject(TagCell) != null){
        _currentCell = _interactorService.InteractableObject(TagCell);
        Debug.LogError(_currentCell.name);
      }

      if(_currentCell != null){
        Debug.LogError("KEKA");
        _currentFigure.GetComponentInParent<ChessMover>().SetPosition(_currentCell.transform.position);
      }
    }
  }
}
