using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class MiniMaxTree
    {
        GameState root;

        public static int MAX_VALUE = 1000000000;

        public static int MIN_VALUE = -1000000000;

        public MiniMaxTree()
        {

        }

        public MiniMaxTree(GameState gs)
        {
            root = gs;
        }

        public void GenerateStates(GameState gs, int depth, Boolean maxPlayer)
        {
            if(depth == 0 || gs.GetState().Equals(GameState.State.terminal))
            {
                return;
            }
           
           gs.FindChildrenStates(maxPlayer);
                     
           foreach(GameState child in gs.GetChildren())
           {
               if (maxPlayer)
               {
                   GenerateStates(child, depth - 1, false);
               }
               else
                   GenerateStates(child, depth - 1, true);
            }
        }


        //Tests the cell played if it is terminal
        public static Boolean TerminalTest(Cell cell)
        {
            int connectR = Board.GetConnectR();

            int key = 0;

            foreach(KeyValuePair<int, List<Cell>> c in cell.GetConnectedCells())
            {
                if(c.Key < 4)
                {
                    key = key + 4;
                }
                else
                    key = key - 4;
                //initialized at 1 because cell has the state we are looking for
                int stateCount = 1;

                //the terminal state belongs to the this cell's connectedCells
                if (c.Value.Count == connectR - 1)
                {
                    foreach (Cell k in c.Value)
                    {
                        if (k.getState().Equals(cell.getState()))
                        {
                            stateCount++;
                        }
                    }

                    //The cell last played created a connect R.  Game over.
                    if (stateCount == connectR)
                    {
                        return true;
                    }
                }
                
                //Terminal case may belong with observer
                else 
                {
                    foreach(Cell k in c.Value)
                    {
                        if(k.GetConnectedCells(key).Count == connectR - 1)
                        {
                            foreach(Cell m in k.GetConnectedCells(key))
                            {
                                if(m.getState().Equals(cell.getState()))
                                {
                                    stateCount++;
                                }

                                //The cell last played created a connect R.  Game over.
                                if (stateCount == connectR)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                
                }
            }

            return false;       
        }

        public int FindHeuristicValue(GameState gs)
        {
            int value = 0;

            //The count of connectCells a cell contains
            int bestCount = 0;

            int connectR = Board.GetConnectR();

            if(gs.GetState().Equals(GameState.State.terminal))
            {
                return gs.GetHeuristicValue();
            }

            //if true it is maxPlayers turn and the board needs to be evaluate to minimize min's score
            if(gs.isMaxPlayer())
            {

                //check if max can make a killer move
                foreach(Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer()))
                {
                    if(cell.isTerminal() > 0)
                    {
                        value = MAX_VALUE;
                        return value;
                    }
                }
                
                //check if it is impossible to prevent min from winning
                if(gs.GetCell().isTerminal() > 1)
                {
                    value = MIN_VALUE;
                    return value;
                }

                //check if min can make a killer move
                foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer().getOpponent()))
                {
                    if (cell.isTerminal() == 1)
                    {
                        value = MIN_VALUE / 2;
                        return value;
                    }
                }

                //last resort - find the move that gives the most possibilites for connect 4
                foreach(Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer()))
                {
                    int count = 0;

                    foreach(KeyValuePair<int, HashSet<Cell>> c in cell.GetObservers())
                    {
                        count = count + c.Value.Count;    
                    }

                    foreach(KeyValuePair<int, List<Cell>> k in cell.GetConnectedCells())
                    {
                        count = count + k.Value.Count;
                    }

                    if(count > bestCount)
                    {
                        bestCount = count;
                    }
                }

                value = bestCount;

                return value;
            
            }
            
            //It is not max's turn
            else
            {
               
                //check if min can make a killer move
                foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer().getOpponent()))
                {
                    if (cell.isTerminal() > 0)
                    {
                        value = MIN_VALUE;
                        return value;
                    } 
                }

                //check if it is impossible to prevent max from winning
                if (gs.GetCell().isTerminal() > 1)
                {
                    value = MAX_VALUE;
                    return value;
                }

                //check if max can make a killer move
                foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer()))
                {
                    if (cell.isTerminal() == 1)
                    {
                        value = MAX_VALUE / 2;
                        return value;
                    }
                }

            }

            //last resort - find the move that gives the most possibilites for connect 4
            foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer().getOpponent()))
            {
                int count = 0;

                foreach (KeyValuePair<int, HashSet<Cell>> c in cell.GetObservers())
                {
                    count = count + c.Value.Count;
                }

                foreach (KeyValuePair<int, List<Cell>> k in cell.GetConnectedCells())
                {
                    count = count + k.Value.Count;
                }

                if (count > bestCount)
                {
                    bestCount = count;
                }
            }

            value = bestCount;
            return value;
        }
   

        public int MiniMax(GameState gs, int depth, Boolean maxPlayer)
        {
            
            //base case
            if (depth == 0 || gs.GetState().Equals(GameState.State.terminal))
            {
                return FindHeuristicValue(gs);        
            }

            if(maxPlayer)
            {
                int bestValue = MIN_VALUE;

                foreach(GameState child in gs.GetChildren())
                {
                    int value = MiniMax(child, depth - 1, false);

                    if (value.CompareTo(bestValue) > 0)
                    {
                        gs.SetHeuristicValue(value);
                        bestValue = value;
                    }
                }

                return bestValue;
            }
            else
            {
                int bestValue = MAX_VALUE;
               
                foreach (GameState child in gs.GetChildren())
                {
                    int value = MiniMax(child, depth - 1, true);

                    if(value.CompareTo(bestValue) < 0)
                    {
                        gs.SetHeuristicValue(value);
                        bestValue = value;
                    }
                }

                return bestValue;
            }    
        }

     
    }


}
