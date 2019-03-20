using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bm = new BooksManager(new GoogleBookApIRepo());

            int zahl = 1;
            Console.WriteLine($"{zahl}");
            zahl += 1;
            Console.WriteLine($"{zahl}");
            zahl *= 1;
            Console.WriteLine($"{zahl}");
            zahl -= 1;

            foreach (var b in bm.GetAllBookTitles())
            {
                Console.WriteLine($"[{b.Id:00}] {b.Title} - {b.Jahr}");
            }

            Console.WriteLine($"OldBook: {bm.GetOldestBook().Title}");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
