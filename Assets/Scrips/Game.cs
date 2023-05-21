using System;
using System.Collections.Generic;
using Scrips.Cell;
using Scrips.DataSaving;
using TMPro;
using UnityEngine;

namespace Scrips
{
    public class Game : MonoBehaviour
    {
        private const string PlayerProgressKey = "PlayerProgress";

        [SerializeField] private GameObject _cellsParent;
        [SerializeField] private CurrentPlayerView _currentPlayerView;
        [SerializeField] private LineView _lineView;
        [SerializeField] private TextMeshProUGUI _winText;

        private int _currentPlayer;
        private CellController _cellController;
        private CellView[] _cellViews;
        private readonly List<CellController> _cellControllers = new List<CellController>();

        private EndGameController _endGameController;
        private PlayerProgress _playerProgress;
        private CellModel _cellModel;
        private Char[] _loadedCellStateIndexes;
        private int _loadedPlayerNumber;
        private int _loadedAmountOfTurns;

        public event Action<int> PlayerChanged;

        private void Awake()
        {
            _cellViews = _cellsParent.transform.GetComponentsInChildren<CellView>();

            LoadProgress();

            InitStartData();
        }

        private void InitStartData()
        {
            HideWinMessage();
                
            SetPlayerNumber(_loadedPlayerNumber != 0 ? _loadedPlayerNumber : 1);

            _lineView.HideLine();

            _playerProgress = new PlayerProgress();

            _endGameController = new EndGameController(_loadedAmountOfTurns);

            _endGameController.RowPassed += OnRowPassed;
            _endGameController.ColumnPassed += OnColumnPassed;
            _endGameController.DiagonalPassed += OnDiagonalPassed;

            for (int i = 0; i < _cellViews.Length; i++)
                InitCell(_cellViews[i], i);
        }

        private void OnRowPassed(int cellNumber) =>
            _lineView.ShowHorizontalLine(cellNumber);

        private void OnColumnPassed(int cellNumber) =>
            _lineView.ShowVerticalLine(cellNumber);

        private void OnDiagonalPassed(int index) =>
            _lineView.ShowDiagonalLine(index);

        private void InitCell(CellView cellView, int index)
        {
            if (_loadedCellStateIndexes != null)
            {
                if (Int32.TryParse(_loadedCellStateIndexes[index].ToString(), out int indexInt))
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
                SaveProgress();
            }
            else
            {
                ShowWinMessage(isCurrentPlayerWon);
                ClearProgress();
                RemoveSubscribes();
                Invoke(nameof(RestartGame), 3);
            }
        }

        private void ShowWinMessage(bool isCurrentPlayerWon)
        {
            _winText.gameObject.SetActive(true);
            
            if (isCurrentPlayerWon)
                _winText.text = "Player " + _currentPlayer + " WON !!!";
            else
                _winText.text = "DRAW";
        }

        private void HideWinMessage() => _winText.gameObject.SetActive(false);

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

            _currentPlayerView.RenderCurrentPlayer(_currentPlayer);
        }

        private void SetPlayerNumber(int playerNumber)
        {
            _currentPlayer = playerNumber;

            PlayerChanged?.Invoke(_currentPlayer);

            _currentPlayerView.RenderCurrentPlayer(_currentPlayer);
        }


        private void SaveProgress()
        {
            _playerProgress.CellStateIndexes = _endGameController.GetCellStates();
            _playerProgress.CurrentPlayerIndex = _currentPlayer;
            _playerProgress.AmountDoneTurns = _endGameController.AmountOfTurns;

            SaveManager.Save(PlayerProgressKey, _playerProgress);
        }

        private void LoadProgress()
        {
            PlayerProgress playerProgress = SaveManager.Load<PlayerProgress>(PlayerProgressKey);

            _loadedPlayerNumber = playerProgress.CurrentPlayerIndex;
            _loadedCellStateIndexes = playerProgress.CellStateIndexes?.ToCharArray();
            _loadedAmountOfTurns = playerProgress.AmountDoneTurns;
        }

        private void ClearProgress()
        {
            _loadedCellStateIndexes = null;
            _loadedPlayerNumber = 0;
            _loadedAmountOfTurns = 0;

            SaveManager.ResetData(PlayerProgressKey);
        }
    }
}