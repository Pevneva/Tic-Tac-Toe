using System;

namespace Scrips.Cell
{
    public class CellModel
    {
        public int State { get; private set; }
        public CellModel() => State = 0;
        
        public CellModel(int cellState) => State = cellState;

        public event Action<int> StateChanged;


        public void ChangeState(int state)
        {
            State = state;
            
            StateChanged?.Invoke(State);
        }
    }
}