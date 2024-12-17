using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace AdventCode
{
    public class Day11:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = sr.ReadLine();
            sr.Close();

            long[] stones1 = new long[100000000];
            long[] stones2 = new long[100000000];

            string[] st = ln.Split(' ');

            int pos1 = st.Length;
            int pos2 = st.Length;
            for (int i=0; i < pos1; i++)
            {
                stones1[i] = int.Parse(st[i]);
            }

            
            for (int i = 0; i < 25; i++)
            {
                if (i % 2 == 0)
                {
                    pos2 = MoveStones(stones1, stones2, pos1);
                }
                else
                {
                    pos1 = MoveStones(stones2, stones1, pos2);
                }
                Debug.WriteLine(i.ToString());
            }




            sw.Stop();
        
            string ret = "Answer : " + pos1.ToString() + " - " + pos2.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = sr.ReadLine();
            sr.Close();

            
            string[] st = ln.Split(' ');

            
            Dictionary<long, long> nums = new Dictionary<long, long>();
            for (int i = 0; i < st.Length; i++)
            {
                nums[long.Parse(st[i])] = 1;
            }


            for (int i = 0; i < 75; i++)
            {
                nums = MoveStones3(nums);
            }

            long c = 0;

            foreach (KeyValuePair<long, long> kvp in nums)
            {
                c += kvp.Value;
            }


            sw.Stop();

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int MoveStones(long[] st1, long[] st2, int maxpos)
        {
            int newpos = 0;

            for (int i = 0; i < maxpos; i++)
            {
                if (st1[i] < 2)
                {
                    st2[newpos] = 1;
                    newpos++;
                }
                else if (st1[i].ToString().Length%2==0)
                {
                    string s1 = st1[i].ToString();
                    st2[newpos] = long.Parse(s1.Substring(0, s1.Length / 2));
                    newpos++;
                    st2[newpos] = long.Parse(s1.Substring(s1.Length / 2));
                    newpos++;
                }
                else
                {
                    st2[newpos] = st1[i] * 2024;
                    newpos++;
                }
            }
            return newpos;
        }

        //int MoveStones2(long[] st1, long[] st2, int maxpos)
        //{
        //    int newpos = 0;

        //    for (int i = 0; i < maxpos; i++)
        //    {
        //        if (st1[i] == 0)
        //        {
        //            //st2[i] = 1;
        //            //newpos++;
        //        }
        //        else if (st1[i].ToString().Length % 2 == 0)
        //        {
        //            string s1 = st1[i].ToString();
        //            st2[newpos] = long.Parse(s1.Substring(0, s1.Length / 2));
        //            newpos++;
        //            st2[newpos] = long.Parse(s1.Substring(s1.Length / 2));
        //            newpos++;
        //        }
        //        else
        //        {
        //            st2[newpos] = st1[i] * 2024;
        //            newpos++;
        //        }
        //    }
        //    return newpos;
        //}
        Dictionary<long, long> MoveStones3(Dictionary<long, long> nums)
        {
            Dictionary<long, long>  newnums = new Dictionary<long, long>();

            foreach(KeyValuePair<long, long> kvp in nums)
            {
                if (kvp.Key == 0)
                {
                    if (newnums.ContainsKey(1))
                    {
                        newnums[1] += kvp.Value;
                    }
                    else
                    {
                        newnums[1] = kvp.Value;
                    }


                }
                //else if (kvp.Key.ToString().Length % 2 == 0)
                else
                {
                    int digits = (int)(Math.Log10(kvp.Key) + 1);

                    if (digits % 2 == 0)
                    {

                        long p = (long)(kvp.Key / Math.Pow(10, digits / 2));


                        if (newnums.ContainsKey(p))
                        {
                            newnums[p] += kvp.Value;
                        }
                        else
                        {
                            newnums[p] = kvp.Value;
                        }

                        p = (long)(kvp.Key % Math.Pow(10, digits / 2));
                        if (newnums.ContainsKey(p))
                        {
                            newnums[p] += kvp.Value;
                        }
                        else
                        {
                            newnums[p] = kvp.Value;
                        }
                    }
                    else
                    {
                        //long n = kvp.Key * 2024;
                        //if (newnums.ContainsKey(n))
                        //{
                        //newnums[kvp.Key * 2024] += kvp.Value;
                        //}   
                        //else
                        //{
                        newnums[kvp.Key * 2024] = kvp.Value;
                        //}

                    }
                }
            }

            return newnums;
        }
    }
}
