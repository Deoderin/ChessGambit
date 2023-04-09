using System.Collections.Generic;
using GameElements;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services;
using UnityEngine;

namespace Main{
  public class Player : MonoBehaviour{
    private const string TagChess = "Chess";
    private const string TagCell = "Cell";

    private IInteractableService _interactorService;
    private IGameFactory _gameFactory;
    private List<GameObject> _availableCell = new();
    private Chess _currentFigure;
    private CellIdentity _currentCell;
    private bool _isTap;


    private void Start(){
      _interactorService = AllServices.Container.Single<IInteractableService>();
      _gameFactory = AllServices.Container.Single<IGameFactory>();
    }

    private void Update(){
      switch(Input.touchCount){
        case 1 when !_isTap:
          _isTap = true;
          
          ChooseCell();
          ChooseFigure();
          ActivateAvailableCell();
          SetTargetFigureCell();
          break;
        case 0:
          _isTap = false;
          break;
      }
    }

    private void ChooseCell(){
      if(!_currentFigure) return;
      _currentCell = _interactorService?.InteractableObject(TagCell)?.GetComponentInParent<CellIdentity>();
    }

    private void ActivateAvailableCell(){
      foreach(var cell in _availableCell){
        cell.GetComponent<CellIdentity>().EnableAvailableState();
      }
    }

    private void DisableAvailableCell(){
      foreach(var cell in _availableCell){
        cell.GetComponent<CellIdentity>().DisableAvailableState();
      }
    }

    private void ChooseFigure(){
      if(_interactorService.InteractableObject(TagChess) == null) return;

      _currentFigure = _interactorService.InteractableObject(TagChess).GetComponentInParent<Chess>();
      _availableCell = _gameFactory.GetAvailableCell(_currentFigure.PositionInBoard, _currentFigure.ChessType);
    }

    private void SetTargetFigureCell(){
      if(_availableCell.Count > 0 && _currentFigure != null && _currentCell != null){

        if(_currentCell.Available){
          _currentFigure.GetComponent<ChessMover>().SetPosition(_currentCell.transform.position);
          _currentFigure.PositionInBoard = _currentCell.PositionOnBoard;
          DisableAvailableCell();
        }
      }
    }
  }
}