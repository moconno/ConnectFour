using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class GameState
    {
        public enum State
        {
            initial,
            transition,
            terminal,
        }

        State state;

        //The cell that was played in this state - null for initial state
        Cell cell;

        //The current board for the state
        Board board;

        //the parent board of the state - null for initial state
        GameState parent;

        //the children of the state - null for terminal nodes
        List<GameState> children;

        //The player in control of the board and trying to maximize its score
        Player player;

        //The state the board is in
        Boolean maxPlayer;

        //heuristic value for the player of the game state
        private int heuristicValue;


        public GameState(State s, Board b, Player p, GameState parent, Cell c, Boolean mp)
        {
            maxPlayer = mp;
            state = s;
            cell = c;
            board = b;
            player = p;
            this.parent = parent;
            children = new List<GameState>();  
            Region.findConnectedCells(b);
            b.UpdateCellObservers();  
            if(cell != null && cell.isTerminal(false) && s == GameState.State.transition)
            {
                state = GameState.State.terminal;
            }
        }

        public void FindChildrenStates(Boolean maxPlayer)
        {
            GameState gs;

            for(int i = 0; i < board.GetWidth(); i++)
            {
                for(int j = 0; j < board.GetLength(); j++)
                {
                    if(board.getCell(j,i).getState().Equals(Cell.CellState.empty))
                    {
                        if (board.getCell(j, i).isPlayable())
                        {
                            Board b = new Board(board);

                            Cell cell = b.getCell(j, i);

                            if (maxPlayer)
                            {
                                cell.setState(player.getColor());
                            }
                            else
                            {
                                cell.setState(player.getOpponent().getColor());
                            }

                            if (j != 0)
                            {
                                b.getCell(j - 1, i).isPlayable(true);
                            }

                            gs = new GameState(GameState.State.transition, b, player, this, cell, !maxPlayer);
                            children.Add(gs);

                            //cell made a move that ended the game
                            /*if (cell.isTerminal(false))
                            {
                                gs = new GameState(GameState.State.terminal, b, player, this, cell, !maxPlayer);
                                children.Add(gs);
                                break;
                            }
                            
                            else
                            {
                                gs = new GameState(GameState.State.transition, b, player, this, cell, !maxPlayer);

                                if( j != 0)
                                {
                                    b.getCell(j - 1, i).isPlayable(true);
                                }
                                children.Add(gs);
                            }*/
                        }  
                    }
                    else
                        break;
                }
                
            }
        }

        public Boolean hasGameEndingMove(Cell cell)
        {
            int connectR = Board.GetConnectR();

            foreach (KeyValuePair<int, HashSet<Cell>> c in cell.GetObservers())
            {
                //Count is one because cell with observers is max's state cell
                int count = 1;

                foreach (Cell o in c.Value)
                {
                    if ((int)o.getState() == this.GetPlayer().getColor())
                    {
                        count++;
                    }
                }

                if (count == connectR - 1)
                {
                    return true;
                }

            }
            return false;
        }

        public Boolean isMaxPlayer()
        {
            return maxPlayer;
        }

        public Player GetPlayer()
        {
            return player;
        }

        public GameState GetParent()
        {
            return parent;
        }

        public void SetHeuristicValue(int value)
        {
            heuristicValue = value;
        }

        public int GetHeuristicValue()
        {
            return heuristicValue;
        }

        public Cell GetCell()
        {
            return cell;
        }

        public Board GetBoard()
        {
            return board;
        }

        public void SetState(GameState.State s)
        {
            state = s;
        }

        public State GetState()
        {
            return state;
        }

        public List<GameState> GetChildren()
        {
            return children;
        }



    }
}
