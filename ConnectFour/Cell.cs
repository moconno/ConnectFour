using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Cell
    {
        int rowPos;

        int colPos;

        Boolean playable;  

        //Map key = direction and value = Region - call made by cell to its observers so that observers can check if the cell's state affects its heuristic function
        SortedDictionary<int, HashSet<Cell>> observers;

        //map key = direction and value = List<Cell> -  Used to tell if connect R has been made 
        SortedDictionary<int, List<Cell>> connectCells;

        //The state of the cell - empty, black, red
        private CellState state;

        public enum CellState
        {
            empty = 0,
            red = 1,
            black = 2,
        }

        //Copy Constructor
        public Cell(Cell cell)
        {
            rowPos = cell.rowPos;
            colPos = cell.colPos;
            playable = cell.isPlayable();
            state = cell.state;
            observers = cell.observers;
            connectCells = cell.connectCells; 
        }

        public Cell(int r, int c)
        {
            rowPos = r;
            colPos = c;
            state = CellState.empty;
            connectCells = new SortedDictionary<int, List<Cell>>();
            observers = new SortedDictionary<int, HashSet<Cell>>();
        }

        public void AddConnectedCells(int direction, List<Cell> region)
        {
            if(!connectCells.ContainsKey(direction))
                connectCells.Add(direction, region);
        }

        //The cell adds itself to its connected cells observer list and the connected cells add the cell to their observer lists
        public void UpdateObservers()
        {
            foreach(KeyValuePair<int, List<Cell>> c in connectCells)
            {
                foreach(Cell cell in c.Value)
                {
                    //Adds connectCells to observer list
                    this.AddObserver(c.Key, cell); 
                    //Adds itself to connectCells
                    if (c.Key < 4)
                        cell.AddObserver(c.Key + 4, this);
                    else
                        cell.AddObserver(c.Key - 4, this);
                }
            }
        }

        public void AddObserver(int index, Cell cell)
        {

            if (observers.ContainsKey(index))
            {
                observers[index].Add(cell);
            }
            else
            {
                HashSet<Cell> c = new HashSet<Cell>();
                c.Add(cell);
                observers.Add(index, c);
            }

        }

        public int getRow()
        {
            return rowPos;
        }

        public int getColumn()
        {
            return colPos;
        }

        public void setState(int s)
        {
            if(state == CellState.empty)
            {
                state = (CellState)s;
            }
            playable = false;
                
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
