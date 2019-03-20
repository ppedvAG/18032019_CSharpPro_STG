using ppedv.BookManager2000.Logic;
using ppedv.BookManager2000.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.BookManager2000.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** BookManager v0.1 GOLD EDITION ***");

            var core = new Core();
            foreach (var b in core.Repository.GetAll<Book>())
            {
                Console.WriteLine($"{b.Title}");
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
