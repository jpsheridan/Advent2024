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
    public class Day13 : DayClass
    {
        Dictionary<int, long> comp = new Dictionary<int, long>();
        object o = new object();
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            long valid = 0;

            //string[] nums = sr.ReadLine().Split(',');
            string ln = "";
            int test = 0;
            while  (ln != null)
            {
                test++;
                string ba = sr.ReadLine();
                string bb = sr.ReadLine();
                string pz = sr.ReadLine();
                ln = sr.ReadLine();

                long ax = 0;
                long ay = 0;
                long bx = 0;
                long by = 0;
                long px = 0;
                long py = 0;

                GetXY(ba, out ax, out ay);
                GetXY(bb, out bx, out by);
                GetXY(pz, out px, out py);

                if (test == 10)
                {
                    int abc = 0;
                }
                long mintoken = GetSolution(ax, ay, bx, by, px, py);
                comp[test] = mintoken;
                valid += mintoken;
            }
            
            sr.Close();






            sw.Stop();


            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        //public override string Part2()
        //{
            // semi brute force but completes in ~ 30 mins
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

        //    long valid = 0;

        //    //string[] nums = sr.ReadLine().Split(',');
        //    string ln = "";
        //    int test = 0;

        //    List<string> sba = new List<string>();
        //    List<string> sbb = new List<string>();
        //    List<string> spz = new List<string>();

        //    while (ln != null)
        //    {
        //        test++;
        //        string ba = sr.ReadLine();
        //        string bb = sr.ReadLine();
        //        string pz = sr.ReadLine();
        //        ln = sr.ReadLine();
        //        sba.Add(ba);
        //        sbb.Add(bb);
        //        spz.Add(pz);
        //    }

        //    Parallel.For(0, sba.Count, i =>
        //    {
        //        long ax = 0;
        //        long ay = 0;
        //        long bx = 0;
        //        long by = 0;
        //        long px = 0;
        //        long py = 0;



        //        GetXY(sba[i], out ax, out ay);
        //        GetXY(sbb[i], out bx, out by);
        //        GetXY(spz[i], out px, out py);
        //        py += 10000000000000;
        //        px += 10000000000000;

        //        long mintoken = GetSolution4(ax, ay, bx, by, px, py);

        //        //if (comp[test] != mintoken)
        //        //{
        //        //   Debug.WriteLine(test.ToString() + " - " + mintoken.ToString());
        //        //}

        //        lock (o)
        //        {
        //            valid += mintoken;
        //        }
                
        //    });

        //    sr.Close();






        //    sw.Stop();


        //    string ret = "Answer : " + valid.ToString();
        //    ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
        //    return ret;

        //}

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            long valid = 0;

            //string[] nums = sr.ReadLine().Split(',');
            string ln = "";
            int test = 0;

            while (ln != null)
            {
                test++;
                string ba = sr.ReadLine();
                string bb = sr.ReadLine();
                string pz = sr.ReadLine();
                ln = sr.ReadLine();

                long ax = 0;
                long ay = 0;
                long bx = 0;
                long by = 0;
                long px = 0;
                long py = 0;



                GetXY(ba, out ax, out ay);
                GetXY(bb, out bx, out by);
                GetXY(pz, out px, out py);
                py += 10000000000000;
                px += 10000000000000;

                long mintoken = GetSolution5(ax, ay, bx, by, px, py);

                //if (comp[test] != mintoken)
                //{
                //   Debug.WriteLine(test.ToString() + " - " + mintoken.ToString());
                //}


                valid += mintoken;
            }

            sr.Close();


            sw.Stop();


            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

            void GetXY(string inp, out long x, out long y)
        {
            string[] s = inp.Split(':');
            
            // prizes use = instead of +
            s[1] = s[1].Replace("=", "+");
            string[] xy = s[1].Split(',');
            
            x = long.Parse(xy[0].Substring(xy[0].IndexOf('+')));
            y = long.Parse(xy[1].Substring(xy[1].IndexOf('+')));

        }

        long GetSolution(long ax, long ay, long bx, long by, long px, long py)
        {
            if (((ax * 101 + bx*101) < px) || ((ay*101 + by*101) < py))
            {
                return 0;
            }

            long mincost = 500;

            for (long a=100; a> 0; a--)
            {
                for (long b=100; b>0; b--)
                {
                    if (((ax*a + bx*b)==px) && ((ay*a + by*b) == py))
                    {
                        long thiscost = a * 3 + b;
                        if (thiscost < mincost)
                        {
                            mincost = thiscost;
                        }
                    }
                }
            }

            if (mincost == 500)
            {
                mincost = 0;
            }

            return mincost;

        }

        long GetSolution2(long ax, long ay, long bx, long by, long px, long py)
        {
            //if (((ax * 100 + bx * 100) < px) || ((ay * 100 + by * 100) < py))
            //{
            //return 0;
            //}

            long maxa = Math.Min(px / ax, py/ay);
            long maxb = Math.Min(px / bx, py / by);


            long mincost = 500;

            for (long a = maxa; a > 0; a--)
            {
                for (long b = maxb; b > 0; b--)
                {

                    if (((ax * a + bx * b) == px) && ((ay * a + by * b) == py))
                    {
                        long thiscost = a * 3 + b;
                        if (thiscost < mincost)
                        {
                            mincost = thiscost;
                        }
                    }
                }
            }


            if (mincost == 500)
            {
                mincost = 0;
            }


            return mincost;

        }

        long GetSolution3(long ax, long ay, long bx, long by, long px, long py)
        {
            //if (((ax * 100 + bx * 100) < px) || ((ay * 100 + by * 100) < py))
            //{
            //return 0;
            //}

            long maxa = Math.Min(px / ax, py / ay);
            long maxb = Math.Min(px / bx, py / by);
            //long mincost = long.MaxValue;

            long aa = ax - ay;
            long bb = bx - by;
            long pz = px - py;





            if (aa >= 0)
            {
                long minx = pz/aa;
                
                for (long a = minx; a < maxa; a+=3)
                {
                    long res = pz - (aa * a);

                    if (res % bb == 0)
                    {
                        long b = res / bb;
                        if (((ax * a + bx * b) == px) && ((ay * a + by * b) == py))
                        {
                            return a * 3 + b;
                        }
                    }
                }

            }


            return 0;

        }


        long GetSolution4(long ax, long ay, long bx, long by, long px, long py)
        {
            
            long maxa = Math.Min(px / ax, py / ay);
            long maxb = Math.Min(px / bx, py / by);

            long mina = 0;
            long minb = 0;
            if (ax < ay)
            {
                minb = (px - (ax * maxa)) / bx;
            }
            else
            {
                minb = (py - (ay * maxa)) / by;
            }

            if (bx < by)
            {
                mina = (px - (bx * maxb)) / ax;
            }
            else
            {
                mina = (py - (by * maxb)) / ay;
            }

            //long mincost = long.MaxValue;

            long aa = ax - ay;
            long bb = bx - by;
            long pz = px - py;


            long lcm = 0;
            if (aa == 0 || bb == 0)
            {
                lcm = 1;
            }
            else if (Math.Abs(aa) == Math.Abs(bb))

            {
                lcm = 1;
            }
            else
            {
                lcm = findLCM(Math.Abs(aa), Math.Abs(bb));
            }


            long mult = 1;

            if (lcm == Math.Abs(aa) || lcm == Math.Abs(bb))
            {
                mult = 1;
            }
            else
            {
                if (aa > 0 && lcm > 1)
                {
                    mult = lcm / aa;
                }
            }
                

  
            
            if (aa >= 0)
            {
                long astart = mina;
                if (lcm > 1)
                {
                    
                    for (int i = 0; i <= Math.Max(aa, mult); i++)
                    {
                        long sum = (mina + i) * aa;
                        if (((sum - pz) % Math.Abs(bb)) == 0)
                        {
                            astart = mina + i;
                            break;
                        }
                    }
                }

                for (long a = astart; a <= maxa; a += mult)
                {
                    long res = pz - (aa * a);
                    if (res % bb == 0)
                    {
                        long b = res / bb;
                        if (((ax * a + bx * b) == px) && ((ay * a + by * b) == py))
                        {
                            return a * 3 + b;
                        }
                    }
                }

            }
            else
            {
                long bstart = minb;
                if (lcm > 1)
                {
                    for (int i = 0; i <= Math.Max(bb,mult); i++)
                    {
                        long sum = (minb + i) * bb;
                        if (((sum - pz) % Math.Abs(aa)) == 0)
                            {
                            bstart = minb + i;
                            break;
                        }
                    }
                }

                for (long b = bstart; b <= maxb; b += mult)
                {
                    long res = pz - (bb * b);
                    if (res % aa == 0)
                    {
                        long a = res / aa;
                        if (((ax * a + bx * b) == px) && ((ay * a + by * b) == py))
                        {
                            return a * 3 + b;
                        }
                    }
                }
            }


            return 0;

        }

        long GetSolution5(long ax, long ay, long bx, long by, long px, long py)
        {

            long maxa = Math.Min(px / ax, py / ay);
            long maxb = Math.Min(px / bx, py / by);
            //long mincost = long.MaxValue;


            long bfact = bx * ay - ax * by;
            long btot = px * ay - ax * py;

            long a = 0;
            long b = 0;
            if (btot % bfact == 0)
            {
                b = btot / bfact;

                a = (px - (b * bx)) / ax;

                if ((a*ax + b*bx)==px && (a*ay + b * by) == py)
                {
                    return 3 * a + b;
                }

                
            }


            return 0;

        }

        public static long findLCM(long a, long b)
        {
            long num1, num2;

            if (a > b)
            {
                num1 = a;
                num2 = b;
            }
            else
            {
                num1 = b;
                num2 = a;
            }

            if (num1  % num2 == 0)
            {
                return num1;
            }

            for (int i = 3; i <= num2; i += 1)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num2;
        }
    }
}
