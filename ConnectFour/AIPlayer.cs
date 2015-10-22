using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class AIPlayer : Player
    {

        public AIPlayer()
        {

        }

        public override void Move(Board b)
        {
            GameState gameState = new GameState(b, this, null, null);
            MiniMaxTree m = new MiniMaxTree();
            m.MiniMax(gameState, 4, true);
            int a = 0;
           
        }
    }
}
