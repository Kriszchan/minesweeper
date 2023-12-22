using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
        public static int[] lepescheck(bool[,] nyitott)
        {
            bool a = false;
            int sx = 0;
            int sy = 0;
            int [] d = new int [2];
            bool b = false;
            do
            {
                Console.Write("adja meg a lépés koordinátáját pl: x-y: ");
                string [] c = Console.ReadLine().Split("-");
                try
                {
                    a = int.TryParse(c[0], out sx);
                    b = int.TryParse(c[1], out sy);
                }
                catch (Exception)
                {
                    continue;
                    throw;
                }

            }
            while (((sx < 1 || sx > nyitott.GetLength(0)-1 || sy < 1 || sy > nyitott.GetLength(1)-1) || (a == false || b == false))&& nyitott[sx, sy] == false);
            d[0] = sx;
            d[1] = sy;
            return d;
        }
        public static int[,] palyafeltolt(int[,] palyabelso)
        {
            for (int i = 0; i < palyabelso.GetLength(0); i++)
            {
                for (int j = 0; j < palyabelso.GetLength(1); j++)
                {
                    if (palyabelso[i, j] != 9)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int x = i + dx;
                                int y = j + dy;

                                if ((dx != 0 || dy != 0) && x >= 0 && x < palyabelso.GetLength(0) && y >= 0 && y < palyabelso.GetLength(1) && palyabelso[x, y] == 9)
                                {
                                    palyabelso[i, j]++;
                                }
                            }
                        }
                    }
                }
            }
            return palyabelso;
        }

        //gameplay

        public static void palyakiir(char[,] palya, int[,]belsopalya)
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
                        if (palya[i-2,j] == '\0')
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
        public static bool gameplayloop(int[,] palyabelso, char[,] palya, bool[,] nyitott, int minedb)
        {
            int nemfelfedettdb = 0;
            bool vesztett = false;
            bool nyert = false;
            int[] s = new int[2];
            while (nemfelfedettdb != minedb|| vesztett!= true|| nyert != true)
            {
                s = lepescheck(nyitott);
                cellafelnyit(s[0],s[1], palyabelso, nyitott, palya, vesztett);
                palyakiir(palya, palyabelso);
                for (int i = 0; i < palya.GetLength(0); i++) 
                {
                    for (int j = 0; j < palya.GetLength(1); j++)
                    {
                        if (palya[i, j] == '\0')
                        {
                            nemfelfedettdb++;                            
                        }
                    }
                }
                if (nemfelfedettdb == minedb)
                {
                    nyert = true;
                }
            }
            if (vesztett == true) return false;
            else return true;
        }
        public static void cellafelnyit(int sx, int sy, int[,] palyabelso, bool[,] nyitott, char[,] palya, bool vesztett)
        {
            if (palyabelso[sx, sy] != 9 && palyabelso[sx, sy] != 0)
            {
                palya[sx, sy] = (char)palyabelso[sx, sy];
                nyitott[sx, sy] = true;
            }
            else if (palyabelso[sx, sy] != 9)
            {
                nyitott[sx, sy] = true;
                if ((sx + 1 < palyabelso.GetLength(0)) && !nyitott[sx + 1, sy]) cellafelnyit(sx + 1, sy, palyabelso, nyitott, palya, vesztett);
                if ((sx + 1 < palyabelso.GetLength(0) && sy + 1 < palyabelso.GetLength(0)) && !nyitott[sx + 1, sy + 1]) cellafelnyit(sx + 1, sy + 1, palyabelso, nyitott, palya, vesztett);
                if ((sx + 1 < palyabelso.GetLength(0) && sy - 1 >= 0) && !nyitott[sx + 1, sy - 1]) cellafelnyit(sx + 1, sy - 1, palyabelso, nyitott, palya, vesztett);
                if ((sy + 1 < palyabelso.GetLength(0)) && !nyitott[sx, sy + 1]) cellafelnyit(sx, sy + 1, palyabelso, nyitott, palya, vesztett);
                if ((sy - 1 >= 0) && !nyitott[sx, sy - 1]) cellafelnyit(sx, sy - 1, palyabelso, nyitott, palya, vesztett);
                if ((sx - 1 >= 0 && sy + 1 < palyabelso.GetLength(0)) && !nyitott[sx - 1, sy + 1]) cellafelnyit(sx - 1, sy + 1, palyabelso, nyitott, palya, vesztett);
                if ((sx - 1 >= 0 && sy - 1 >= 0) && !nyitott[sx - 1, sy - 1]) cellafelnyit(sx - 1, sy - 1, palyabelso, nyitott, palya, vesztett);
                if ((sx - 1 >= 0) && !nyitott[sx - 1, sy]) cellafelnyit(sx - 1, sy, palyabelso, nyitott, palya, vesztett);
            }
            else vesztett = true;
        }
    } 
}

