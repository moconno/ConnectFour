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

        public override void AddCells(Cell cell, connectedCells c)
        {
           
            switch(c)
            {
                case connectedCells.north:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board[cell.getRow() - i, cell.getColumn()]);
                    }

                    cell.AddConnectedCells((int)connectedCells.north, cells);

                    break;

                case connectedCells.south:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board[cell.getRow() + i, cell.getColumn()]);
                    }

                    cell.AddConnectedCells((int)connectedCells.south, cells);

                    break;
            }

        }
    }
}
