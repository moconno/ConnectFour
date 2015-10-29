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

        public Cell()
        {

        }

        //Copy Constructor
        public Cell(Cell cell)
        {
            rowPos = cell.rowPos;
            colPos = cell.colPos;
            playable = cell.isPlayable();
            state = cell.state;
            connectCells = new SortedDictionary<int, List<Cell>>();
            observers = new SortedDictionary<int, HashSet<Cell>>();
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
            //Cells that are not the same state(red or black) should not be added to connectCells
            Boolean addCells = true;

            //Shouldn't add a region twice
            if(!connectCells.ContainsKey(direction))
            {
                foreach (Cell cell in region)
                {

                    //Don't add cells from a region if they contain two different colors
                    if(cell.state == CellState.red)
                    {
                        if(this.state == CellState.black)
                        {
                            addCells = false;
                            break;
                        }
                        
                    }

                    else if(cell.state == CellState.black)
                    {
                        if(this.state == CellState.red)
                        {
                            addCells = false;
                            break;
                        }
                    }         
                }

                if (addCells)
                    connectCells.Add(direction, region);
            }           
                
        }

        //The cell adds itself to its connected cells observer list and the connected cells add the cell to their observer lists
        public void UpdateObservers()
        {
            foreach(KeyValuePair<int, List<Cell>> c in connectCells)
            {
                foreach(Cell cell in c.Value)
                {
                    
                        //Adds connected cell to observer list
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
                if(cell.state.Equals(CellState.empty))
                {
                    if(cell.isPlayable())
                    {
                        observers[index].Add(cell);
                        return;
                    }
                }

                if (this.state.Equals(CellState.empty))
                {
                    //this giving me problems - not evaluating correclty
                    //if(cell.isPlayable())
                    {
                        observers[index].Add(cell);
                        return;
                    }
                }

                if(cell.state.Equals(this.state))
                {
                    observers[index].Add(cell);
                    return;
                }
                
            }

            //Dictionary Doesn't contain key
            else
            {
                if (cell.state.Equals(CellState.empty))
                {
                    //this giving me problems - not evaluating correctly
                    if(cell.isPlayable())
                    {
                        HashSet<Cell> c = new HashSet<Cell>();
                        c.Add(cell);
                        observers.Add(index, c);
                        return;
                    }
                }

                if (this.state.Equals(CellState.empty))
                {
                    //this giving me problems - not evaluating correctly
                    //if (cell.isPlayable())
                    {
                        HashSet<Cell> c = new HashSet<Cell>();
                        c.Add(cell);
                        observers.Add(index, c);
                        return;
                    }
                }

                if (cell.state.Equals(this.state))
                {
                    HashSet<Cell> c = new HashSet<Cell>();
                    c.Add(cell);
                    observers.Add(index, c);
                    return;
                }
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

        
        //checks if a potential terminal state exists for the cell
        public int isTerminal()
        {
            int connectR = Board.GetConnectR();

            //if count greater than 1, then the state is terminal
            int terminalStates = 0;

            foreach(KeyValuePair<int, HashSet<Cell>> c in observers)
            {
                //initialized at 1 because this.cell has the state we are looking for
                int stateCount = 1;

                //keeps track of empty playable cells - should only equal 1 if a terminal state exists
                int emptyCount = 0;

                //the terminal state belongs to the this cell's connectedCells
                if(c.Value.Count == connectR - 1)
                {
                    foreach(Cell cell in c.Value)
                    {
                        if(cell.state.Equals(this.state))
                        {
                            stateCount++;
                        }
                        if(cell.state.Equals(Cell.CellState.empty) && cell.isPlayable())
                        {
                            emptyCount++;
                        }
                    }

                    //the connectedCells have a possibility to end the game
                    if(stateCount == connectR - 1 && emptyCount == 1)
                    {
                        terminalStates++;
                    }         
                }
                
                //The terminal state belongs to an observer
                else
                {
                    if (c.Value.Count == 1)
                    {
                        int index;

                        if (c.Key < 4)
                        {
                            index = c.Key + 4;
                        }
                        else
                        {
                            index = c.Key - 4;
                        }

                        foreach (Cell cell in c.Value)
                        {
                            if (cell.GetConnectedCells(index) != null && cell.GetConnectedCells(index).Count == connectR - 1)
                            {
                                foreach (Cell o in cell.GetConnectedCells(index))
                                {
                                    if (o.state.Equals(this.state))
                                    {
                                        stateCount++;
                                    }
                                    else if(o.state.Equals(Cell.CellState.empty) && o.isPlayable())
                                    {
                                        emptyCount++;
                                    }
                                }

                                //the cell have a possibility to end the game
                                if (stateCount == connectR - 1 && emptyCount == 1)
                                {
                                    terminalStates++;
                                }

                                
                            }
                        }
                    }
                }
            }

            return terminalStates;

        }

        public void setState(int s)
        {
            if(playable)
            {
                state = (CellState)s;
                playable = false;
            }        
        }

        public List<Cell> GetConnectedCells(int key)
        {
            if (connectCells.ContainsKey(key))

                return connectCells[key];
            else
                return null; 
        }

        public SortedDictionary<int, List<Cell>> GetConnectedCells()
        {
            return connectCells;
        }

        public HashSet<Cell> GetObservers(int key)
        {
            return observers[key];
        }

        public SortedDictionary<int, HashSet<Cell>> GetObservers()
        {
            return observers;
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
