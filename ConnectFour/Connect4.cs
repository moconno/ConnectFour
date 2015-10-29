using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Connect4
    {
        //Prompt user for width and height and win requirement

        static int colNum;

        static int rowNum;

        static int r;

        const int BLACK = 2, RED = 1;

        public enum Players{
            humanVsHuman = 1,
            humanVsAi = 2,
            AiVsAi = 3,
        }

        static void Main(string[] args)
        {

            Console.Write("Board Width? ");
            colNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Board Height? ");
            rowNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Connect? ");
            r = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("1: Human vs Human");
            Console.WriteLine("2: Human vs AI");
            Console.WriteLine("3: AI vs AI");
            Console.Write("Select Players: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Board board = new Board(rowNum, colNum, r);
            Region.findConnectedCells(board);
            board.UpdateCellObservers();

            Player p1 = null;
            Player p2 = null;

            switch (choice)
            {
                case 1:
                    p1 = new HumanPlayer();
                    p1.setAsRed();
                    p2 = new HumanPlayer();
                    p2.setAsBlack();
                    break;

                case 2:
                    Console.WriteLine("1: Human 2: AI");
                    Console.Write("Who will go first? ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if(choice == 1)
                    {
                        p1 = new HumanPlayer();
                        p1.setAsRed();
                        p2 = new AIPlayer();
                        p2.setAsBlack();
                    }
                    else
                    {
                        p1 = new AIPlayer();
                        p1.setAsRed();
                        p2 = new HumanPlayer();
                        p2.setAsBlack();
                    }
                    p1.setOpponent(p2);
                    p2.setOpponent(p1);
                    break;

                case 3:
                    break;
                   
            }

            int turn = RED;
 
            while(!board.isGameOver())
            { 
                switch (turn)
                {
                    case RED:
                        p1.Move(board);
                        turn = BLACK;
                        break;
                    case BLACK:
                        p2.Move(board);
                        turn = RED;
                        break;
                }
            }

            Console.Read();
         
        }
    }
}
