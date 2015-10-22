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
        

        protected enum Color
        {
            black = 2,
            red = 1,
        }

        public Player()
        {

        }

        public Player(Player player)
        {
            opponent = player;
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
