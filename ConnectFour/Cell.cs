using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Cell
    {

        Boolean playable;  

        //Map key = direction and value = Region - call made by a cell to its observers so that observers can check if the cell's state affects its heuristic function
        SortedDictionary<int, HashSet<Cell>> observers;

        //map key = direction and value = List<Cell> -  for observer cells so that an observer cell can t
        SortedDictionary<int, Region> connectCells;

        public enum CellState
        {
            empty = 0,
            red = 1,
            black = 2,
        }

        //The state of the cell - empty, black, red
        private CellState state;

        //The temporary state used in the minimax tree to determine moves
        private CellState tempState;

        public Cell()
        {
            state = CellState.empty;
        }

        public void AddConnectedCells(SortedDictionary<int, Region> r)
        {
            connectCells = r;
        }

        public void setState(int s)
        {
            if(state == CellState.empty)
                state = (CellState)s;
        }

        public CellState getState()
        {
            return state;
        }

        public Boolean isPlayable()
        {
            return playable;
        }

        public void isPlayable(Boolean p)
        {
            playable = p;
        }

    }
}
