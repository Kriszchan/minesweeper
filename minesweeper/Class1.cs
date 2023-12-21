using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class eredmeny
    {
         //alap
        public static int[,] bombalerak(int[,] palyabelso, int minesdb)
        {
            Random r = new Random();
            int x = minesdb;
            while (x > 0)
            {
                palyabelso[r.Next(palyabelso.GetLength(0)), r.Next(palyabelso.GetLength(1))] = 9;
                x -= 1;
            }
            return palyabelso;
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
        public static int[] lepescheck(int[] step, char[,] palya)
        {
            bool a = false;
            bool b = false;
            do
            {
                Console.Write("adja meg az első lépés koordinátáját pl: 9,6");
                a = int.TryParse(Console.ReadLine().Split(",")[1], out step[0]);
                b = int.TryParse(Console.ReadLine().Split(",")[1], out step[1]);
            }
            while ((step[0] < 1 || step[0] > palya.GetLength(0) || step[1] < 1 || step[1] > palya.GetLength(1)) || (a == false || b == false));
            return step;
        }
        public static int[,] palyafeltolt(int[,] palyabelso)
        {
            for (int i = 0; i < palyabelso.GetLength(0); i++)
            {
                for (int j = 0; j < palyabelso.GetLength(1); j++)
                {
                    if (palyabelso[i, j] == 9)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int x = i + dx;
                                int y = j + dy;

                                if (!(dx == 0 && dy == 0) && x >= 0 && x < palyabelso.GetLength(0) && y >= 0 && y < palyabelso.GetLength(1) && palyabelso[x, y] == 9)
                                {
                                    palyabelso[x, y]++;
                                }
                            }
                        }
                    }
                }
            }
            return palyabelso;
        }

        //gameplay

        public static void palyakiir(char[,] palya, bool[,] mines)
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
                        if (palya[i,j] == '\0')
                        {
                            Console.Write(" - |");
                        }
                        else
                        {
                            Console.Write($" {palya[i,j]} |");
                        }
                    }
                }
                Console.Write("\n");
            }
        }
        public static int[,] lepes(int[,] palyabelso, char[,] palya, int[]step)
        {
            if (palyabelso[step[0], step[1]] != 9) palya[step[0], step[1]] = (char)palyabelso[step[0], step[1]];

            for (int i = 0; i < palyabelso.GetLength(0); i++)
            {
                for (int j = 0; j < palyabelso.GetLength(1); j++)
                {
                    if (i == step[0]&& j == step[1])
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int x = i + dx;
                                int y = j + dy;

                                if (!(dx == 0 && dy == 0) && x >= 0 && x < palyabelso.GetLength(0) && y >= 0 && y < palyabelso.GetLength(1) && palyabelso[x, y] == 0)
                                {
                                    
                                }
                            }
                        }
                    }
                }
            }
        }


}
}
