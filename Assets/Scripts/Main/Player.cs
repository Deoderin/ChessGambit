using System.Collections.Generic;
using GameElements;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services;
using UnityEngine;

namespace Main
{
  public class Player : MonoBehaviour
  {
    private const string TagChess = "Chess";
    private const string TagCell = "Cell";

    private IInteractableService _interactorService;
    private IGameFactory _gameFactory;
    private List<CellIdentity> _availableCell = new();
    private Chess _currentFigure;
    private CellIdentity _currentCell;
    private bool _isTap;

    private void Start()
    {
      _interactorService = AllServices.Container.Single<IInteractableService>();
      _gameFactory = AllServices.Container.Single<IGameFactory>();
    }

    private void Update()
    {
      switch (Input.touchCount)
      {
        case 1 when !_isTap:
          _isTap = true;

          DisableAvailableCell();
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

    private void ChooseCell()
    {
      if (!_currentFigure) return;
      _currentCell = _interactorService.InteractableObject(TagCell)?.GetComponentInParent<CellIdentity>();
    }

    private void ActivateAvailableCell()
    {
      if (_availableCell == null) return;
      foreach (var cell in _availableCell)
      {
        cell.EnableAvailableState();
      }
    }

    private void DisableAvailableCell()
    {
      if (_availableCell == null) return;
      foreach (var cell in _availableCell)
      {
        cell.DisableAvailableState();
      }
    }

    private void ChooseFigure()
    {
      if (_interactorService.InteractableObject(TagChess) == null) return;

      _currentFigure = _interactorService.InteractableObject(TagChess).GetComponentInParent<Chess>();
      _availableCell = _gameFactory.GetAvailableCell(_currentFigure.PositionOnBoard, _currentFigure.ChessType);
    }

    private void SetTargetFigureCell()
    {
      if (ConditionForMovingFigure()) return;
      if (!_currentCell.Available) return;

      _gameFactory.SetEntityCell(_currentFigure.PositionOnBoard).SetChess(null);
      _currentFigure.GetComponent<ChessMover>().SetPosition(_currentCell.transform.position);
      _currentFigure.PositionOnBoard = _currentCell.PositionOnBoard;
      MoveComplete();
    }

    private bool ConditionForMovingFigure() =>
      _availableCell is not { Count: > 0 } || _currentFigure == null || _currentCell == null;

    private void MoveComplete()
    {
      _gameFactory.SetEntityCell(_currentCell.PositionOnBoard).SetChess(_currentFigure);
      
      DisableAvailableCell();
      
      _currentCell = null;
      _currentFigure = null;
      _availableCell = null;
      
      PlayerActionEvents.RaiseTurnEnded();
    }
  }
}