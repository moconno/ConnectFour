using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Board
    {

        protected Cell[,] board;

        protected static int width;

        protected static int length;

        protected static int connectR;

        protected Boolean gameOver = false;

        public Board()
        {

        }

        //Initial board creation
        public Board(int x, int y, int r)
        {
            length = x;

            width = y;

            connectR = r;

            board = new Cell[length, width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Cell cell = new Cell(i, j);

                    if (i == length - 1)
                    {
                        cell.isPlayable(true);
                    }

                    board[i, j] = cell;
                }
            }
        }

        //Copy constructor
        public Board(Board b)
        {
            length = b.GetLength();

            width = b.GetWidth();

            connectR = Board.GetConnectR();

            board = new Cell[length, width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Cell cell = new Cell(b.getCell(i, j));

                    board[i, j] = cell;
                }
            }
        }

        //Has each cell update its observers 
        public void UpdateCellObservers()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    board[i, j].UpdateObservers();
                }
            } 
        }

        public Boolean isGameOver()
        {
            return gameOver;
        }

        public void isGameOver(Boolean g)
        {
            gameOver = g;
        }

        public Cell[,] GetBoard()
        {
            return board;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetLength()
        {
            return length;
        }

        public static int GetConnectR()
        {
            return connectR;
        }

        public Cell getCell(int x, int y)
        {
            return board[x, y];
        }

        //Get the cells on the board that belong to the player
        public List<Cell> getPlayerCells(Player player)
        {
            List<Cell> cells = new List<Cell>();

            for(int i = length - 1; i >= 0; i--)
            {
                for (int j = 0; j < width - 1; j++)
                {
                    if((int)board[i, j].getState() == player.getColor())
                    {
                        cells.Add(board[i, j]);
                    }
                }
            }
            return cells;
        }

        public void printBoard()
        {
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write((int)board[i, j].getState() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
