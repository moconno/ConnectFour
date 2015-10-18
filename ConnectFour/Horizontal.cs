using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour 
{
    class Horizontal : Region
    {
        List<Cell> cells;

        public Horizontal()
        {
            cells = new List<Cell>();
        }

        public override void AddCells(Cell cell, connectedCells c)
        {
           
            switch(c)
            {
                case connectedCells.east:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board[cell.getRow(), cell.getColumn() + i]);
                    }

                    cell.AddConnectedCells((int)connectedCells.east, cells);

                    break;

                case connectedCells.west:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board[cell.getRow(), cell.getColumn() - i]);
                    }

                    cell.AddConnectedCells((int)connectedCells.west, cells);

                    break;
            }

        }
    }
}
