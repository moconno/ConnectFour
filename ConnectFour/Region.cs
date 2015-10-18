using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    abstract class Region : Board
    {
        public enum connectedCells
        {
            north,
            northEast,
            east,
            southEast,
            south,
            southWest,
            west,
            northWest,
        }

        public Region()
        {
            
        }

        public SortedDictionary<int, Region> findConnectedCells()
        {
            List<connectedCells> directions;

            for(int i = length - 1; i >= 0; i--)
            {
                for(int j = 0; j < width; j++)
                {
                    Cell cell = board[i, j];

                    directions = new List<connectedCells>();
                    
                    //has r north cells
                    if(i >= connectR - 1)
                    {
                        directions.Add(connectedCells.north);

                        //has r northeast cells
                        if(j + connectR - 1 < width)
                        {
                            directions.Add(connectedCells.northEast);
                        }

                        //has r northwest cells
                        if(j >= connectR - 1)
                        {
                            directions.Add(connectedCells.northWest);
                        }
                    }

                    //has r south cells
                    if(i + connectR - 1 < length)
                    {
                        directions.Add(connectedCells.south);

                        //has r southwest cells
                        if(j - connectR - 1 >= 0)
                        {
                            directions.Add(connectedCells.southWest);
                        }

                        //has r southeast cells
                        if(j >= connectR - 1)
                        {
                            directions.Add(connectedCells.southEast);
                        }
                    }

                    //has r east cells
                    if(j >= connectR - 1)
                    {
                        directions.Add(connectedCells.east);
                    }

                    //has r west cells
                    if(j - connectR - 1 >= 0)
                    {
                        directions.Add(connectedCells.west);
                    }

                    ConnectCells(i, j, cell, directions);
                }
            }
        }

        private SortedDictionary<int, Region> ConnectCells(int row, int column, Cell cell, List<connectedCells> c)
        {
            SortedDictionary<int, Region> cells = new SortedDictionary<int,Region>();

            Region region;

            for(int i = 0; i < c.Count; i++)
            {
                connectedCells direction = c[i];

                switch(direction)
                {
                    case connectedCells.north:
                        region = new Vertical();
                        region.AddCells(row, column, cell, c);
                        break;
                }
            }
        }


        public abstract void AddCells(int row, int column, Cell cell, connectedCells c);

    }
}
