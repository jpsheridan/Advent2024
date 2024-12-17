using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day03:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            string mem = "";
            long c = 0;

            while ((mem = sr.ReadLine()) != null)
            {

                int i = 0;
                int i2 = 0;
                
                while (i < mem.Length - 8)
                {
                    i = mem.IndexOf("mul(", i);
                    if (i < 0)
                    {
                        break;
                    }
                    i2 = mem.IndexOf(")", i);
                    string s = mem.Substring(i + 4, i2 - i - 4);
                    //Debug.WriteLine(s);
                    if (s.Length >= 3 && s.Length <= 7)
                    {
                        if (s.Contains(","))
                        {
                            string[] n = s.Split(',');
                            if (n.Length == 2)
                            {
                                int n1 = int.Parse(n[0]);
                                int n2 = int.Parse(n[1]);
                                c += n1 * n2;
                            }

                        }
                    }
                    i = i + 4;
                }
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

            string mem = "";
            long c = 0;
            bool bdo = true;
            int nextdo = 0;

            while ((mem = sr.ReadLine()) != null)
            {

                int i = 0;
                int i2 = 0;
                //bool bdo = true;
                nextdo = GetNextDo(bdo, mem, 0);

                while (i < mem.Length - 8)
                {
                    i = mem.IndexOf("mul(", i);
                    if (i < 0)
                    {
                        if (nextdo < mem.Length)
                        {
                            bdo = !bdo;
                        }
                        break;
                    }
                    if (i > nextdo)
                    {
                        bdo = !bdo;
                        nextdo = GetNextDo(bdo, mem, nextdo + 1);
                    }

                    i2 = mem.IndexOf(")", i);
                    string s = mem.Substring(i + 4, i2 - i - 4);
                    //if (bdo)
                    //{
                    //    Debug.WriteLine("DO: " + s);
                    //}
                    
                    //else
                    //{
                    //    Debug.WriteLine("DO: " + s);
                    //}
                    if (s.Length >= 3 && s.Length <= 7)
                    {
                        if (s.Contains(","))
                        {
                            string[] n = s.Split(',');

                            if (n.Length == 2)
                            {
                                int n1 = int.Parse(n[0]);
                                int n2 = int.Parse(n[1]);
                                if (bdo)
                                {
                                    c += n1 * n2;
                                }
                                
                            }

                        }
                    }
                    i = i + 4;
                }

            }
            sw.Stop();
            sr.Close();

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int GetNextDo(bool bdo, String mem, int pos)
        {
            int nextdo = 0;

            if (pos < mem.Length)
            {
                if (bdo)
                {
                    nextdo = mem.IndexOf("don't()", pos);
                }
                else
                {
                    nextdo = mem.IndexOf("do()", pos);
                }
                if (nextdo == -1)
                {
                    nextdo = mem.Length + 1;
                }
            }
            else
            {
                nextdo = mem.Length + 1;
            }
            return nextdo;

        }

    }
}
