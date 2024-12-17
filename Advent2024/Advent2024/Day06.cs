using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    
    public class Day06:DayClass
    {
        object obj = new object();
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            

            string[,] grid = new string[1, 1];

            //StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");
            grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            int x = 0;
            int y = 0;
            Tuple<int, int> xy = new Tuple<int, int>(0,0);

            xy = FindStart(grid);
            string[,] grid2 = WalkGrid(grid, xy);

            int c = CountGrid(grid2);

            sw.Stop();
            
            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            string[,] grid = new string[1, 1];

            //StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");
            grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");


            Tuple<int, int> xy = new Tuple<int, int>(0, 0);

            xy = FindStart(grid);
            int c = 0;

            //string[,] grid2 = (string[,])grid.Clone();

            string[,] g2 = WalkGrid(grid, xy);
            g2[xy.Item1, xy.Item2] = ".";
            string[,] grid2 = (string[,])grid.Clone();
            
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (g2[x, y] == "X")
                    {
                        grid2[x, y] = "#";
                        bool loop = WalkGrid2(grid2, xy);
                        
                        //bool loop = false;
                        if (loop)
                        {
                            c++;
                            //break;
                        }
                        grid2[x, y] = ".";
                    }
                }
            }

            //Parallel.For(0, grid.GetLength(0), x =>
            //{
            //    for (int y = 0; y < grid.GetLength(1); y++)
            //    {
            //        if (g2[x, y] == "X")
            //        {
            //            string[,] grid2 = (string[,])grid.Clone();
            //            grid2[x, y] = "#";
            //            bool loop = WalkGrid2(grid2, xy);
            //            if (loop)
            //            {
            //                lock (obj)
            //                {
            //                    c++;
            //                }

            //            }
            //        }
            //    }
            //});


            sw.Stop();

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        Tuple<int, int> FindStart(string[,] grid)
        {
            for (int x= 0; x < grid.GetLength(0); x++)
            {
                for (int y= 0; y< grid.GetLength(1); y++)
                {
                    if (grid[x,y]=="^")
                    {
                        return new Tuple<int, int>(x,y);
                    }
                }
            }

            return new Tuple<int, int>(0, 0);
        }

        string[,] WalkGrid(string[,] grid, Tuple<int, int> xy)
        {
            int x = xy.Item1;
            int y = xy.Item2;
            string[,] grid2 = (string[,])grid.Clone();
            string p = grid[x, y];

            while (x < grid.GetLength(0) && x >= 0 && y < grid.GetLength(1) && y >= 0)
            {
                //grid[x, y] = p;
                grid2[x, y] = "X";
                
                //grid[x, y] = ".";
                switch (p)
                {
                    case "^":
                        if (y > 0 && grid[x, y - 1] == "#")
                        {
                            p = ">";
                        }
                        else
                        {
                            y--;
                        }
                        
                        break;
                    case ">":
                        if (x < grid.GetLength(0)-1 && grid[x + 1, y] == "#")
                        {
                            p = "v";
                        }
                        else
                        {
                            x++;
                        }
                        
                        break;
                    case "<":
                        if (x > 0 && grid[x - 1, y] == "#")
                        {
                            p = "^";
                        }
                        else
                        {
                            x--;
                        }
                        break;
                    case "v":
                        if (y < grid.GetLength(1)-1 && grid[x, y + 1] == "#")
                        {
                            p = "<";
                        }
                        else
                        {
                            y++;
                        }

                        break;

                }
                

                  
            }

            return grid2;
        }

        bool WalkGrid2(string[,] grid, Tuple<int, int> xy)
        {
            int x = xy.Item1;
            int y = xy.Item2;
            
            string p = grid[x, y];
            //List<int> pos = new List<int>();
            //int[] pos = new int[6000];
            var pos = new HashSet<(int, int)>();  //hashset significantly faster than array or list.
                // another (actually slightly faster) alternative is to just count steps and exit if > 10000
                // could also 'teleport' between obstacles.

            int pt = 0;
            int thisdir = 0;
            int step = 0;
            while (x < grid.GetLength(0) && x >= 0 && y < grid.GetLength(1) && y >= 0)
            {
                int thispos = x * 1000 + y * 1000000;
                step++;
                if (step> 10000)
                {
                    break;
                }
                switch (p)
                {
                    case "^":
                        thisdir = 1;
                        if (y > 0 && grid[x, y - 1] == "#")
                        {
                            p = ">";
                        }
                        else
                        {
                            y--;
                        }
                        break;
                    case ">":
                        thisdir = 2;
                        if (x < grid.GetLength(0) - 1 && grid[x + 1, y] == "#")
                        {
                            p = "v";
                        }
                        else
                        {
                            x++;
                        }

                        break;
                    case "<":
                        thisdir = 3;
                        if (x > 0 && grid[x - 1, y] == "#")
                        {
                            p = "^";
                        }
                        else
                        {
                            x--;
                        }
                        break;
                    case "v":
                        thisdir = 4;
                        if (y < grid.GetLength(1) - 1 && grid[x, y + 1] == "#")
                        {
                            p = "<";
                        }
                        else
                        {
                            y++;
                        }

                        break;
                        

                }

                if (!pos.Add((thispos, thisdir)))
                {
                    return true;
                }
                //else
                //{
                //   //pos.Add(thispos);
                //  pos[pt] = thispos;
                // pt++;
                //}

            }


            return false;
        }
        int CountGrid(string[,] grid)
        {
            int c = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == "X")
                    {
                        c++;
                    }
                }
            }

            return c;
        }


    }
}
