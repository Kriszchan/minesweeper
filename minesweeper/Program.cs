using System;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using minesweeper;

namespace Arcade
{
    class MainMenu
    {
        public static void Menu()
        {
            for (; ; )
            {
                Console.Clear();
                WriteLogo();
                Say("1", "Minesweeper Indítása");
                Say("2", "Drive indítása");
                Say("3", "Kilépés");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    int x;
                    int minedb = 0;
                    cella[,] palya;
                    Console.WriteLine("adja meg a pálya méretét: x*x maximum 50");
                    Console.WriteLine("Ha a mentett játékállást szeretné betölteni írjon 1-et");
                    x = eredmeny.intcheck("x= ", 50);
                    if (x == 1)
                    {
                        palya = eredmeny.load();
                        for (int i = 0; i < palya.GetLength(0); i++)
                        {
                            for (int j = 0; j < palya.GetLength(1); j++)
                            {
                                if (palya[i, j].belso == 9)
                                {
                                    minedb++;
                                }
                            }
                        }

                        eredmeny.palyakiir(palya);
                    }
                    else
                    {
                        palya = new cella[x, x];
                        for (int i = 0; i < x; i++)
                        {
                            for (int j = 0; j < x; j++)

                            {
                                palya[i, j] = new cella();
                            }
                        }

                        Console.Write("adja meg a bombák számát:");
                        minedb = eredmeny.intcheck("db= ", 2 * x);
                        Console.Clear();
                        eredmeny.palyakiir(palya);
                        palya = eredmeny.bombalerak(palya, minedb);
                        palya = eredmeny.palyafeltolt(palya);
                    }
                    if (eredmeny.gameplayloop(palya, minedb) == true)

                    {
                        Console.WriteLine("Gratulálunk ön nyert");
                    }
                    else
                    {
                        Console.WriteLine("GAME OVER");
                    }
                }
                else if (option == "2")
                {
                    int szelesseg = 50;
                    int magassag = 30;

                    int ablakSzelesseg;
                    int ablakMagassag;
                    char[,] jelenet;
                    int pontszam = 0;
                    int autoPozicio;
                    int autoSebesseg;
                    bool jatekFut;
                    bool tovabbJatszik = true;
                    bool konzolMeretHiba = false;
                    int elozoUtFrissites = 0;

                    Console.CursorVisible = false;
                    try
                    {
                        Inicializal();
                        InditoKep();
                        while (tovabbJatszik)
                        {
                            JelenetBetölt();
                            while (jatekFut)
                            {
                                if (Console.WindowHeight < magassag || Console.WindowWidth < szelesseg)
                                {
                                    konzolMeretHiba = true;
                                    tovabbJatszik = false;
                                    break;
                                }
                                BemenetKezelese();
                                Frissites();
                                Rajzol();
                                if (jatekFut)
                                {
                                    Thread.Sleep(TimeSpan.FromMilliseconds(33));
                                }
                            }
                            if (tovabbJatszik)
                            {
                                JatekVegeKep();
                            }
                        }
                        Console.Clear();
                        if (konzolMeretHiba)
                        {
                            Console.WriteLine("A konzol/téglalap ablak túl kicsi.");
                            Console.WriteLine($"A minimális méret: {szelesseg} szélesség x {magassag} magasság.");
                            Console.WriteLine("Növeld meg a konzol ablak méretét.");
                        }
                        Console.WriteLine("Drive bezárva.");
                    }
                    finally
                    {
                        Console.CursorVisible = true;
                    }

                    void Inicializal()
                    {
                        ablakSzelesseg = Console.WindowWidth;
                        ablakMagassag = Console.WindowHeight;
                        if (OperatingSystem.IsWindows())
                        {
                            if (ablakSzelesseg < szelesseg && OperatingSystem.IsWindows())
                            {
                                ablakSzelesseg = Console.WindowWidth = szelesseg + 1;
                            }
                            if (ablakMagassag < magassag && OperatingSystem.IsWindows())
                            {
                                ablakMagassag = Console.WindowHeight = magassag + 1;
                            }
                            Console.BufferWidth = ablakSzelesseg;
                            Console.BufferHeight = ablakMagassag;
                        }
                    }

                    void InditoKep()
                    {
                        Console.Clear();
                        Console.WriteLine("Ez egy autós játék.");
                        Console.WriteLine();
                        Console.WriteLine("Maradj az úton!");
                        Console.WriteLine();
                        Console.WriteLine("Használd az A, W és D billentyűket a sebesség irányításához.");
                        Console.WriteLine();
                        Console.Write("Nyomj [enter]-t a kezdéshez...");
                        EnterNyomasraVar();
                    }

                    void JelenetBetölt()
                    {
                        const int utSzelesseg = 10;
                        jatekFut = true;
                        autoPozicio = szelesseg / 2;
                        autoSebesseg = 0;
                        int balSzel = (szelesseg - utSzelesseg) / 2;
                        int jobbSzel = balSzel + utSzelesseg + 1;
                        jelenet = new char[magassag, szelesseg];
                        for (int i = 0; i < magassag; i++)
                        {
                            for (int j = 0; j < szelesseg; j++)
                            {
                                if (j < balSzel || j > jobbSzel)
                                {
                                    jelenet[i, j] = '.';
                                }
                                else
                                {
                                    jelenet[i, j] = ' ';
                                }
                            }
                        }
                    }

                    void Rajzol()
                    {
                        StringBuilder szovegEpito = new(szelesseg * magassag);
                        for (int i = magassag - 1; i >= 0; i--)
                        {
                            for (int j = 0; j < szelesseg; j++)
                            {
                                if (i is 1 && j == autoPozicio)
                                {
                                    szovegEpito.Append(
                                        !jatekFut ? 'X' :
                                        autoSebesseg < 0 ? '<' :
                                        autoSebesseg > 0 ? '>' :
                                        '^');
                                }
                                else
                                {
                                    szovegEpito.Append(jelenet[i, j]);
                                }
                            }
                            if (i > 0)
                            {
                                szovegEpito.AppendLine();
                            }
                        }
                        Console.SetCursorPosition(0, 0);
                        Console.Write(szovegEpito);
                    }

                    void BemenetKezelese()
                    {
                        while (Console.KeyAvailable)
                        {
                            ConsoleKey billentyu = Console.ReadKey(true).Key;
                            switch (billentyu)
                            {
                                case ConsoleKey.A or ConsoleKey.LeftArrow:
                                    autoSebesseg = -1;
                                    break;
                                case ConsoleKey.D or ConsoleKey.RightArrow:
                                    autoSebesseg = +1;
                                    break;
                                case ConsoleKey.W or ConsoleKey.UpArrow or ConsoleKey.S or ConsoleKey.DownArrow:
                                    autoSebesseg = 0;
                                    break;
                                case ConsoleKey.Escape:
                                    jatekFut = false;
                                    tovabbJatszik = false;
                                    break;
                                case ConsoleKey.Enter:
                                    Console.ReadLine();
                                    break;
                            }
                        }
                    }

                    void JatekVegeKep()
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Játék Vége");
                        Console.WriteLine($"Pontszám: {pontszam}");
                        Console.WriteLine($"Újra játszani (I/N)?");
                    BillentyuBekeres:
                        ConsoleKey billentyu = Console.ReadKey(true).Key;
                        switch (billentyu)
                        {
                            case ConsoleKey.I:
                                tovabbJatszik = true;
                                break;
                            case ConsoleKey.N or ConsoleKey.Escape:
                                tovabbJatszik = false;
                                break;
                            default:
                                goto BillentyuBekeres;
                        }
                    }

                    void Frissites()
                    {
                        for (int i = 0; i < magassag - 1; i++)
                        {
                            for (int j = 0; j < szelesseg; j++)
                            {
                                jelenet[i, j] = jelenet[i + 1, j];
                            }
                        }
                        int utFrissites =
                            Random.Shared.Next(5) < 4 ? elozoUtFrissites :
                            Random.Shared.Next(3) - 1;
                        if (utFrissites is -1 && jelenet[magassag - 1, 0] is ' ') utFrissites = 1;
                        if (utFrissites is 1 && jelenet[magassag - 1, szelesseg - 1] is ' ') utFrissites = -1;
                        switch (utFrissites)
                        {
                            case -1:
                                for (int i = 0; i < szelesseg - 1; i++)
                                {
                                    jelenet[magassag - 1, i] = jelenet[magassag - 1, i + 1];
                                }
                                jelenet[magassag - 1, szelesseg - 1] = '.';
                                break;
                            case 1:
                                for (int i = szelesseg - 1; i > 0; i--)
                                {
                                    jelenet[magassag - 1, i] = jelenet[magassag - 1, i - 1];
                                }
                                jelenet[magassag - 1, 0] = '.';
                                break;
                        }
                        elozoUtFrissites = utFrissites;
                        autoPozicio += autoSebesseg;
                        if (autoPozicio < 0 || autoPozicio >= szelesseg || jelenet[1, autoPozicio] is not ' ')
                        {
                            jatekFut = false;
                        }
                        pontszam++;
                    }

                    void EnterNyomasraVar()
                    {
                    BillentyuBekeres:
                        ConsoleKey billentyu = Console.ReadKey(true).Key;
                        switch (billentyu)
                        {
                            case ConsoleKey.Enter:
                                break;
                            case ConsoleKey.Escape:
                                tovabbJatszik = false;
                                break;
                            default: goto BillentyuBekeres;
                        }
                    }
                }
                else if (option == "3")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Kérem a fentiek közül válasszon!");
                    Thread.Sleep(1500);
                }

            }
        }

        public static void Say(string prefix, string message)
        {
            Console.Write("[");
            Console.Write(prefix, Color.BlueViolet);
            Console.WriteLine("] " + message);

        }

        public static void WriteLogo()
        {
            string logo = @"     _                      _      
    / \   _ __ ___ __ _  __| | ___ 
   / _ \ | '__/ __/ _` |/ _` |/ _ \
  / ___ \| | | (_| (_| | (_| |  __/
 /_/   \_\_|  \___\__,_|\__,_|\___|
                                   ";
            Console.WriteLine(logo, Color.BlueViolet);
        }
    }
}
