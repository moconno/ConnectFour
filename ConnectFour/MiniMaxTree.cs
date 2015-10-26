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

        public MiniMaxTree()
        {

        }

        public MiniMaxTree(GameState gs)
        {
            root = gs;
        }

        public void GenerateStates(GameState gs, int depth, Boolean maxPlayer)
        {
            if(depth == 0)
            {
                //gs.SetHeuristicValue(FindHeuristicValue(gs));
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

        public void FindHeuristicValue(GameState gs)
        {
            int value;

            //if true it is maxPlayers turn and the board needs to be evaluate to minimize min's score
            if(gs.isMaxPlayer())
            {
                int gameEndingMoves = 0;

                if(gs.GetCell().isTerminal())
                {
                    value = -100;
                    //return value;
                }

                foreach(KeyValuePair<int, HashSet<Cell>> c in gs.GetCell().GetObservers())
                {
                    foreach(Cell cell in c.Value)
                    {
                        HashSet<Cell> cells;

                        int stateCount = 0;

                        if(cell.isPlayable())
                        {
                            switch(c.Key)
                            {
                                case 0:
                                    break;


                            }
                        }
                    }
                }
                
            }
        }
   

        public void MiniMax(GameState gs, int depth, Boolean maxPlayer)
        {
            
            //base case
            if (depth == 0 || gs.GetState() == GameState.State.terminal)
                //return;
            //return heuristic value of node
            gs.FindChildrenStates(maxPlayer);

            if(maxPlayer)
            {
                int bestValue = -100;
                //for child in node
                foreach(GameState child in gs.GetChildren())
                {
                    //int value = MiniMax(child, depth - 1, false);
                    //bestValue = Max(bestValue, value);
                }
                
            }
            else
            {
                int bestValue = 100;
                //for child in node
                foreach (GameState child in gs.GetChildren())
                {
                    MiniMax(child, depth - 1, true);
                }
                //int val = MiniMax(child, depth - 1, true);
                //bestValue = min(bestValue, val)
            }
        }

        /*public int Max(GameState gs)
        {

        }

        public int Min(GameState gs)
        {

        }*/
    }


}
