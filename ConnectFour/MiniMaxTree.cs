using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class MiniMaxTree
    {
   

        public void MiniMax(GameState gs, int depth, Boolean maxPlayer)
        {
            gs.FindChildrenStates(maxPlayer);
            //base case
            if (depth == 0) //|| !gs.HasChildren())
                return;
            //return heuristic value of node

            if(maxPlayer)
            {
                int bestValue = -100;
                //for child in node
                foreach(GameState child in gs.GetChildren())
                {
                    MiniMax(child, depth - 1, false);
                }
                //val = minimax(child, depth-1, false)
                //bestValue = max(bestValue, val)
            }
            else
            {
                //intbest = 100;
                //for child in node
                foreach (GameState child in gs.GetChildren())
                {
                    MiniMax(child, depth - 1, true);
                }
                //bestValue = min(bestValue, val)

            }
        }
    }


}
