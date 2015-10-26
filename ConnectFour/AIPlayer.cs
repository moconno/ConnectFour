using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class AIPlayer : Player
    {
        //int DEPTH = 4;

        public AIPlayer()
        {

        }

        public override void Move(Board b)
        {
            GameState gameState = new GameState(GameState.State.initial, b, this, null, null, true);
            MiniMaxTree m = new MiniMaxTree(gameState);
            //m.MiniMax(gameState, DEPTH, true);
            m.GenerateStates(gameState, 4, true);
            int a = 0;
           
        }
    }
}
