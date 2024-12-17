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
    public class Day17:DayClass
    {
        long[] reg = new long[3];
        int[] prog = new int[10];
        string outp1 = "";
        string outp2 = "";
        int[] origprog = new int[10];
        
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2024\\advent_2024_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            //string[] nums = sr.ReadLine().Split(',');

            reg[0] = long.Parse(sr.ReadLine().Split(' ')[2]);
            reg[1] = long.Parse(sr.ReadLine().Split(' ')[2]);
            reg[2] = long.Parse(sr.ReadLine().Split(' ')[2]);

            ln = sr.ReadLine();
            prog = sr.ReadLine().Split(' ')[1].Split(',').Select(x => Int32.Parse(x)).ToArray();
            sr.Close();

            RunProg();

            sw.Stop();
            

            string ret = "Answer : " + outp2;
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

            //string[] nums = sr.ReadLine().Split(',');

            reg[0] = long.Parse(sr.ReadLine().Split(' ')[2]);
            reg[1] = long.Parse(sr.ReadLine().Split(' ')[2]);
            reg[2] = long.Parse(sr.ReadLine().Split(' ')[2]);

            
            ln = sr.ReadLine();
            string p = sr.ReadLine().Split(' ')[1];
            prog = p.Split(',').Select(x => Int32.Parse(x)).ToArray();
            origprog = p.Split(',').Select(x => Int32.Parse(x)).ToArray();
            sr.Close();
            p += ",";

            long i = 0;

            while (true)
            {
                outp2 = "";
                reg[0] = i;
                reg[1] = 0;
                reg[2] = 0;
                RunProg();
                
                
                if (outp2 == p)
                {
                    break;
                }

                if (outp2 == p.Substring(p.Length-outp2.Length))
                {
                    i *= 8;
                }
                else
                {
                    i += 1;
                }
                
            }
            

            sw.Stop();


            string ret = "Answer : " + i.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        void RunProg()
        {
            int pt = 0;
            int outpt = 0;

            while (pt < prog.Length)
            {
                int opcode = prog[pt];
                int operand = prog[pt + 1];
                
                switch(opcode)
                {
                    case 0: // adv = division
                        long pow = GetCombo(operand);
                        long d = (long)Math.Pow(2, pow);
                        reg[0] = reg[0] / d;
                        pt += 2;
                        break;
                    
                    case 1: //bxl = bitwise xor
                        reg[1] = reg[1] ^ operand;
                        pt += 2;
                        break;

                    case 2:  // bst - combo mod 8
                        long bst = GetCombo(operand);
                        reg[1] = bst % 8;
                        pt += 2;
                        break;

                    case 3: // jnz - nothing if reg a = 0 else jump
                        if (reg[0] == 0)
                        {
                            pt += 2;
                        }
                        else
                        {
                            pt = operand;
                        }
                        break;

                    case 4: // bxc - bitwise xor of reg b and c
                        reg[1] = reg[1] ^ reg[2];
                        pt += 2;
                        break;

                    case 5: // out 
                        long outp = GetCombo(operand);
                        outp = outp % 8;
                        //Debug.WriteLine(outp);
                        //outp1 += outp;
                        outp2 += outp + ",";
                        
                        pt += 2;
                        break;
                    case 6: // bdiv
                        long pow2 = GetCombo(operand);
                        long d2 = (long)Math.Pow(2, pow2);
                        reg[1] = reg[0] / d2;
                        pt += 2;
                        
                        break;
                    case 7:  // cdiv
                        long pow3 = GetCombo(operand);
                        long d3 = (long)Math.Pow(2, pow3);
                        reg[2] = reg[0] / d3;
                        pt += 2;

                        break;

                }
            }
            int abc = 0;
        }

        void RunProg2()
        {
            int pt = 0;
            int outpt = 0;

            while (pt < prog.Length)
            {
                int opcode = prog[pt];
                int operand = prog[pt + 1];

                switch (opcode)
                {
                    case 0: // adv = division
                        long pow = GetCombo(operand);
                        long d = (long)Math.Pow(2, pow);
                        reg[0] = reg[0] / d;
                        pt += 2;
                        break;

                    case 1: //bxl = bitwise xor
                        reg[1] = reg[1] ^ operand;
                        pt += 2;
                        break;

                    case 2:  // bst - combo mod 8
                        long bst = GetCombo(operand);
                        reg[1] = bst % 8;
                        pt += 2;
                        break;

                    case 3: // jnz - nothing if reg a = 0 else jump
                        if (reg[0] == 0)
                        {
                            pt += 2;
                        }
                        else
                        {
                            pt = operand;
                        }
                        break;

                    case 4: // bxc - bitwise xor of reg b and c
                        reg[1] = reg[1] ^ reg[2];
                        pt += 2;
                        break;

                    case 5: // out 
                        long outp = GetCombo(operand);
                        outp = outp % 8;
                        //Debug.WriteLine(outp);
                        //outp1 += outp;
                        outp2 += outp + ",";
                        if (outp != origprog[outpt])
                        {
                            return;
                        }
                        outpt++;
                        pt += 2;
                        break;
                    case 6: // bdiv
                        long pow2 = GetCombo(operand);
                        long d2 = (long)Math.Pow(2, pow2);
                        reg[1] = reg[0] / d2;
                        pt += 2;

                        break;
                    case 7:  // cdiv
                        long pow3 = GetCombo(operand);
                        long d3 = (long)Math.Pow(2, pow3);
                        reg[2] = reg[0] / d3;
                        pt += 2;

                        break;

                }
            }
            int abc = 0;
        }


        long GetCombo(long operand)
        {
            if (operand < 4)
            {
                return operand;
            }
            if (operand >=4 && operand < 7)
            {
                return reg[operand - 4];
            }
            if (operand == 7)
            {
                // placeholder
            }
            return 0;       

        }

    }
}
