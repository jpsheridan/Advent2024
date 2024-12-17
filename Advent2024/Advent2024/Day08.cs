using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Cryptography.Xml;

namespace AdventCode
{
    public class Day08 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            Dictionary<string, List<Tuple<int, int>>> nodes = WalkGrid(grid);

            string[,] grid2 = ProcessNodes(nodes, grid.GetLength(0), grid.GetLength(1));

            int node = CountNodes(grid2);
            
            sw.Stop();
            
            string ret = "Answer : " + node.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            Dictionary<string, List<Tuple<int, int>>> nodes = WalkGrid(grid);

            string[,] grid2 = ProcessNodes2(nodes, grid.GetLength(0), grid.GetLength(1));

            int node = CountNodes(grid2);

            sw.Stop();

            string ret = "Answer : " + node.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        Dictionary<string, List<Tuple<int, int>>>  WalkGrid(string[,] grid)
        {
            Dictionary<string, List<Tuple<int, int>>> nodes = new Dictionary<string, List<Tuple<int, int>>>();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] != ".")
                    {
                        if (!nodes.ContainsKey(grid[x,y]))
                        {
                            nodes[grid[x,y]] = new List<Tuple<int, int>>();
                        }
                        nodes[grid[x,y]].Add(new Tuple<int, int>(x, y));
                    }
                }
            }
            return nodes;

        }

        string[,] ProcessNodes(Dictionary<string, List<Tuple<int, int>>> nodes,  int maxx, int maxy)
        {
            
            string[,] grid2 = new string[maxx,maxy];

            
            
            foreach (KeyValuePair<string, List<Tuple<int, int>>> kvp in nodes)
            {
                for (int i = 0; i < kvp.Value.Count-1; i++)
                {
                    for (int j=i+1; j < kvp.Value.Count; j++)
                    {
                        int xdiff = kvp.Value[i].Item1 - kvp.Value[j].Item1;
                        int ydiff = kvp.Value[i].Item2 - kvp.Value[j].Item2;

                        //
                        int x1 = kvp.Value[i].Item1 + xdiff;
                        int x2 = kvp.Value[j].Item1 - xdiff;

                        int y1 = kvp.Value[i].Item2 + ydiff;
                        int y2 = kvp.Value[j].Item2 - ydiff;

                        if (x1==5 || x2==5)
                        {
                            int abc = 0;
                        }

                        if (x1 >= 0 && x1 < maxx && y1 >= 0 && y1 < maxy)
                        {
                            grid2[x1, y1] = "#";
                        }

                        if (x2 >= 0 && x2 < maxx && y2 >= 0 && y2 < maxy)
                        {
                            grid2[x2, y2] = "#";
                        }
                    }
                    
                }
            }

            return grid2;
        }

        string[,] ProcessNodes2(Dictionary<string, List<Tuple<int, int>>> nodes, int maxx, int maxy)
        {

            string[,] grid2 = new string[maxx, maxy];



            foreach (KeyValuePair<string, List<Tuple<int, int>>> kvp in nodes)
            {
                for (int i = 0; i < kvp.Value.Count - 1; i++)
                {
                    for (int j = i + 1; j < kvp.Value.Count; j++)
                    {
                        int xdiff = kvp.Value[i].Item1 - kvp.Value[j].Item1;
                        int ydiff = kvp.Value[i].Item2 - kvp.Value[j].Item2;

                        //
                        int x1 = kvp.Value[i].Item1 + xdiff;
                        int x2 = kvp.Value[j].Item1 - xdiff;

                        int y1 = kvp.Value[i].Item2 + ydiff;
                        int y2 = kvp.Value[j].Item2 - ydiff;
                        grid2[kvp.Value[i].Item1, kvp.Value[i].Item2] = "#";
                        grid2[kvp.Value[j].Item1, kvp.Value[j].Item2] = "#";

                        while (x1 >= 0 && x1 < maxx && y1 >= 0 && y1 < maxy)
                        {
                            grid2[x1, y1] = "#";
                            x1 += xdiff;
                            y1 += ydiff;
                        }

                        while (x2 >= 0 && x2 < maxx && y2 >= 0 && y2 < maxy)
                        {
                            grid2[x2, y2] = "#";
                            x2 -= xdiff;
                            y2 -= ydiff;
                        }
                    }

                }
            }

            return grid2;
        }

        int CountNodes(string[,] grid)
        {
            int c = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y=0; y< grid.GetLength(1); y++)
                {
                    if (grid[x,y]=="#")
                    {
                        c++;
                    }
                }
            }
            return c;
        }

    }
}
