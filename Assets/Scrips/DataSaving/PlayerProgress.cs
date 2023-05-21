using System;

namespace Scrips.DataSaving
{
    [Serializable]
    public class PlayerProgress
    {
        public int CurrentPlayerIndex;

        public string CellStateIndexes;

        public int AmountDoneTurns;
    }
}