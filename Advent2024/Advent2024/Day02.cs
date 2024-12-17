using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace AdventCode
{
    public class Day02:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            

            while ((ln = sr.ReadLine()) != null)
            {
                string[] nums = ln.Split(' ');

                
                bool ok = ProcessList(nums);
             
                if (ok)
                {
                    valid++;
                    Debug.WriteLine(ln);
                }
                    
            }

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



            while ((ln = sr.ReadLine()) != null)
            {
                string[] nums = ln.Split(' ');


                bool ok = ProcessList(nums);

                if (ok)
                {
                    valid++;
                    Debug.WriteLine(ln);
                }
                else
                {
                    
                    for (int i = 0; i < nums.Length; i++)
                    {
                        List<string> n2 = new List<string>();
                        n2 = nums.ToList();
                        n2.RemoveAt(i);

                        string[] nums2 = n2.ToArray();

                        ok = ProcessList(nums2);
                        if (ok)
                        {
                            valid++;
                            break;
                        }

                    }
                }

            }

            sw.Stop();
            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        bool  ProcessList(string[] nums)
        {
            bool ok = true;
            bool up = false;
            int a = 0;
            int b = 0;
               
            a = int.Parse(nums[0]);
            b = int.Parse(nums[1]);

            if (a<b)
            {
                up = true;
            }

            for (int i = 1; i < nums.Length; i++)
            {
                b = int.Parse(nums[i]);
                if (a < b && !up)
                {
                    ok = false;
                    break;
                }
                if (a > b && up)
                {
                    ok = false;
                    break;

                }
                int c = Math.Abs(a - b);
                if (c < 1 || c > 3)
                {
                    ok = false;
                    break;
                }
                a = b;
            }
            return ok;
        }

    }
}
