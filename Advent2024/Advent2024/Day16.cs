using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Numerics;
using static System.Windows.Forms.AxHost;
using System.Windows.Forms;

namespace AdventCode
{
    class Day16 : DayClass
    {
        //HashSet<(int, int)> visited2 = new HashSet<(int, int)>();
        Dictionary<int, long> visited2 = new Dictionary<int, long>();
        List<long> obs = new List<long>();
        long minscore = long.MaxValue;


        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            int thisx = 0;
            int thisy = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == "S")
                    {
                        thisx = x;
                        thisy = y;
                        break;
                    }
                }
            }
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            //WalkGrid(grid, thisx, thisy, 1, 0, visited);

            sw.Stop();


            string ret = "Answer : " + minscore.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            int thisx = 0;
            int thisy = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == "S")
                    {
                        thisx = x;
                        thisy = y;
                        break;
                    }
                }
            }
            List<long> visited = new List<long>();
            WalkGrid2(grid, thisx, thisy, 1, 0, visited);

            sw.Stop();


            string ret = "Answer : " + obs.Count.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        void WalkGrid(string[,] grid, int thisx, int thisy, int dir, long thisscore, HashSet<(int, int)> visited)
        {


            if (grid[thisx, thisy] == "#")
            {
                return;
            }

            if (thisscore > 200000 || thisscore > minscore)
            {
                return;
            }

            if (grid[thisx, thisy] == "E")
            {
                if (thisscore < minscore)
                {
                    minscore = thisscore;
                }
                return;
            }

            int thispos = thisx + thisy * 1000;
            if (!visited.Add((thispos, dir)))
            {
                return;
            }

            if (visited2.ContainsKey(thispos))
            {
                if (visited2[thispos] < thisscore)
                {
                    return;
                }
                else
                {
                    visited2[thispos] = thisscore;
                }
            }
            else
            {
                visited2[thispos] = thisscore;
            }



            long newscore = newscore = thisscore + 1;
            HashSet<(int, int)> newvisited = new HashSet<(int, int)>(visited);
            switch (dir)
            {
                case 0:

                    WalkGrid(grid, thisx, thisy - 1, 0, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx + 1, thisy] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx + 1, thisy, 1, newscore, newvisited);
                    }
                    if (grid[thisx - 1, thisy] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx - 1, thisy, 3, newscore, newvisited);
                    }

                    break;
                case 1:
                    WalkGrid(grid, thisx + 1, thisy, 1, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx, thisy - 1] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx, thisy - 1, 0, newscore, newvisited);
                    }
                    if (grid[thisx, thisy + 1] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx, thisy + 1, 2, newscore, newvisited);
                    }


                    break;
                case 2:
                    WalkGrid(grid, thisx, thisy + 1, 2, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx + 1, thisy] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx + 1, thisy, 1, newscore, newvisited);
                    }
                    if (grid[thisx - 1, thisy] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx - 1, thisy, 3, newscore, newvisited);
                    }
                    break;
                case 3:
                    WalkGrid(grid, thisx - 1, thisy, 3, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx, thisy - 1] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx, thisy - 1, 0, newscore, newvisited);
                    }
                    if (grid[thisx, thisy + 1] != "#")
                    {
                        newvisited = new HashSet<(int, int)>(visited);
                        WalkGrid(grid, thisx, thisy + 1, 2, newscore, newvisited);
                    }
                    break;
            }

        }

        void WalkGrid2(string[,] grid, int thisx, int thisy, int dir, long thisscore, List<long> visited)
        {


            if (grid[thisx, thisy] == "#")
            {
                return;
            }

            if (thisx==14 && thisy==9)
            {
                int abc = 0;
            }
            if (thisscore > 83432 || thisscore > minscore)
            {
                return;
            }

            if (grid[thisx, thisy] == "E")
            {
                if (thisscore == 83432)
                {
                    foreach (long pos in visited)
                    {
                        if (!obs.Contains(pos))
                        {
                            obs.Add(pos);
                        }
                    }
                }
                return;
            }

            int thispos = thisx + thisy * 1000;
            //int visitedpos = thispos + dir * 1000000;
            

            if (visited2.ContainsKey(thispos))
            {
                if (visited2[thispos] < thisscore-1000)
                {
                    return;
                }
                else
                {
                    if (thisscore < visited2[thispos])
                    {
                        visited2[thispos] = thisscore;
                    }
                        
                }
            }
            else
            {
                visited2[thispos] = thisscore;
            }

            if (!visited.Contains(thispos))
            {
                visited.Add(thispos); 
            }
            else
            {
                //return;
            }



            long newscore = newscore = thisscore + 1;
            //HashSet<(int, int)> newvisited = new HashSet<(int, int)>(visited);
            List<long> newvisited = new List<long>(visited);
            switch (dir)
            {
                case 0:

                    WalkGrid2(grid, thisx, thisy - 1, 0, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx + 1, thisy] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx + 1, thisy, 1, newscore, newvisited);
                    }
                    if (grid[thisx - 1, thisy] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx - 1, thisy, 3, newscore, newvisited);
                    }

                    break;
                case 1:
                    WalkGrid2(grid, thisx + 1, thisy, 1, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx, thisy - 1] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx, thisy - 1, 0, newscore, newvisited);
                    }
                    if (grid[thisx, thisy + 1] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx, thisy + 1, 2, newscore, newvisited);
                    }


                    break;
                case 2:
                    WalkGrid2(grid, thisx, thisy + 1, 2, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx + 1, thisy] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx + 1, thisy, 1, newscore, newvisited);
                    }
                    if (grid[thisx - 1, thisy] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx - 1, thisy, 3, newscore, newvisited);
                    }
                    break;
                case 3:
                    WalkGrid2(grid, thisx - 1, thisy, 3, newscore, newvisited);
                    newscore = thisscore + 1001;
                    if (grid[thisx, thisy - 1] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx, thisy - 1, 0, newscore, newvisited);
                    }
                    if (grid[thisx, thisy + 1] != "#")
                    {
                        newvisited = new List<long>(visited);
                        WalkGrid2(grid, thisx, thisy + 1, 2, newscore, newvisited);
                    }
                    break;
            }



        }
    }
    
}

