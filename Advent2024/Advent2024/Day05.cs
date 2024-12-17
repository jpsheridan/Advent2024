using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Threading;

namespace AdventCode
{
    public class Day05 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            

            //string[] nums = sr.ReadLine().Split(',');
            bool blist = false;
            Dictionary<int, List<int>> pagerules = new Dictionary<int, List<int>>();
            List<int> pages = new List<int>();
            int c = 0;
            while ((ln = sr.ReadLine()) != null)
            {

                if (ln == "")
                {
                    blist = true;
                }
                else if (!blist)
                {
                    string[] n = ln.Split('|');
                    int n1 = int.Parse(n[0]);
                    int n2 = int.Parse(n[1]);
                    if (!pagerules.ContainsKey(n1))
                    {
                        pagerules[n1] = new List<int>();
                    }

                    pagerules[n1].Add(n2);
                }
                else
                {
                    string[] pg = ln.Split(',');

                    pages = pg.Select(x => Int32.Parse(x)).ToList();
                    bool valid = true; 
                    for (int i = 0; i < pages.Count-1; i++)
                    {
                        for (int j=i+1; j < pages.Count; j++)
                        {
                            if (!pagerules.ContainsKey(pages[i]))
                            {
                                valid = false;
                            }
                            else if (!pagerules[pages[i]].Contains(pages[j]))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (valid)
                    {
                        c += pages[pages.Count / 2];
                    }

                       
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


            //string[] nums = sr.ReadLine().Split(',');
            bool blist = false;
            Dictionary<int, List<int>> pagerules = new Dictionary<int, List<int>>();
            List<int> pages = new List<int>();
            int c = 0;
            List<string[]> inc = new List<string[]>();

            while ((ln = sr.ReadLine()) != null)
            {

                if (ln == "")
                {
                    blist = true;
                }
                else if (!blist)
                {
                    string[] n = ln.Split('|');
                    int n1 = int.Parse(n[0]);
                    int n2 = int.Parse(n[1]);
                    if (!pagerules.ContainsKey(n1))
                    {
                        pagerules[n1] = new List<int>();
                    }

                    pagerules[n1].Add(n2);
                }
                else
                {
                    string[] pg = ln.Split(',');

                    pages = pg.Select(x => Int32.Parse(x)).ToList();
                    bool valid = true;
                    for (int i = 0; i < pages.Count - 1; i++)
                    {
                        for (int j = i + 1; j < pages.Count; j++)
                        {
                            if (!pagerules.ContainsKey(pages[i]))
                            {
                                valid = false;
                                inc.Add(pg);
                                break;
                            }
                            else if (!pagerules[pages[i]].Contains(pages[j]))
                            {
                                valid = false;
                                inc.Add(pg);
                                break;
                            }
             
                        }
                        if (!valid)
                        {
                            break;
                        }
                    }

                }

            }


            foreach (string[] pg in inc)
            {
                pages = pg.Select(x => Int32.Parse(x)).ToList();
                bool valid = false;


                for (int i = 0; i < pages.Count - 1; i++)
                {
                    valid = false;
                    while (!valid)
                    {
                        valid = FixOrder(pages, pagerules, i);
                    }
                }

                if (valid)
                {
                    c += pages[pages.Count / 2];
                }


            }



            sw.Stop();
            sr.Close();

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        bool FixOrder(List<int> pages, Dictionary<int, List<int>> pagerules, int i)
        {
            bool valid = true;
            for (int j = i + 1; j < pages.Count; j++)
            {
                if (!pagerules.ContainsKey(pages[i]))
                {
                    int tmp = pages[i];
                    pages.RemoveAt(i);
                    pages.Add(tmp);
                }
                else if (!pagerules[pages[i]].Contains(pages[j]))
                {
                    valid = false;

                    int tmp = pages[i];
                    pages[i] = pages[j];
                    pages[j] = tmp;

                    break;
                }

            }

            return valid;
        }
    }
}
