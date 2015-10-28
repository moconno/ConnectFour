using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    abstract class Player
    {

        protected Color color;

        Player opponent;

        protected static int moveCount = 0;
        
        protected enum Color
        {
            red = 1,
            black = 2,
        }

        public Player()
        {

        }

        public void setOpponent(Player player)
        {
            opponent = player;
        }

        public Player getOpponent()
        {
            return opponent;
        }

        public int getColor()
        {
            return (int)color;
        }

        public String getColorToString()
        {
            return color.ToString();
        }

        public void setAsRed()
        {
            color = Color.red;
        }   

        public void setAsBlack()
        {
            color = Color.black;
        }

        abstract public void Move(Board board);

    }
}
