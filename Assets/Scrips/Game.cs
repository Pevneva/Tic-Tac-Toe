using System;
using System.Collections.Generic;
using Scrips.Cell;
using Scrips.DataSaving;
using UnityEngine;

namespace Scrips
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject _cellsParent;
        [SerializeField] private GameDataViewer _dataViewer;
        [SerializeField] private SaveLoadPanel _saveLoadPanel;

        private int _currentPlayer;
        private CellController _cellController;
        private CellView[] _cellViews;
        private readonly List<CellController> _cellControllers = new List<CellController>();

        private EndGameController _endGameController;
        private CellModel _cellModel;
        private PlayerProgressSaver _playerProgressSaver;
        private PlayerProgress _playerProgress;

        public event Action<int> PlayerChanged;

        private void Awake()
        {
            _cellViews = _cellsParent.transform.GetComponentsInChildren<CellView>();

            
            _playerProgressSaver = new PlayerProgressSaver();
            _playerProgress = new PlayerProgress();

            InitStartData();
            
            _saveLoadPanel.SaveButtonClicked += OnSaveButtonClicked;
            _saveLoadPanel.LoadButtonClicked += OnLoadButtonClicked;
        }

        private void OnSaveButtonClicked() => 
            _playerProgressSaver.SaveProgress(_endGameController.GetCellStates(), _currentPlayer, _endGameController.AmountOfTurns);

        private void OnLoadButtonClicked()
        {
            _playerProgress = _playerProgressSaver.LoadProgress();
            RemoveSubscribes();
            RestartGame();
        }

        private void InitStartData()
        {
            _dataViewer.HideWinMessage();
            _dataViewer.HideLine();

            SetPlayerNumber(_playerProgress.CurrentPlayerIndex != 0 ? _playerProgress.CurrentPlayerIndex : 1);
            
            _endGameController = new EndGameController(_playerProgress?.AmountDoneTurns ?? 0);
            _endGameController.RowPassed += OnRowPassed;
            _endGameController.ColumnPassed += OnColumnPassed;
            _endGameController.DiagonalPassed += OnDiagonalPassed;

            for (int i = 0; i < _cellViews.Length; i++)
                InitCell(_cellViews[i], i);
        }

        private void OnRowPassed(int cellNumber) =>
            _dataViewer.ShowHorizontalLine(cellNumber);

        private void OnColumnPassed(int cellNumber) =>
            _dataViewer.ShowVerticalLine(cellNumber);

        private void OnDiagonalPassed(int index) =>
            _dataViewer.ShowDiagonalLine(index);

        private void InitCell(CellView cellView, int index)
        {
            if (_playerProgress?.AmountDoneTurns != 0)
            {
                Char[] states = _playerProgress.CellStateIndexes.ToCharArray();
                if (Int32.TryParse(states[index].ToString(), out int indexInt))
                    _cellModel = new CellModel(indexInt);
            }
            else
            {
                _cellModel = new CellModel();
            }

            _cellController = new CellController(_cellModel, cellView, _currentPlayer);

            _cellControllers.Add(_cellController);
            _endGameController.AddModel(_cellModel);

            PlayerChanged += _cellController.OnPlayerChanged;
            _cellController.CellClicked += OnCellClicked;
        }

        private void OnCellClicked()
        {
            if (_endGameController.IsGameEnded(_currentPlayer, out bool isCurrentPlayerWon) == false)
            {
                ChangePlayerNumber();
            }
            else
            {
                _dataViewer.ShowWinMessage(isCurrentPlayerWon, _currentPlayer);
                _playerProgress = new PlayerProgress();
                RemoveSubscribes();
                Invoke(nameof(RestartGame), 3);
            }
        }

        private void RestartGame()
        {
            ResetImages();

            _endGameController.CleanUp();

            InitStartData();
        }

        private void ResetImages()
        {
            foreach (CellController cellController in _cellControllers)
                cellController.ResetImage();
        }

        private void RemoveSubscribes()
        {
            foreach (CellController cellController in _cellControllers)
                cellController.RemoveSubscribes();
        }

        private void ChangePlayerNumber()
        {
            _currentPlayer = _currentPlayer == 1 ? 2 : 1;

            PlayerChanged?.Invoke(_currentPlayer);

            _dataViewer.ShowCurrentPlayer(_currentPlayer);
        }

        private void SetPlayerNumber(int playerNumber)
        {
            _currentPlayer = playerNumber;
            
            PlayerChanged?.Invoke(_currentPlayer);

            _dataViewer.ShowCurrentPlayer(_currentPlayer);
        }
    }
}