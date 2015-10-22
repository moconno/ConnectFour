using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Diagonal : Region
    {

        List<Cell> cells;

        public Diagonal()
        {
            cells = new List<Cell>();
        }

        public override void AddCells(Board board, Cell cell, connectedCells c)
        {
           
            switch(c)
            {
                case connectedCells.northEast:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board.getCell(cell.getRow() - i, cell.getColumn() + i));
                    }

                    cell.AddConnectedCells((int)connectedCells.northEast, cells);

                    break;

                case connectedCells.southEast:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board.getCell(cell.getRow() + i, cell.getColumn() + i));
                    }

                    cell.AddConnectedCells((int)connectedCells.southEast, cells);

                    break;

                case connectedCells.southWest:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board.getCell(cell.getRow() + i, cell.getColumn() - i));
                    }

                    cell.AddConnectedCells((int)connectedCells.southWest, cells);

                    break;

                case connectedCells.northWest:
                    for (int i = 1; i < connectR; i++)
                    {
                        cells.Add(board.getCell(cell.getRow() - i, cell.getColumn() - i));
                    }

                    cell.AddConnectedCells((int)connectedCells.northWest, cells);

                    break;
            }

        }

    }
}
