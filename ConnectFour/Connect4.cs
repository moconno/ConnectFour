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
            //Board board = new Board();

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
                        //p2 ;
                        //p2.setAsBlack();
                    }
                    else
                    {
                        p1 = new HumanPlayer();
                        p1.setAsBlack();
                        //p2.setAsAI();
                        //p2.setAsRed();
                    }
                    break;

                case 3:
                    break;
                   
            }

            int turn = RED;

            while(!board.isGameOver())
            {
                board.printBoard();

                switch (turn)
                {
                    case RED:
                        Console.WriteLine(p1.getColor() + " player's turn");
                        p1.Move(board);
                        turn = BLACK;
                        break;
                    case BLACK:
                        Console.WriteLine(p2.getColor() + " player's turn");
                        p2.Move(board);
                        turn = RED;
                        break;
                }
            }
         
        }
    }
}
