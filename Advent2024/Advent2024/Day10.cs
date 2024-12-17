using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    
    public class Day10 : DayClass
    {
        List<int> ends = new List<int>();

        public override string Part1()
        {
            
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            List<Tuple<int, int>> trailheads = GetTrailHeads(grid);

            int c = 0;
            foreach (Tuple<int, int> xy in trailheads)
            {
                ends = new List<int>();
                int z = ValidTrails(grid, xy.Item1, xy.Item2);
                c += ends.Count;
            }
            
            sw.Stop();
            

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            List<Tuple<int, int>> trailheads = GetTrailHeads(grid);

            int c = 0;
            int z = 0;
            foreach (Tuple<int, int> xy in trailheads)
            {
                ends = new List<int>();
                z+= ValidTrails(grid, xy.Item1, xy.Item2);
                c += ends.Count;
            }

            sw.Stop();


            string ret = "Answer : " + z.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        List<Tuple<int, int>> GetTrailHeads(string[,] grid)
        {
            List<Tuple<int, int>> th = new List<Tuple<int, int>>();
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y]=="0")
                    {
                        th.Add(new Tuple<int, int>(x, y));
                    }
                }
            }
            return th;
        }

        int ValidTrails(string[,] grid, int x, int y)
        {
            int curpos = int.Parse(grid[x, y]);

            if (curpos == 9)
            {
                int p = x + y * 1000;
                if (!ends.Contains(p))
                {
                    ends.Add(p);
                }
                return 1;
            }
            string nextpos = (curpos + 1).ToString();
            int v = 0;

            // right
            if (x < grid.GetLength(0)-1 && grid[x+1, y] == nextpos)
            {
                v += ValidTrails(grid, x + 1, y);
            }

            // left
            if (x > 0 && grid[x - 1, y] == nextpos)
            {
                v += ValidTrails(grid, x - 1, y);
            }

            // down
            if (y < grid.GetLength(1) - 1 && grid[x, y + 1] == nextpos)
            {
                v += ValidTrails(grid, x, y + 1);
            }

            // up
            if (y > 0 && grid[x, y - 1] == nextpos)
            {
                v += ValidTrails(grid, x, y - 1);
            }



            return v;
        }
    }
}
