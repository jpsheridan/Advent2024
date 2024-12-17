using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace AdventCode
{
    public class Day04:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[,] grid = new string[1, 1];

            //StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");
            grid = Utils.GetGridFromFile0("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            //string[] nums = sr.ReadLine().Split(',');
            
            for (int x=0; x < grid.GetLength(0); x++)
            {
                for (int y=0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == "X")
                    { 
                        // check right
                        if (x < grid.GetLength(0)-3)    
                        {
                            if ((grid[x+1, y] == "M") && (grid[x + 2, y] == "A" && (grid[x + 3, y] == "S")))
                            {
                                valid++;
                            }
                        }
                        // check left
                        if (x > 2)
                        {
                            if ((grid[x - 1, y] == "M") && (grid[x - 2, y] == "A" && (grid[x - 3, y] == "S")))
                            {
                                valid++;
                            }
                        }

                        // check down
                        if (y < grid.GetLength(1) - 3)
                        {
                            if ((grid[x, y+1] == "M") && (grid[x, y + 2] == "A" && (grid[x, y + 3] == "S")))
                            {
                                valid++;
                            }
                        }

                        // up

                        if (y > 2)
                        {
                            if ((grid[x, y - 1] == "M") && (grid[x, y - 2] == "A" && (grid[x, y - 3] == "S")))
                            {
                                valid++;
                            }
                        }

                        // up right
                        if (y > 2 && (x < grid.GetLength(0) - 3))
                        {
                            if ((grid[x + 1, y - 1] == "M") && (grid[x + 2, y - 2] == "A" && (grid[x + 3, y - 3] == "S")))
                            {
                                valid++;
                            }
                        }

                        // up left
                        if (y > 2 && x > 2)
                        {
                            if ((grid[x -1, y - 1] == "M") && (grid[x - 2, y - 2] == "A" && (grid[x - 3, y - 3] == "S")))
                            {
                                valid++;
                            }
                        }

                        // down right
                        if ((y < grid.GetLength(1) - 3) && (x < grid.GetLength(0) - 3))
                        {
                            if ((grid[x + 1, y + 1] == "M") && (grid[x + 2, y + 2] == "A" && (grid[x + 3, y + 3] == "S")))
                            {
                                valid++;
                            }
                        }

                        // down left
                        if ((y < grid.GetLength(1) - 3) && x > 2)
                        {
                            if ((grid[x - 1, y + 1] == "M") && (grid[x - 2, y + 2] == "A" && (grid[x - 3, y + 3] == "S")))
                            {
                                valid++;
                            }
                        }

                    }
                }
            }


            sw.Stop();
            
            string ret = "Answer : " + valid.ToString();
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

            string ln = "";
            int valid = 0;

            //string[] nums = sr.ReadLine().Split(',');

            for (int x = 1; x < grid.GetLength(0)-1; x++)
            {
                for (int y = 1; y < grid.GetLength(1)-1; y++)
                {
                    if (grid[x, y] == "A")
                    {
                        if ((grid[x+1, y-1]== "S") && grid[x + 1, y + 1] == "S")
                        {
                            if ((grid[x - 1, y - 1] == "M") && grid[x - 1, y + 1] == "M")
                            {
                                valid++;
                            }
                        }

                        if ((grid[x - 1, y - 1] == "S") && grid[x + 1, y - 1] == "S")
                        {
                            if ((grid[x - 1, y + 1] == "M") && grid[x + 1, y + 1] == "M")
                            {
                                valid++;
                            }
                        }

                        if ((grid[x + 1, y - 1] == "M") && grid[x + 1, y + 1] == "M")
                        {
                            if ((grid[x - 1, y - 1] == "S") && grid[x - 1, y + 1] == "S")
                            {
                                valid++;
                            }
                        }

                        if ((grid[x - 1, y - 1] == "M") && grid[x + 1, y - 1] == "M")
                        {
                            if ((grid[x - 1, y + 1] == "S") && grid[x + 1, y + 1] == "S")
                            {
                                valid++;
                            }
                        }

                    }
                }
            }


            sw.Stop();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }


    }
}
