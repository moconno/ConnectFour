using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Board
    {

        protected static Cell[,] board;

        protected static int width;

        protected static int length;

        protected static int connectR;

        protected Player player;

        protected Boolean gameOver = false;

        public Board()
        {

        }

        public Board(Cell[,] b)
        {
            board = new Cell[b.GetLength(0), b.GetLength(1)];
            board = b;
        }

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

        public Boolean isGameOver()
        {
            return gameOver;
        }

        public void isGameOver(Boolean g)
        {
            gameOver = g;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetLength()
        {
            return length;
        }

        public void setPlayer(Player p)
        {
            player = p;
        }

        public Cell getCell(int x, int y)
        {
            return board[x, y];
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
