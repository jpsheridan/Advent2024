using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace AdventCode
{
    public class Day01:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();

            while ((ln = sr.ReadLine()) != null)
            {

                string[] t = ln.Replace("   ", " ").Split(' ');
                l1.Add(int.Parse(t[0]));
                l2.Add(int.Parse(t[1]));
            }
            l1.Sort();
            l2.Sort();

            int c = 0;
            for (int i = 0; i< l1.Count; i++)
            {
                c+= Math.Abs(l1[i] - l2[i]);
            }
            sw.Stop();
            sr.Close();

            string ret = "Answer : " + c.ToString();
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

            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();

            while ((ln = sr.ReadLine()) != null)
            {

                string[] t = ln.Replace("   ", " ").Split(' ');
                l1.Add(int.Parse(t[0]));
                l2.Add(int.Parse(t[1]));
            }
            l1.Sort();
            l2.Sort();


            long c = 0;
            for (int i = 0; i < l1.Count; i++)
            {
                long c2 = 0;
                for (int j = 0; j < l2.Count; j++)
                {
                    if (l2[j] == l1[i])
                    {
                        c2++;
                    }
                }
                c += l1[i] * c2;
            }
            sw.Stop();
            sr.Close();

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

    }
}
