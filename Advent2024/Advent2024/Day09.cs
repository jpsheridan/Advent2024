using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.CodeDom;
using System.Windows.Forms;

namespace AdventCode
{
    public class Day09 : DayClass
    {
        public override string Part1()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";

            //string[] nums = sr.ReadLine().Split(',');

            ln = sr.ReadLine();

            int[] blks = new int[ln.Length];
            long tot = 0;
            for (int i = 0; i < ln.Length; i++)
            {
                blks[i] = int.Parse(ln.Substring(i, 1));
                tot += blks[i];

            }


            int c = 0;
            int[] ds = new int[tot];
            int pos = -1;
            for (int i = 0; i < ln.Length; i++)
            {
                for (int j = 0; j < int.Parse(ln.Substring(i, 1)); j++)
                {
                    pos++;
                    if (i % 2 == 0)
                    {
                        ds[pos] = c;
                    }
                    else
                    {
                        ds[pos] = -1;
                    }
                }
                if (i % 2 == 0)
                {
                    c++;
                }
            }

            Defrag(ds);

            long chk = CheckSum(ds);


            sw.Stop();
            sr.Close();

            string ret = "Answer : " + chk.ToString();
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

            ln = sr.ReadLine();

            int[] blks = new int[ln.Length];
            long tot = 0;
            Dictionary<int, int> filesize = new Dictionary<int, int>();
            Dictionary<int, int> filestart = new Dictionary<int, int>();
            int fn = 0;
            for (int i = 0; i < ln.Length; i++)
            {
                blks[i] = int.Parse(ln.Substring(i, 1));
                tot += blks[i];
                if (i%2==0)
                {
                    filesize[fn] = blks[i];
                    
                    fn++;
                }
            }
            fn--;

            int c = 0;
            int[] ds = new int[tot];
            int pos = -1;
            for (int i = 0; i < ln.Length; i++)
            {
                if (i%2==0)
                {
                    filestart[c] = pos+1;
                }
                for (int j = 0; j < int.Parse(ln.Substring(i, 1)); j++)
                {
                    pos++;
                    if (i % 2 == 0)
                    {
                        ds[pos] = c;
                    }
                    else
                    {
                        ds[pos] = -1;
                    }
                }
                if (i%2 == 0)
                {
                    c++;
                }
            }

            Defrag2(ds, filesize, fn, filestart);

            long chk = CheckSum2(ds);


            sw.Stop();
            sr.Close();

            string ret = "Answer : " + chk.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        void Defrag(int[] ds)
        {
            int lastpt = ds.Length-1;


            for (int i = 0; i < ds.Length; i++)
            {
                if (ds[i] < 0)
                {
                    while (ds[lastpt] < 0)
                    {
                        lastpt--;
                        if (lastpt < i)
                        {
                            break;
                        }
                    }

                    if (lastpt > i)
                    {
                        ds[i] = ds[lastpt];
                        ds[lastpt] = -1;
                    }

                }
                if (lastpt <= i)
                {
                    break;
                }
            }


        }

     
        void Defrag2(int[] ds, Dictionary<int, int> fs, int fnums, Dictionary<int, int> fstart)
        {
            int lastpt = ds.Length - 1;
            int szpt = 0;

            for (int i = fnums; i > 1; i--)
            {
                int sz = fs[i];
                int spc = 0;
                for (int j = 0; j < fstart[i]; j++)
                {

                    if (ds[j] < 0)
                    {
                        szpt = j;

                        for (int k = j + 1; k < ds.Length; k++)
                        {
                            if (ds[k] > 0)
                            {

                                break;
                            }
                            szpt = k;
                        }

                         spc = szpt - j + 1;
                    }

                    if (spc >= sz)
                    {
                        for (int a = 0; a < sz; a++)
                        {
                            ds[j + a] = i;
                            ds[fstart[i]+a] = -1;
                        }
                        break;
                    }
                }
                
            }


        }

        long CheckSum(int[] ds)
        {
            long chk = 0;
            
            for (int i=0; i< ds.Length; i++)
            {
                if (ds[i]<0)
                {
                    break;
                }
                else
                {
                    chk += i * ds[i];
                }

            }
            return chk;
        }

        long CheckSum2(int[] ds)
        {
            long chk = 0;

            for (int i = 0; i < ds.Length; i++)
            {
                if (ds[i] >= 0)
                {
                    chk += i * ds[i];
                }

            }
            return chk;
        }
    }
}
