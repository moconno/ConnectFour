using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class AIPlayer : Player
    {
        //The cell to be played
        Cell cell;

        public AIPlayer()
        {

        }

        public override void Move(Board b)
        {
            //base case = the optimal move to start a game if AI Player is first to move
            if(moveCount == 0)
            {
                b.getCell(b.GetLength() - 1, (b.GetWidth() - 1) / 2).setState((int)color);
                b.getCell(b.GetLength() - 2, (b.GetWidth() - 1) / 2).isPlayable(true);
                moveCount++;
            }
            else
            {
                GameState gameState = new GameState(GameState.State.initial, b, this, null, null, true);
                MiniMaxTree m = new MiniMaxTree(gameState);
                m.GenerateStates(gameState, 4, true);
                int value = m.MiniMax(gameState, 4, true);

                int count = 0;

                Cell cell = null;

                foreach(GameState child in gameState.GetChildren())
                {
                    if (child.GetHeuristicValue() == value)
                    {
                        if(child.GetCell().GetObservers().Count > count)
                        {
                            count = child.GetCell().GetObservers().Count;
                            cell = child.GetCell();
                        }
                    }
                }

                b.getCell(cell.getRow(), cell.getColumn()).setState(gameState.GetPlayer().getColor());

                if (cell.getRow() - 1 != 0)
                {
                    b.getCell(cell.getRow() - 1, cell.getColumn()).isPlayable(true);
                }
                moveCount++;
                int a = 0;
            }
       
        }
    }
}
