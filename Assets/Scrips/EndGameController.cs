using System;
using System.Collections.Generic;
using Scrips.Cell;
using Scrips.DataSaving;

namespace Scrips
{
    public class EndGameController
    {
        private int _size = 3;
        private readonly List<CellModel> _cellModels = new List<CellModel>();
        
        public int AmountOfTurns { get; private set; }

        public event Action<int> RowPassed;
        public event Action<int> ColumnPassed;
        public event Action<int> DiagonalPassed;

        public EndGameController(int amountOfTurns) => 
            AmountOfTurns = amountOfTurns;

        public void AddModel(CellModel cellModel) => 
            _cellModels.Add(cellModel);

        public bool IsGameEnded(int currentPlayer, out bool isCurrentPlayerWon)
        {
            isCurrentPlayerWon = false;

            if (IsRowPassed(currentPlayer) || IsColumnPassed(currentPlayer) || IsDiagonalPassed(currentPlayer))
            {
                isCurrentPlayerWon = true;
                return true;
            }

            if (IsAmountOfTurnsFinished()) return true;

            return false;
        }

        public void CleanUp() => _cellModels.Clear();

        public string GetCellStates() => 
            _cellModels.StatesToString();

        private bool IsAmountOfTurnsFinished()
        {
            AmountOfTurns++;

            if (AmountOfTurns >= _size * _size)
                return true;

            return false;
        }

        private bool IsRowPassed(int currentPlayer)
        {
            for (int i = 0; i < _size * _size; i += _size)
            {
                if (_cellModels[i].State == currentPlayer && _cellModels[i + 1].State == currentPlayer &&
                    _cellModels[i + 2].State == currentPlayer)
                {
                    RowPassed?.Invoke(i);
                    return true;
                }
            }

            return false;
        }

        private bool IsColumnPassed(int currentPlayer)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_cellModels[i].State == currentPlayer && _cellModels[i + _size].State == currentPlayer &&
                    _cellModels[i + _size * 2].State == currentPlayer)
                {
                    ColumnPassed?.Invoke(i);
                    return true;
                }
            }

            return false;
        }

        private bool IsDiagonalPassed(int currentPlayer)
        {
            if (_cellModels[0].State == currentPlayer && _cellModels[_size + 1].State == currentPlayer &&
                _cellModels[_size * 2 + 2].State == currentPlayer)
            {
                DiagonalPassed?.Invoke(1);
                return true;
            }

            if (_cellModels[2 * _size].State == currentPlayer && _cellModels[_size + 1].State == currentPlayer &&
                _cellModels[2].State == currentPlayer)
            {
                DiagonalPassed?.Invoke(2);
                return true;
            }

            return false;
        }
    }
}