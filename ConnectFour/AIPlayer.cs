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
            {
                //Create a new game state for the root of the state space
                GameState gameState = new GameState(GameState.State.initial, b, this, null, cell, true);

                //Set the state as the root
                MiniMaxTree m = new MiniMaxTree(gameState);

                //Find children states if they exist
                m.GenerateStates(gameState, 4, true);

                //The value returned by the recursive minimax function
                int value = m.MiniMax(gameState, 4, true);

                cell = null;

                foreach(GameState child in gameState.GetChildren())
                {
                    if (child.GetHeuristicValue() == value)
                    {
                        cell = child.GetCell();
                    }
                }

                //Set the cell on the playing board
                b.getCell(cell.getRow(), cell.getColumn()).setState((int)cell.getState());

                //if a row exists above this cell, make it playable
                if (cell.getRow() != 0)
                {
                    b.getCell(cell.getRow() - 1, cell.getColumn()).isPlayable(true);
                }

                //Test if the game is over
                if(MiniMaxTree.TerminalTest(cell))
                {
                    b.printBoard();
                    Console.WriteLine("GameOver: " + this.getColorToString() + " wins");
                    b.isGameOver(true);
                }

                moveCount++;
                
            }
       
        }
    }
}
