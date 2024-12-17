using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    
    public class Day15:DayClass
    {
        List<int> boxes = new List<int>();
        public override string Part1()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");
            
            string ln = "";
            int valid = 0;

            int r = 0;
            int c = 0;

            // start from 0,0

            ln = sr.ReadLine();
            r++;
            c = ln.Length;
            while ((ln = sr.ReadLine()) != null)
            {
                if (ln == "")
                {
                    break;
                }
                r++;
            }
            
            string[,] grid = new string[c, r];

            r = 0;
            c = 0;

            string moves = "";
            while ((ln = sr.ReadLine()) != null)
            {
                moves += ln;
            }


            sr.BaseStream.Position = 0;

            int startx = 0;
            int starty = 0;

            while ((ln = sr.ReadLine()) != null)
            {

                for (c = 0; c < ln.Length; c++)
                {
                    grid[c, r] = ln.Substring(c, 1);
                    if (grid[c,r]=="@")
                    {
                        startx = c;
                        starty = r;
                    }
                }
                if (ln == "")
                {
                    break;
                }
                r++;
            }


            DoMoves(grid, moves,startx,starty);
            
            valid = GetGPS(grid);

           // Utils.DrawGrid(grid);
            sw.Stop();
            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            int r = 0;
            int c = 0;

            // start from 0,0

            ln = sr.ReadLine();
            r++;
            c = ln.Length;
            while ((ln = sr.ReadLine()) != null)
            {
                if (ln == "")
                {
                    break;
                }
                r++;
            }

            string[,] grid = new string[c, r];
            string[,] grid2 = new string[c * 2, r];
            r = 0;
            c = 0;

            string moves = "";
            while ((ln = sr.ReadLine()) != null)
            {
                moves += ln;
            }


            sr.BaseStream.Position = 0;

            int startx = 0;
            int starty = 0;

            while ((ln = sr.ReadLine()) != null)
            {

                for (c = 0; c < ln.Length; c++)
                {
                    grid[c, r] = ln.Substring(c, 1);
                    switch(grid[c,r])
                    {
                        case "#":
                            grid2[c * 2, r] = "#";
                            grid2[(c * 2)+1, r] = "#";
                            break;
                        case "O":
                            grid2[c * 2, r] = "[";
                            grid2[(c * 2) + 1, r] = "]";
                            break;
                        case ".":
                            grid2[c * 2, r] = ".";
                            grid2[(c * 2) + 1, r] = ".";
                            break;
                        case "@":
                            grid2[c * 2, r] = "@";
                            grid2[(c * 2) + 1, r] = ".";
                            break;
                    }
                    
                    if (grid[c, r] == "@")
                    {
                        startx = c*2;
                        starty = r;
                    }
                }
                if (ln == "")
                {
                    break;
                }
                r++;
            }

            

            DoMoves3(grid2, moves, startx, starty);

            valid = GetGPS2(grid2);

            Utils.DrawGrid(grid2);
            sw.Stop();
            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

   

        int GetGPS(string[,] grid)
        {
            int c = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x,y]=="O")
                    {
                        c += x + y * 100;
                    }
                }
            }

            return c;
        }

        int GetGPS2(string[,] grid)
        {
            int c = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == "[")
                    {
                        c += x + y * 100;
                    }
                }
            }

            return c;
        }

        void DoMoves(string[,] grid, string moves, int x, int y)
        {

            int newx = x;
            int newy = y;

            for (int i = 0; i < moves.Length; i++)
            {
                x = newx;
                y = newy;
                switch (moves.Substring(i, 1))
                {
                    case "^":
                        newy--;
                        break;
                    case ">":
                        newx++;
                        break;
                    case "v":
                        newy++;
                        break;
                    case "<":
                        newx--;
                        break;
                }

                if (grid[newx, newy] == "#")
                {
                    // don't move.
                    newx = x;
                    newy = y;
                }
                else if (grid[newx, newy] == ".")
                {
                    grid[newx, newy] = "@";
                    grid[x, y] = ".";
                }
                else if (grid[newx, newy] == "O")
                {
                    // check if box can move
                    bool bmoved = false;
                    switch (moves.Substring(i, 1))
                    {
                        case "^":
                            
                            for (int y1 = newy-1; y1 >=0; y1--)
                            {
                                if (grid[x, y1] == "#")
                                {
                                    break;
                                }
                                else if (grid[x, y1] == ".")
                                {
                                    grid[x, y1] = "O";
                                    grid[x, newy] = "@";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                        case ">":
                            for (int x1 = newx; x1 < grid.GetLength(0); x1++)
                            {
                                if (grid[x1, y] == "#")
                                {
                                    break;
                                }
                                else if (grid[x1, y] == ".")
                                {
                                    grid[x1, y] = "O";
                                    grid[newx, y] = "@";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                        case "v":
                            for (int y1 = newy + 1; y1 < grid.GetLength(1); y1++)
                            {
                                if (grid[x, y1] == "#")
                                {
                                    break;
                                }
                                else if (grid[x, y1] == ".")
                                {
                                    grid[x, y1] = "O";
                                    grid[x, newy] = "@";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                        case "<":
                            for (int x1 = newx; x1 >= 0; x1--)
                            {
                                if (grid[x1, y] == "#")
                                {
                                    break;
                                }
                                else if (grid[x1, y] == ".")
                                {
                                    grid[x1, y] = "O";
                                    grid[newx, y] = "@";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                    }
                    if (!bmoved)
                    {
                        newx = x;
                        newy = y;
                    }

                }
                else
                {
                    // shouldn't be here...
                    int abc = 0;
                }

               //Utils.DrawGrid(grid);

            }

            //return c;
        }

        void DoMoves3(string[,] grid, string moves, int x, int y)
        {

            int newx = x;
            int newy = y;

            for (int i = 0; i < moves.Length; i++)
            {
                x = newx;
                y = newy;
                switch (moves.Substring(i, 1))
                {
                    case "^":
                        newy--;
                        break;
                    case ">":
                        newx++;
                        break;
                    case "v":
                        newy++;
                        break;
                    case "<":
                        newx--;
                        break;
                }

                if (grid[newx, newy] == "#")
                {
                    // don't move.
                    newx = x;
                    newy = y;
                }
                else if (grid[newx, newy] == ".")
                {
                    grid[newx, newy] = "@";
                    grid[x, y] = ".";
                }
                else if (grid[newx, newy] == "[" || grid[newx, newy] == "]")
                {
                    // check if box can move
                    bool bmoved = false;
                    
                    switch (moves.Substring(i, 1))
                    {
                        case "^":
                            if (grid[newx, newy] == "[")
                            {
                                
                                boxes.Clear();
                                if (BoxCanMove(grid, newx, newy, -1))
                                {
                                    MoveBoxes(grid, -1);
                                    grid[newx, newy] = "@";
                                    grid[newx + 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }
                            }
                            else if (grid[newx,newy] == "]")
                            {
                                boxes.Clear();
                                if (BoxCanMove(grid, newx - 1, newy, -1))
                                {
                                    MoveBoxes(grid, -1);
                                    grid[newx, newy] = "@";
                                    grid[newx - 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }

                            }

                            break;



                        case ">":
                            for (int x1 = newx; x1 < grid.GetLength(0); x1+=2)
                            {
                                if (grid[x1, y] == "#")
                                {
                                    break;
                                }
                                else if (grid[x1, y] == ".")
                                {
                                    
                                    grid[newx, y] = "@";
                                    grid[x, y] = ".";
                                    for (int x2 = newx+1; x2 < x1; x2+=2)
                                    {
                                        grid[x2, y] = "[";
                                    }
                                    for (int x2 = newx + 2; x2 <= x1; x2 += 2)
                                    {
                                        grid[x2, y] = "]";
                                    }
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                        case "v":

                            if (grid[newx, newy] == "#")
                            {
                                break;
                            }

                            if (grid[newx, newy] == "[")
                            {
                                boxes.Clear();
                                if (BoxCanMove(grid, newx, newy, 1))
                                {
                                    MoveBoxes(grid, 1);
                                    grid[newx, newy] = "@";
                                    grid[newx + 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }
                            }
                            else if (grid[newx, newy] == "]")
                            {
                                boxes.Clear();
                                if (BoxCanMove(grid, newx - 1, newy, 1))
                                {
                                    MoveBoxes(grid, 1);
                                    grid[newx, newy] = "@";
                                    grid[newx - 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }

                            }

                            break;
                        case "<":
                            for (int x1 = newx; x1 >= 0; x1--)
                            {
                                if (grid[x1, y] == "#")
                                {
                                    break;
                                }
                                else if (grid[x1, y] == ".")
                                {
                                    grid[newx, y] = "@";
                                    grid[x, y] = ".";
                                    for (int x2 = newx - 1; x2 > x1; x2 -= 2)
                                    {
                                        grid[x2, y] = "]";
                                    }
                                    for (int x2 = newx - 2; x2 >= x1; x2 -= 2)
                                    {
                                        grid[x2, y] = "[";
                                    }
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                    }
                    if (!bmoved)
                    {
                        newx = x;
                        newy = y;
                    }

                }
                else
                {
                    // shouldn't be here...
                    int abc = 0;
                }

                //Utils.DrawGrid(grid);

            }

            //return c;
        }


        void DoMoves2(string[,] grid, string moves, int x, int y)
        {

            int newx = x;
            int newy = y;

            for (int i = 0; i < moves.Length; i++)
            {
                x = newx;
                y = newy;
                switch (moves.Substring(i, 1))
                {
                    case "^":
                        newy--;
                        break;
                    case ">":
                        newx++;
                        break;
                    case "v":
                        newy++;
                        break;
                    case "<":
                        newx--;
                        break;
                }

                if (grid[newx, newy] == "#")
                {
                    // don't move.
                    newx = x;
                    newy = y;
                }
                else if (grid[newx, newy] == ".")
                {
                    grid[newx, newy] = "@";
                    grid[x, y] = ".";
                }
                else if (grid[newx, newy] == "[" || grid[newx, newy] == "]")
                {
                    // check if box can move
                    bool bmoved = false;
                    int minx = 0;
                    int maxx = 0;
                    List<int> min = new List<int>();
                    List<int> max = new List<int>();
                    bool canmove = true;

                    switch (moves.Substring(i, 1))
                    {
                        case "^":
                            minx = newx;
                            maxx = newx;

                            if (grid[newx, newy] == "[")
                            {
                                if (grid[newx, newy - 1] == "#" || grid[newx + 1, newy - 1] == "#")
                                {
                                    //canmove = false;
                                    break;
                                }
                                if (grid[newx, newy - 1] == "." && grid[newx + 1, newy - 1] == ".")
                                {
                                    grid[newx, newy - 1] = "[";
                                    grid[newx + 1, newy - 1] = "]";
                                    grid[newx, newy] = "@";
                                    grid[newx + 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                                boxes.Clear();
                                if (BoxCanMove(grid, newx, newy, -1))
                                {
                                    MoveBoxes(grid, -1);
                                    grid[newx, newy] = "@";
                                    grid[newx + 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }
                            }
                            else if (grid[newx, newy] == "]")
                            {
                                if (grid[newx, newy - 1] == "#" || grid[newx - 1, newy - 1] == "#")
                                {
                                    //canmove = false;
                                    break;
                                }
                                if (grid[newx, newy - 1] == "." && grid[newx - 1, newy - 1] == ".")
                                {
                                    grid[newx, newy - 1] = "]";
                                    grid[newx - 1, newy - 1] = "[";
                                    grid[newx, newy] = "@";
                                    grid[newx - 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                                boxes.Clear();
                                if (BoxCanMove(grid, newx - 1, newy, -1))
                                {
                                    MoveBoxes(grid, -1);
                                    grid[newx, newy] = "@";
                                    grid[newx - 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }

                            }

                            break;



                        case ">":
                            for (int x1 = newx; x1 < grid.GetLength(0); x1 += 2)
                            {
                                if (grid[x1, y] == "#")
                                {
                                    break;
                                }
                                else if (grid[x1, y] == ".")
                                {

                                    grid[newx, y] = "@";
                                    grid[x, y] = ".";
                                    for (int x2 = newx + 1; x2 < x1; x2 += 2)
                                    {
                                        grid[x2, y] = "[";
                                    }
                                    for (int x2 = newx + 2; x2 <= x1; x2 += 2)
                                    {
                                        grid[x2, y] = "]";
                                    }
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                        case "v":
                            minx = newx;
                            maxx = newx;

                            if (grid[newx, newy] == "#")
                            {
                                break;
                            }

                            if (grid[newx, newy] == "[")
                            {
                                if (grid[newx, newy + 1] == "#" || grid[newx + 1, newy + 1] == "#")
                                {
                                    //canmove = false;
                                    break;
                                }
                                if (grid[newx, newy + 1] == "." && grid[newx + 1, newy + 1] == ".")
                                {
                                    grid[newx, newy + 1] = "[";
                                    grid[newx + 1, newy + 1] = "]";
                                    grid[newx, newy] = "@";
                                    grid[newx + 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                                boxes.Clear();
                                if (BoxCanMove(grid, newx, newy, 1))
                                {
                                    MoveBoxes(grid, 1);
                                    grid[newx, newy] = "@";
                                    grid[newx + 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }
                            }
                            else if (grid[newx, newy] == "]")
                            {
                                if (grid[newx, newy + 1] == "#" || grid[newx - 1, newy + 1] == "#")
                                {
                                    //canmove = false;
                                    break;
                                }
                                if (grid[newx, newy + 1] == "." && grid[newx - 1, newy + 1] == ".")
                                {
                                    grid[newx, newy + 1] = "]";
                                    grid[newx - 1, newy + 1] = "[";
                                    grid[newx, newy] = "@";
                                    grid[newx - 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                    break;
                                }
                                boxes.Clear();
                                if (BoxCanMove(grid, newx - 1, newy, 1))
                                {
                                    MoveBoxes(grid, 1);
                                    grid[newx, newy] = "@";
                                    grid[newx - 1, newy] = ".";
                                    grid[x, y] = ".";
                                    bmoved = true;
                                }

                            }

                            break;
                        case "<":
                            for (int x1 = newx; x1 >= 0; x1--)
                            {
                                if (grid[x1, y] == "#")
                                {
                                    break;
                                }
                                else if (grid[x1, y] == ".")
                                {
                                    grid[newx, y] = "@";
                                    grid[x, y] = ".";
                                    for (int x2 = newx - 1; x2 > x1; x2 -= 2)
                                    {
                                        grid[x2, y] = "]";
                                    }
                                    for (int x2 = newx - 2; x2 >= x1; x2 -= 2)
                                    {
                                        grid[x2, y] = "[";
                                    }
                                    bmoved = true;
                                    break;
                                }
                            }
                            break;
                    }
                    if (!bmoved)
                    {
                        newx = x;
                        newy = y;
                    }

                }
                else
                {
                    // shouldn't be here...
                    int abc = 0;
                }

                //Utils.DrawGrid(grid);

            }

            //return c;
        }


        bool BoxCanMove(string[,] grid, int startx, int starty, int dir)
        {

            
            int pos = startx + starty * 1000;
            if (!boxes.Contains(pos))
            {
                boxes.Add(pos);
            }
            else
            {
                int abc = 0;
            }
            

            if (grid[startx, starty + dir] == "#" || grid[startx + 1, starty + dir] == "#")
            {
                return false;
            }

            if (grid[startx, starty + dir] == "." && grid[startx+1, starty + dir] == ".")
            {
                return true;
            }


            if (grid[startx, starty + dir] == "[")
            {

                if (!BoxCanMove(grid, startx, starty + dir, dir))
                {
                    return false;

                }
            }
            else
            {
                if (grid[startx, starty + dir] == "]")
                {
                    if (!BoxCanMove(grid, startx-1, starty + dir, dir))
                    {
                        return false;
                    }
                }
                
                if (grid[startx+1, starty + dir] == "[")
                {
                    if (!BoxCanMove(grid, startx + 1, starty + dir, dir))
                    {
                        return false;
                    }
                }
            }






            return true;
        }

      
        void MoveBoxes(string[,] grid, int dir)
        {
            if (dir> 0)
            {
                boxes.Sort((a, b) => b.CompareTo(a));
            }
            else
            {
                boxes.Sort(); 
            }
            
            foreach (int pos in boxes)
            {
                int x = pos % 1000;
                int y = pos / 1000;

                grid[x, y + dir] = grid[x, y];
                grid[x, y] = ".";
                grid[x+1, y + dir] = grid[x+1, y];
                grid[x+1, y] = ".";
            }

        }

    }
}
