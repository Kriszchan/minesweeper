using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using minesweeper;
int x;
int minedb;
Random r = new Random();
Console.WriteLine("adja meg a pálya méretét: x*x maximum 50");
x = eredmeny.intcheck("x= ", 50);
cella [,] palya = new cella[x,x];
for (int i = 0; i < x;i ++)
{ 
    for (int j= 0;  j< x;j ++)
    {
        palya[i, j] = new cella();
    }
}

Console.Write("adja meg a bombák számát:");
minedb = eredmeny.intcheck("db= ", 2*x);
Console.Clear();
eredmeny.palyakiir(palya);
palya = eredmeny.bombalerak(palya, minedb);
palya= eredmeny.palyafeltolt(palya);
if (eredmeny.gameplayloop(palya, minedb) == true)
{
    Console.WriteLine("gratulálunk ön nyert");
}
else
{
    Console.WriteLine("GAME OVER");
}


