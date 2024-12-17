using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day07 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            long valid = 0;

            //string[] nums = sr.ReadLine().Split(',');

            while ((ln = sr.ReadLine()) != null)
            {
                string[] s = ln.Split(':');
                long t = long.Parse(s[0]);

                string[] s2 = s[1].Trim().Split(' ');

                List<long> nums = s2.Select(x => long.Parse(x)).ToList();

                if (ValidList(nums, t, 1, nums[0]))
                {
                    valid += t;
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
            long valid = 0;

            //string[] nums = sr.ReadLine().Split(',');

            while ((ln = sr.ReadLine()) != null)
            {
                string[] s = ln.Split(':');
                long t = long.Parse(s[0]);

                string[] s2 = s[1].Trim().Split(' ');

                List<long> nums = s2.Select(x => long.Parse(x)).ToList();
                if (ValidList2(nums, t, 1, nums[0], 0, nums[0]))
                {
                    valid += t;
                }
            }

            sw.Stop();
            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        bool ValidList(List<long> nums, long sum, int pos, long cursum)
        {
            if (pos == nums.Count)
            {
                if (sum == cursum)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (cursum > sum)
            {
                return false;
            }

            long thissum = 0;

            
            thissum = cursum - nums[pos-1];
            
            thissum = cursum + nums[pos];
            if (ValidList(nums, sum, pos + 1, thissum))
            {
                return true;
            }
            
            thissum = cursum * nums[pos];
            if (ValidList(nums, sum, pos + 1, thissum))
            {
                return true;
            }

            return false;
        }

        bool ValidList2(List<long> nums, long sum, int pos, long cursum, int op, long lastnum)
        {
            if (pos == nums.Count)
            {
                if (sum == cursum)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (cursum > sum)
            {
                return false;
            }

            long thissum = 0;


            //if (op == 0)
            //{
            //    thissum = cursum - lastnum;
            //}
            //else if (op == 1 && lastnum != cursum)
            //{
            //    thissum = cursum / lastnum;
            //}
            //else
            //{
            //    thissum = lastnum;
            //}
            thissum += long.Parse(cursum.ToString() + nums[pos].ToString());
            if (ValidList2(nums, sum, pos + 1, thissum, 2, thissum))
            {
                return true;
            }

            thissum = cursum + nums[pos];
            if (ValidList2(nums, sum, pos + 1, thissum, 0, nums[pos]))
            {
                return true;
            }

            thissum = cursum * nums[pos];
            if (ValidList2(nums, sum, pos + 1, thissum, 1, nums[pos]))
            {
                return true;
            }

            return false;
        }
    }
}
