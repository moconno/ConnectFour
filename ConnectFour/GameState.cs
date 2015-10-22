using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class GameState
    {
        //The cell that was played in this state - null for initial state
        Cell cell;

        //The current board for the state
        Board board;

        //the parent board of the state - null for initial state
        Board parent;

        //the children of the state - null for terminal nodes
        List<GameState> children;
        
        //true if state has children
        Boolean hasChildren;

        //The player in control of the board and trying to maximize its score
        Player player;

        public GameState(Board b, Player p, Board parent, Cell c)
        {
            cell = c;
            board = b;
            player = p;
            this.parent = parent;
            children = new List<GameState>();
        }

        public void FindChildrenStates(Boolean maxPlayer)
        {
            for(int i = 0; i < board.GetLength(); i++)
            {
                for(int j = 0; j < board.GetWidth(); j++)
                {
                    if(board.getCell(i, j).isPlayable())
                    {
                        Board b = new Board(board);
                        if(maxPlayer)
                        {
                            b.getCell(i, j).setState(player.getColor());     
                        }
                        else
                        {
                            b.getCell(i, j).setState(player.getOpponent().getColor());
                        }
                        if (i >= 1)
                        {
                            b.getCell(i - 1, j).isPlayable(true);
                        }
                        GameState gs = new GameState(b, player, board, b.getCell(i, j));
                        children.Add(gs);
                        hasChildren = true;
                    }
                }
            }
        }

        public List<GameState> GetChildren()
        {
            return children;
        }

        public Boolean HasChildren()
        {
            return hasChildren;
        }

    }
}
