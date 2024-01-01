using Arcade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    class Login
    {
        static void Main(string[] args)
        {
            Console.Title = "Login";
            MainMenu.WriteLogo();
            Console.WriteLine("Üdvözlünk!");
            Console.ReadKey(true);
            MainMenu.Menu();
        }
    }
}
