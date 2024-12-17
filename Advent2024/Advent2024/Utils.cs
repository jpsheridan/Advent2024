using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
//using System.Threading.Tasks;

namespace AdventCode
{
    static class Utils
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            if (!list.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var startingElementIndex = 0;

                foreach (var startingElement in list)
                {
                    var index = startingElementIndex;
                    var remainingItems = list.Where((e, i) => i != index);

                    foreach (var permutationOfRemainder in remainingItems.Permute())
                    {
                        yield return startingElement.Concat(permutationOfRemainder);
                    }

                    startingElementIndex++;
                }
            }
        }

        private static IEnumerable<T> Concat<T>(this T firstElement, IEnumerable<T> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }

        public static string[,] GetGridFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            
            string ln = "";
            int r = 0;
            int c = 0;

            ln = sr.ReadLine();
            r++;
            c = ln.Length;
            while ((ln = sr.ReadLine()) != null)
            {
                r++;
            }

            string[,] grid = new string[c+2, r+2];

            r = 0;
            c = 0;
            
            sr.BaseStream.Position = 0;

            while ((ln = sr.ReadLine()) != null)
            {
                r++;
                for (c = 0; c < ln.Length; c++)
                {
                    grid[c + 1, r] = ln.Substring(c, 1);
                }

            }

            return grid;
        }

        public static string[,] GetGridFromFile0(string filename)
        {
            // start from 0,0

            StreamReader sr = new StreamReader(filename);


            string ln = "";
            int r = 0;
            int c = 0;

            ln = sr.ReadLine();
            r++;
            c = ln.Length;
            while ((ln = sr.ReadLine()) != null)
            {
                r++;
            }

            string[,] grid = new string[c, r];

            r = 0;
            c = 0;

            sr.BaseStream.Position = 0;

            while ((ln = sr.ReadLine()) != null)
            {

                for (c = 0; c < ln.Length; c++)
                {
                    grid[c, r] = ln.Substring(c, 1);
                }
                r++;
            }

            sr.Close();
            return grid;
        }



        public static string[,,] Get3dGridFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);


            string ln = "";
            int r = 0;
            int c = 0;
            int z = 50;

            ln = sr.ReadLine();
            r++;
            c = ln.Length;

            while ((ln = sr.ReadLine()) != null)
            {
                r++;
            }

            string[,,] grid = new string[100, c + 2, r + 2];

            for (int w = 0; w < 100; w++)
            {
                for (int x = 0; x < c+1; x++)
                {
                    for (int y = 0; y < r + 1; y++)
                    {
                        grid[w, x, y] = ".";
                    }
                }
            }

            
            r = 0;
            c = 0;

            sr.BaseStream.Position = 0;

            while ((ln = sr.ReadLine()) != null)
            {
                r++;
                for (c = 0; c < ln.Length; c++)
                {
                    grid[z,c + 1, r] = ln.Substring(c, 1);
                }

            }

            return grid;
        }

        public static void DrawGrid(string[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string ln = "";
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == null)
                    {
                        ln += " ";
                    }
                    else
                    {
                        ln += grid[x, y];
                    }


                }
                Debug.WriteLine(ln);
            }

            Debug.WriteLine("");


        }


        public static void WriteGrid(string[,] grid, int turn)
        {
            StreamWriter sw = new StreamWriter("f:\\temp\\tree.txt", true);
            sw.WriteLine("turn = " + turn.ToString());

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string ln = "";
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == null)
                    {
                        ln += " ";
                    }
                    else
                    {
                        ln += grid[x, y];
                    }


                }
                sw.WriteLine(ln);
            }

            sw.WriteLine("");
            sw.WriteLine("");

            sw.Close();

        }

        public static void DrawBitmapAsppm(string[,] grid)
        {
            StreamWriter sw = new StreamWriter("f:\\temp\\tree.ppm", true);
            sw.WriteLine("P1");
            sw.WriteLine(grid.GetLength(0).ToString() + " " + grid.GetLength(1).ToString());

            

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string ln = "";
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == null)
                    {
                        ln += "0 ";
                    }
                    else
                    {
                        ln += "1 ";
                    }


                }
                sw.WriteLine(ln.Trim());
            }

            sw.Close();

        }

        public static void DrawBitmap(string[,] grid)
        {
            //StreamWriter sw = new StreamWriter("f:\\temp\\tree\\tree.bmp", true);
            //sw.WriteLine("P1");
            //sw.WriteLine(grid.GetLength(0).ToString() + " " + grid.GetLength(1).ToString());


            Bitmap bmp = new Bitmap(grid.GetLength(0), grid.GetLength(1));


            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string ln = "";
                for (int x = 0; x < grid.GetLength(0); x++)
                {

                    if (grid[x, y] == null)
                    {
                        Color newColor = Color.FromArgb(0, 0, 0);
                        bmp.SetPixel(x, y, newColor) ;
                    }
                    else
                    {
                        Color newColor = Color.FromArgb(255, 255, 255);
                        bmp.SetPixel(x, y, newColor);
                    }


                }
                //sw.WriteLine(ln.Trim());
            }

            bmp.Save("f:\\temp\\tree\\tree.bmp");

        }
    }
}
