using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using minesweeper;
int x;
int minedb;
int[] step = new int[2];
Console.CursorVisible = false;
bool isgamelost = false;
bool isgamewon = false;
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
eredmeny.palyakiir(palya, nyitott);
palyabelso = eredmeny.bombalerak(palyabelso, minedb);
palyabelso = eredmeny.palyafeltolt(palyabelso);
if (eredmeny.gameplayloop(palyabelso, palya, nyitott, minedb) == true)
{
    Console.WriteLine("gratulálunk ön nyert");
}
else
{
    Console.WriteLine("GAME OVER");
}


