using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class eredmeny
    {
        public static void palyagen(char [,] palya, bool[,] mines)
        {
            for (int i = 0; i < palya.GetLength(0) + 3; i++)
            {
                if ((i > 1) && i != palya.GetLength(0) + 2)
                {
                    if (i < 9 + 2) Console.Write($"{i - 1} ");
                    else Console.Write(i - 1);
                }
                else if (i == 1) Console.Write("  ");
                if (i != 0 && i != palya.GetLength(0) + 2)
                {
                    Console.Write("||");
                }
                else
                {
                    Console.Write("   ");
                }
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        Console.Write("====");
                    }
                    else if (i == palya.GetLength(0) + 2)
                    {
                        Console.Write("====");
                    }
                    else if (i == 1)
                    {
                        if (j < 9) Console.Write($" {j + 1} |");
                        else Console.Write($" {j + 1}|");
                    }
                    else if (i >= 2)
                    {
                        if (mines[i - 2, j] == false)
                        {
                            Console.Write(" X |");
                        }
                        else
                        {
                            Console.Write(" S |");
                        }
                    }
                }
                Console.Write("\n");
            }
        }
         public static int intcheck(string szveg, int max)
        {
            bool a;
            int x;
            do
            {
                Console.Write(szveg);
                a = int.TryParse(Console.ReadLine(), out x);
            }
            while (a == false || x > max);
            return x;
        }
    }
}
