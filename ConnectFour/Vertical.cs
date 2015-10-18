using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Vertical : Region
    {

        List<Cell> cells;

        public Vertical()
        {
            cells = new List<Cell>();
        }

        public override void AddCells(int row, int column, Cell cell, connectedCells c)
        {
            switch(c)
            {
                case connectedCells.north:
                    for (int i = row; i < connectR; i--)
                    {
                        
                    }

                    break;
                case connectedCells.south:
                    break;
            }

        }
    }
}
