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

        int MAX_VALUE = 1000000000;

        int MIN_VALUE = -1000000000;

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
                gs.SetHeuristicValue(FindHeuristicValue(gs));
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

        public int FindHeuristicValue(GameState gs)
        {
            int value;

            int connectR = Board.GetConnectR();

            //if true it is maxPlayers turn and the board needs to be evaluate to minimize min's score
            if(gs.isMaxPlayer())
            {
                

                //check if min made a killer move
                if (gs.GetCell().isTerminal(false))
                {
                    value = MIN_VALUE;
                    return value;
                }

                //check if max can make a killer move
                foreach(Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer()))
                {
                    if(gs.hasGameEndingMove(cell))
                    {
                        value = MAX_VALUE;
                        return value;
                    }
                }
                
                //check if it is impossible to prevent min from winning
                if(gs.GetCell().isTerminal(true))
                {
                    value = MIN_VALUE;
                    return value;
                }

                //check if min can make a killer move
                foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer().getOpponent()))
                {
                    if (cell.isTerminal(false))
                    {
                        value = MAX_VALUE / 2;
                        return value;
                    }
                }
                

                //if all else fails
                //check if it is impossible to prevent max from winning
                if (gs.GetParent().GetCell().isTerminal(true))
                {
                    value = MAX_VALUE;
                    return value;
                }
            }
            
            //It is not max's turn
            else
            {
                
                //check if max made a killer move
                if (gs.GetCell().isTerminal(false))
                {
                    value = MAX_VALUE;
                    return value;
                }

                //check if min can make a killer move
                foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer().getOpponent()))
                {
                    if (cell.isTerminal(false))
                    {
                        value = MIN_VALUE / 2;
                        return value;
                    }
                }

                //check if it is impossible to prevent max from winning
                if (gs.GetCell().isTerminal(true))
                {
                    value = MAX_VALUE;
                    return value;
                }

                //check if max can make a killer move
                foreach (Cell cell in gs.GetBoard().getPlayerCells(gs.GetPlayer()))
                {
                    if (cell.isTerminal(false))
                    {
                        value = MAX_VALUE / 2;
                        return value;
                    }
                }


                //check if it is impossible to prevent min from winning
                if (gs.GetParent().GetCell().isTerminal(true))
                {
                    value = MIN_VALUE;
                    return value;
                }


            }

            if (gs.isMaxPlayer())
                return 1;
            else
                return -1;
        }
   

        public int MiniMax(GameState gs, int depth, Boolean maxPlayer)
        {
            
            //base case
            if (depth == 0 || gs.GetState().Equals(GameState.State.terminal))
            {
                return gs.GetHeuristicValue();
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
