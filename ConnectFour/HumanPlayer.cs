﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class HumanPlayer : Player
    {

        public HumanPlayer()
        {

        }

        public override void Move(Board b)
        {

            if(moveCount == b.GetLength() * b.GetWidth())
            {
                Console.WriteLine("Game Over, you both lose");
            }

            Boolean moveMade = false;

            b.printBoard();

            if(this.getOpponent() != null)
            {
                Console.WriteLine(" ");
            }

            Console.WriteLine(this.getColorToString() + " player's turn");

            while(!moveMade)
            {
                Console.Write("Select a Move: " + "0 - " + (b.GetWidth() - 1) + ": ");

                int choice = Convert.ToInt32(Console.ReadLine());

                for (int i = b.GetLength() - 1; i >= 0; i--)
                {
                    if(choice < 0 || choice > b.GetWidth() - 1)
                    {
                        break;
                    }

                    cell = b.getCell(i, choice);

                    if (cell.isPlayable())
                    {
                        cell.setState((int)color);

                        cell.isPlayable(false);

                        if (i != 0)
                        {
                            b.getCell(i - 1, choice).isPlayable(true);
                        }

                        moveMade = true;

                        moveCount++;

                        if (MiniMaxTree.TerminalTest(cell))
                        {
                            b.printBoard();
                            Console.WriteLine("GameOver: " + this.getColorToString() + " wins");
                            b.isGameOver(true);
                        }

                        break;
                    }
                }


            }      

        }
    }
}
