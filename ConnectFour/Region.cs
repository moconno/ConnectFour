﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    abstract class Region : Board
    {

        static Region region;

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

        public static void findConnectedCells()
        {
            

            for(int i = length - 1; i >= 0; i--)
            {
                for(int j = 0; j < width; j++)
                {
                    Cell cell = board[i, j];
                
                    //has r north cells
                    if(i >= connectR - 1)
                    {
                        region = new Vertical();
                        region.AddCells(cell, connectedCells.north);

                        //has r northeast cells
                        if(j + connectR - 1 < width)
                        {
                            region = new Diagonal();
                            region.AddCells(cell, connectedCells.northEast);
                        }

                        //has r northwest cells
                        if(j >= connectR - 1)
                        {
                            region = new Diagonal();
                            region.AddCells(cell, connectedCells.northWest);
                        }
                    }

                    //has r south cells
                    if(i + connectR - 1 < length)
                    {
                        region = new Vertical();
                        region.AddCells(cell, connectedCells.south);

                        //has r southwest cells
                        if(j - connectR + 1 >= 0)
                        {
                            region = new Diagonal();
                            region.AddCells(cell, connectedCells.southWest);
                        }

                        //has r southeast cells
                        if(j <= connectR - 1)
                        {
                            region = new Diagonal();
                            region.AddCells(cell, connectedCells.southEast);
                        }
                    }

                    //has r east cells
                    if(j <= connectR - 1)
                    {
                        region = new Horizontal();
                        region.AddCells(cell, connectedCells.east);
                    }

                    //has r west cells
                    if(j - connectR + 1 >= 0)
                    {
                        region = new Horizontal();
                        region.AddCells(cell, connectedCells.west);
                    }
                }
            }
        }

        public abstract void AddCells(Cell cell, connectedCells c);

    }
}
