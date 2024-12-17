using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace AdventCode
{
    public class Day12:DayClass
    {
        long area = 0;
        long perimeter = 0;
        List<int> allvisited = new List<int>();
        List<int> visited = new List<int>();
        List<int> lefts = new List<int>();
        List<int> rights = new List<int>();
        List<int> tops = new List<int>();
        List<int> bots = new List<int>();

        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            long cost = 0;
            for (int y=0; y<grid.GetLength(1);y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    int pos = x + y * 1000;
                    //if (grid[x,y] != ".")
                    if (!allvisited.Contains(pos))
                    {
                        string[,] newgrid = (string[,])grid.Clone();
                        area = 0;
                        perimeter = 0;
                        visited = new List<int>();

                        string plottype = grid[x, y];
                        GetPlotSize(newgrid, x, y, plottype);
                        
                        area = visited.Count;
                        cost += area * perimeter;
                    }
                }
            }

            sw.Stop();
            

            string ret = "Answer : " + cost.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            long cost = 0;
            allvisited.Clear();
            
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    int pos = x + y * 1000;
                    //if (grid[x,y] != ".")
                    if (!allvisited.Contains(pos))
                    {
                        string[,] newgrid = (string[,])grid.Clone();
                        area = 0;
                        perimeter = 0;
                        visited.Clear();
                        lefts.Clear();
                        rights.Clear();
                        tops.Clear();
                        bots.Clear();

                        string plottype = grid[x, y];
                        GetPlotSize(newgrid, x, y, plottype);

                        area = visited.Count;
                        long sides =  GetSides2(grid, plottype);
                        cost += area * sides;
                        Debug.WriteLine(plottype + " - " + area.ToString() + " - " + sides.ToString());
                    }
                }
            }

            sw.Stop();


            string ret = "Answer : " + cost.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        void GetPlotSize(string[,] grid, int thisx, int thisy, string plottype)
        {
            
            visited.Add(thisx + thisy * 1000);
            allvisited.Add(thisx + thisy * 1000);

            grid[thisx, thisy] = ".";

            if (thisx > 0)
            {
                if (grid[thisx-1,thisy] != plottype)
                {
                    if (grid[thisx - 1, thisy] != ".")
                    {
                        perimeter += 1;
                    }
                    
                }
                else
                {
                    GetPlotSize(grid, thisx - 1, thisy, plottype);
                }
            }
            else
            {
                perimeter += 1;
            }

            if (thisx < grid.GetLength(0)-1)
            {
                if (grid[thisx + 1, thisy] != plottype )
                {
                    if (grid[thisx + 1, thisy] != ".")
                    {
                        perimeter += 1;
                    }
                    
                }
                else
                {
                    GetPlotSize(grid, thisx + 1, thisy, plottype);
                }
            }
            else
            {
                perimeter += 1;
            }

            if (thisy > 0)
            {
                if (grid[thisx, thisy-1] != plottype)
                {
                    if (grid[thisx, thisy - 1] != ".")
                    {
                        perimeter += 1;
                    }
                    
                }
                else
                {
                    GetPlotSize(grid, thisx, thisy-1, plottype);
                }
            }
            else
            {
                perimeter += 1;
            }

            if (thisy < grid.GetLength(1) - 1)
            {
                if (grid[thisx, thisy+1] != plottype)
                {
                    if (grid[thisx, thisy + 1] != ".")
                    {
                        perimeter += 1;
                    }
                    
                }
                else
                {
                    GetPlotSize(grid, thisx, thisy+1, plottype);
                }
            }
            else
            {
                perimeter += 1;
            }

        }

        long GetSides(string[,] grid, string plottype)
        {
            int sides = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    int pos = x + y * 1000;
                    int pos2 = 0;
                    if (visited.Contains(pos))
                    {
                        if (!lefts.Contains(pos))
                        {
                            if (x == 0 || x > 0 && grid[x - 1, y] != plottype)
                            {
                                sides++;
                                lefts.Add(pos);
                                for (int dn = y + 1; dn < grid.GetLength(1); dn++)
                                {
                                    if (grid[x, dn] == plottype)
                                    {
                                        if (x == 0 || x > 0 && grid[x - 1, dn] != plottype)
                                        {
                                            pos2 = x + dn * 1000;
                                            lefts.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                for (int dn = y - 1; dn >= 0; dn--)
                                {
                                    if (grid[x, dn] == plottype)
                                    {
                                        if (x == 0 || x > 0 && grid[x - 1, dn] != plottype)
                                        {
                                            pos2 = x + dn * 1000;
                                            lefts.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                            
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }


                        if (!rights.Contains(pos))
                        {
                            if (x == grid.GetLength(0)-1 || x < grid.GetLength(0)-1 && grid[x + 1, y] != plottype)
                            {
                                sides++;
                                rights.Add(pos);
                                for (int dn = y + 1; dn < grid.GetLength(1); dn++)
                                {
                                    if (grid[x, dn] == plottype)
                                    {
                                        if (x == grid.GetLength(0) - 1 || x < grid.GetLength(0) - 1 && grid[x + 1, dn] != plottype)
                                        {
                                            pos2 = x + dn * 1000;
                                            rights.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                for (int dn = y - 1; dn >= 0; dn--)
                                {
                                    if (grid[x, dn] == plottype)
                                    {
                                        if (x == grid.GetLength(0) - 1 || x < grid.GetLength(0) - 1 && grid[x + 1, dn] != plottype)
                                        {
                                            pos2 = x + dn * 1000;
                                            rights.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        if (!bots.Contains(pos))
                        {
                            if (y == grid.GetLength(1) - 1 || y < grid.GetLength(1) - 1 && grid[x, y+1] != plottype)
                            {
                                sides++;
                                bots.Add(pos);
                                for (int dn = x + 1; dn < grid.GetLength(1); dn++)
                                {
                                    if (grid[dn, y] == plottype)
                                    {
                                        if (y == grid.GetLength(1) - 1 || y < grid.GetLength(1) - 1 && grid[dn, y + 1] != plottype)
                                        {
                                            pos2 = dn + y * 1000;
                                            bots.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                for (int dn = x - 1; dn >= 0; dn--)
                                {
                                    if (grid[dn, y] == plottype)
                                    {
                                        if (y == grid.GetLength(1) - 1 || y < grid.GetLength(1) - 1 && grid[dn, y + 1] != plottype)
                                        {
                                            pos2 = dn + y * 1000;
                                            bots.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        if (!tops.Contains(pos))
                        {
                            if (y == 0 || y > 0 && grid[x, y-1] != plottype)
                            {
                                sides++;
                                tops.Add(pos);
                                for (int dn = x + 1; dn < grid.GetLength(0); dn++)
                                {
                                    if (grid[dn, y] == plottype)
                                    {
                                        if (y == 0 || y > 0 && grid[dn, y - 1] != plottype)
                                        {
                                            pos2 = dn + y * 1000;
                                            tops.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                for (int dn = x - 1; dn >= 0; dn--)
                                {
                                    if (grid[dn, y] == plottype)
                                    {
                                        if (y == 0 || y > 0 && grid[dn, y - 1] != plottype)
                                        {
                                            pos2 = dn + y * 1000;
                                            tops.Add(pos2);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }


                    }
                }
            }

           return sides;
        }


        long GetSides2(string[,] grid, string plottype)
        {
            int sides = 0;

            foreach (int pos in visited)
            {

                //int pos = x + y * 1000;
                int pos2 = 0;
                int x = pos % 1000;
                int y = pos / 1000;
                //if (visited.Contains(pos))
                {
                    if (!lefts.Contains(pos))
                    {
                        if (x == 0 || x > 0 && grid[x - 1, y] != plottype)
                        {
                            sides++;
                            lefts.Add(pos);
                            for (int dn = y + 1; dn < grid.GetLength(1); dn++)
                            {
                                if (grid[x, dn] == plottype)
                                {
                                    if (x == 0 || x > 0 && grid[x - 1, dn] != plottype)
                                    {
                                        pos2 = x + dn * 1000;
                                        lefts.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int dn = y - 1; dn >= 0; dn--)
                            {
                                if (grid[x, dn] == plottype)
                                {
                                    if (x == 0 || x > 0 && grid[x - 1, dn] != plottype)
                                    {
                                        pos2 = x + dn * 1000;
                                        lefts.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }


                    if (!rights.Contains(pos))
                    {
                        if (x == grid.GetLength(0) - 1 || x < grid.GetLength(0) - 1 && grid[x + 1, y] != plottype)
                        {
                            sides++;
                            rights.Add(pos);
                            for (int dn = y + 1; dn < grid.GetLength(1); dn++)
                            {
                                if (grid[x, dn] == plottype)
                                {
                                    if (x == grid.GetLength(0) - 1 || x < grid.GetLength(0) - 1 && grid[x + 1, dn] != plottype)
                                    {
                                        pos2 = x + dn * 1000;
                                        rights.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int dn = y - 1; dn >= 0; dn--)
                            {
                                if (grid[x, dn] == plottype)
                                {
                                    if (x == grid.GetLength(0) - 1 || x < grid.GetLength(0) - 1 && grid[x + 1, dn] != plottype)
                                    {
                                        pos2 = x + dn * 1000;
                                        rights.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (!bots.Contains(pos))
                    {
                        if (y == grid.GetLength(1) - 1 || y < grid.GetLength(1) - 1 && grid[x, y + 1] != plottype)
                        {
                            sides++;
                            bots.Add(pos);
                            for (int dn = x + 1; dn < grid.GetLength(1); dn++)
                            {
                                if (grid[dn, y] == plottype)
                                {
                                    if (y == grid.GetLength(1) - 1 || y < grid.GetLength(1) - 1 && grid[dn, y + 1] != plottype)
                                    {
                                        pos2 = dn + y * 1000;
                                        bots.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int dn = x - 1; dn >= 0; dn--)
                            {
                                if (grid[dn, y] == plottype)
                                {
                                    if (y == grid.GetLength(1) - 1 || y < grid.GetLength(1) - 1 && grid[dn, y + 1] != plottype)
                                    {
                                        pos2 = dn + y * 1000;
                                        bots.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (!tops.Contains(pos))
                    {
                        if (y == 0 || y > 0 && grid[x, y - 1] != plottype)
                        {
                            sides++;
                            tops.Add(pos);
                            for (int dn = x + 1; dn < grid.GetLength(0); dn++)
                            {
                                if (grid[dn, y] == plottype)
                                {
                                    if (y == 0 || y > 0 && grid[dn, y - 1] != plottype)
                                    {
                                        pos2 = dn + y * 1000;
                                        tops.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int dn = x - 1; dn >= 0; dn--)
                            {
                                if (grid[dn, y] == plottype)
                                {
                                    if (y == 0 || y > 0 && grid[dn, y - 1] != plottype)
                                    {
                                        pos2 = dn + y * 1000;
                                        tops.Add(pos2);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }


                }
            }

            return sides;
        }
    }
}
