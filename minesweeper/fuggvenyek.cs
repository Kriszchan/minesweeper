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
        public static cella[,] bombalerak(cella[,] palya, int minesdb)
        {
            Random r = new Random();
            int x = minesdb;
            while (x > 0)
            {
                palya[r.Next(palya.GetLength(0)), r.Next(palya.GetLength(1))].belso = 9;
                x -= 1;
            }
            return palya;
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
        public static int[] lepescheck(cella[,] palya)
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
                    a = int.TryParse(c[0], out sy);
                    b = int.TryParse(c[1], out sx);
                    sx--;
                    sy--;
                }
                catch (Exception)
                {
                    continue;
                    throw;
                }

            }
            while (((sx < 0 || sx > palya.GetLength(0)-1 || sy < 0 || sy > palya.GetLength(1)-1) || (a == false || b == false))&& palya[sx, sy].nyitott == false);
            d[0] = sx;
            d[1] = sy;
            return d;
        }
        public static cella[,] palyafeltolt(cella[,] palya)
        {
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (palya[i, j].belso != 9)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int x = i + dx;
                                int y = j + dy;

                                if ((dx != 0 || dy != 0) && x >= 0 && x < palya.GetLength(0) && y >= 0 && y < palya.GetLength(1) && palya[x, y].belso == 9)
                                {
                                    palya[i, j].belso++;
                                }
                            }
                        }
                    }
                }
            }
            return palya;
        }

        //gameplay

        public static void palyakiir(cella[,] palya)
        {
            Console.Clear();
            Console.Write("    ");
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                if (i + 1 < 9)
                {
                    Console.Write(i + 1 + "  ");
                }
                else
                {
                    Console.Write(i + 1 + " ");
                }
            }
            Console.Write("\n");
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (palya[i, j].nyitott)
                    {
                        if (j == 0)
                        {
                            if (i < 9)
                            {
                                Console.Write($"{i + 1} | {(int)palya[i, j].kiir}  ");
                            }
                            else
                            {
                                Console.Write($"{i + 1}| {(int)palya[i, j].kiir}  ");
                            }
                        }
                        else
                        {
                            Console.Write((int)palya[i, j].kiir + "  ");
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            if (i < 9)
                            {
                                Console.Write($"{i + 1} | -  ");
                            }
                            else
                            {
                                Console.Write($"{i + 1}| -  ");
                            }
                        }
                        else
                        {
                            Console.Write("-  ");
                        }
                    }
                
                }
                Console.WriteLine();
            }
        }
        public static bool gameplayloop(cella[,] palya,int minedb)
        {
            int nemfelfedettdb = 0;
            int[] s = new int[2];
            while (true)
            {
                s = lepescheck(palya);
                nemfelfedettdb = 0;
                if (cellafelnyit(s[0], s[1], palya)) return false;
                palyakiir(palya);

                for (int i = 0; i < palya.GetLength(0); i++) 
                {
                    for (int j = 0; j < palya.GetLength(1); j++)
                    {

                        if (!palya[i,j].nyitott)

                        {
                            nemfelfedettdb++;                            
                        }
                    }
                }

                if (nemfelfedettdb == minedb)
                { 
                    return true;
                }
            }
        }
        public static bool cellafelnyit(int sx, int sy, cella[,] palya)

        {
            if (palya[sx, sy].belso != 9 && palya[sx, sy].belso != 0)
            {
                palya[sx, sy].kiir = (char)palya[sx, sy].belso;
                palya[sx, sy].nyitott = true;
                return false;
            }
            else if (palya[sx, sy].belso != 9)
            {

                palya[sx, sy].nyitott = true;
                if ((sx + 1 < palya.GetLength(0)) && !palya[sx + 1, sy].nyitott) cellafelnyit(sx + 1, sy, palya);
                if ((sx + 1 < palya.GetLength(0) && sy + 1 < palya.GetLength(0)) && !palya[sx + 1, sy + 1].nyitott) cellafelnyit(sx + 1, sy + 1,palya);
                if ((sx + 1 < palya.GetLength(0) && sy - 1 >= 0) && !palya[sx + 1, sy - 1].nyitott) cellafelnyit(sx + 1, sy - 1, palya);
                if ((sy + 1 < palya.GetLength(0)) && !palya[sx, sy + 1].nyitott) cellafelnyit(sx, sy + 1, palya);
                if ((sx - 1 >= 0 && sy + 1 < palya.GetLength(0)) && !palya[sx - 1, sy + 1].nyitott) cellafelnyit(sx - 1, sy + 1, palya);
                if ((sx - 1 >= 0 && sy - 1 >= 0) && palya[sx - 1, sy].nyitott) cellafelnyit(sx - 1, sy - 1, palya);
                if ((sx - 1 >= 0) && !palya[sx - 1, sy].nyitott) cellafelnyit(sx - 1, sy, palya);

                return false;
            }
            else
            {
                return true;
            } 
        }
    } 
}

