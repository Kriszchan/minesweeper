﻿using System.Security.Cryptography.X509Certificates;
using minesweeper;
int x;
int minedb = 9;
bool a;
Console.CursorVisible = false;
bool isgameover = false;
Random r = new Random();
Console.WriteLine("adja meg a pálya méretét: x*x maximum 50");
x = eredmeny.intcheck("x= ", 50);
char [,] palya = new char[x,x];
bool[,] mines = new bool[palya.GetLength(0), palya.GetLength(1)];
Console.Write("adja meg a bombák számát:");
minedb = eredmeny.intcheck("db= ", x);
Console.Clear();
eredmeny.palyagen(palya, mines);
Console.Write("adja meg az első lépés koordinátáját pl: 9,6");
string [] firststep = Console.ReadLine().Split();
