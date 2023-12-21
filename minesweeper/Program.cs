using System.Security.Cryptography.X509Certificates;
using minesweeper;
int x;
int minedb;
Console.CursorVisible = false;
bool isgameover = false;
Random r = new Random();
Console.WriteLine("adja meg a pálya méretét: x*x maximum 50");
x = eredmeny.intcheck("x= ", 50);
char [,] palya = new char[x,x];
bool[,] mines = new bool[palya.GetLength(0), palya.GetLength(1)];
bool[,] nyitott = new bool[palya.GetLength(0), palya.GetLength(1)];
int[,] palyabelso = new int[palya.GetLength(0), palya.GetLength(1)];
Console.Write("adja meg a bombák számát:");
minedb = eredmeny.intcheck("db= ", 2*x);
Console.Clear();
eredmeny.palyakiir(palya, palyabelso);
palyabelso = eredmeny.bombalerak(palyabelso, minedb);
palyabelso = eredmeny.palyafeltolt(palyabelso);
eredmeny.lepescheck(palya);


