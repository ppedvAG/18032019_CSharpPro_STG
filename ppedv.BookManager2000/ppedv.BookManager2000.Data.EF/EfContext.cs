using ppedv.BookManager2000.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.BookManager2000.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Personen { get; set; }

    }
}
