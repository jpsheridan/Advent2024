using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Policy;
using System.CodeDom;

namespace AdventCode
{
    
    public class Day14:DayClass
    {

        public override string Part1()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            //string[,] grid = new string[101, 103];
            //string[,] grid = new string[11, 7];
            int maxx = 101;
            int maxy = 103;

            List<int> px = new List<int>();
            List<int> py = new List<int>();
            List<int> vx = new List<int>();
            List<int> vy = new List<int>();
            while ((ln = sr.ReadLine()) != null)
            {
                string[] s = ln.Split(' ');

                string[] p = s[0].Replace("p=", "").Split(',');
                string[] v = s[1].Replace("v=", "").Split(',');

                px.Add(int.Parse(p[0]));
                py.Add(int.Parse(p[1]));
                vx.Add(int.Parse(v[0]));
                vy.Add(int.Parse(v[1]));
            }

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < px.Count; j++)
                {

                    int newx = px[j] + vx[j];
                    if (newx < 0)
                    {
                        newx += maxx;
                    }
                    else
                    {
                        newx = newx % maxx;
                    }
                    px[j] = newx;

                    int newy = py[j] + vy[j];
                    if (newy < 0)
                    {
                        newy += maxy;
                    }
                    else
                    {
                        newy = newy % maxy;
                    }
                    py[j] = newy;
                    
                }
            }

            int midx = maxx / 2;
            int midy = maxy / 2;

            int[] quad = new int[4];

            
            for (int j = 0; j < px.Count; j++)
            {
                    
                if (px[j] >=0 && px[j] < midx)
                {
                    if (py[j] >= 0 && py[j] < midy)
                    {
                        quad[0]++;
                    }
                    else if (py[j] > midy)
                    {
                        quad[2]++;
                    }
                }
                else if (px[j] > midx)
                {
                    if (py[j] >= 0 && py[j] < midy)
                    {
                        quad[1]++;
                    }
                    else if (py[j] > midy)
                    {
                        quad[3]++;
                    }
                }
            }
            

            valid = quad[0] * quad[1] * quad[2] * quad[3];

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

            //string[,] grid = new string[101, 103];
            //string[,] grid = new string[11, 7];
            int maxx = 101;
            int maxy = 103;

            List<int> px = new List<int>();
            List<int> py = new List<int>();

            List<int> origx = new List<int>();
            List<int> origy = new List<int>();
            List<int> vx = new List<int>();
            List<int> vy = new List<int>();
            while ((ln = sr.ReadLine()) != null)
            {
                string[] s = ln.Split(' ');

                string[] p = s[0].Replace("p=", "").Split(',');
                string[] v = s[1].Replace("v=", "").Split(',');

                px.Add(int.Parse(p[0]));
                py.Add(int.Parse(p[1]));
                origx.Add(int.Parse(p[0]));
                origy.Add(int.Parse(p[1]));
                vx.Add(int.Parse(v[0]));
                vy.Add(int.Parse(v[1]));
            }

            for (int i = 1; i < 7000; i++)
            {
                string[,] grid = new string[maxx, maxy];
                for (int j = 0; j < px.Count; j++)
                {

                    int newx = px[j] + vx[j];
                    if (newx < 0)
                    {
                        newx += maxx;
                    }
                    else
                    {
                        newx = newx % maxx;
                    }
                    px[j] = newx;

                    int newy = py[j] + vy[j];
                    if (newy < 0)
                    {
                        newy += maxy;
                    }
                    else
                    {
                        newy = newy % maxy;
                    }
                    py[j] = newy;

                    grid[px[j], py[j]] = "#";
                }

                bool same = true;
                
                for (int y=0; y < maxy - 20; y++)
                {
                    for (int x = 10; x < maxx - 10; x++)
                    {
                        if (grid[x,y] == "#")
                        {
                            bool all = true;
                            for (int y2 = y+1; y2 < y+20; y2++)
                            {
                                if (grid[x,y2] != "#")
                                {
                                    all = false;
                                    break;
                                }
                            }
                            if (all)
                            {
                                Utils.DrawGrid(grid);
                                Utils.DrawBitmap(grid);
                                Debug.WriteLine("found at " + i.ToString());
                                valid = i;
                            }
                        }
                    }

                }
                
                //if (i % 10403 == 0)
                //{
                    //Utils.WriteGrid(grid,i);
                //}
                
            }
            
            //int midx = maxx / 2;
            //int midy = maxy / 2;

            //int[] quad = new int[4];


            //for (int j = 0; j < px.Count; j++)
            //{

            //    if (px[j] >= 0 && px[j] < midx)
            //    {
            //        if (py[j] >= 0 && py[j] < midy)
            //        {
            //            quad[0]++;
            //        }
            //        else if (py[j] > midy)
            //        {
            //            quad[2]++;
            //        }
            //    }
            //    else if (px[j] > midx)
            //    {
            //        if (py[j] >= 0 && py[j] < midy)
            //        {
            //            quad[1]++;
            //        }
            //        else if (py[j] > midy)
            //        {
            //            quad[3]++;
            //        }
            //    }
            //}


            //valid = quad[0] * quad[1] * quad[2] * quad[3];

            sw.Stop();
            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

    }
}
