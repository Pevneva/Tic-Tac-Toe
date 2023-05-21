using System;
using Scrips.DataSaving;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerProgressSaver
    {
        private const string PlayerProgressKey = "PlayerProgress";

        private Char[] _loadedCellStateIndexes;
        private int _loadedPlayerNumber;
        private int _loadedAmountOfTurns;
        private PlayerProgress _playerProgress = new PlayerProgress();

        public void SaveProgress(string cellStates, int currentPlayerIndex, int amountOfDoneTurns)
        {
            _playerProgress.CellStateIndexes = cellStates;
            _playerProgress.CurrentPlayerIndex = currentPlayerIndex;
            _playerProgress.AmountDoneTurns = amountOfDoneTurns;

            SaveManager.Save(PlayerProgressKey, _playerProgress);
        }

        public PlayerProgress LoadProgress() => 
            SaveManager.Load<PlayerProgress>(PlayerProgressKey);

        public void ClearProgress()
        {
            _playerProgress.CellStateIndexes = null;
            _playerProgress.CurrentPlayerIndex = 0;
            _playerProgress.AmountDoneTurns = 0;

            SaveManager.ResetData(PlayerProgressKey);
        }
    }
}