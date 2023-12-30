using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using minesweeper;
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
    palya = new cella[x,x];
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
}
if (eredmeny.gameplayloop(palya, minedb) == true)

{
    Console.WriteLine("Gratulálunk ön nyert");
}
else
{
    Console.WriteLine("GAME OVER");
}