using System.Collections.Generic;

namespace ppedv.BookManager2000.Model
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public virtual HashSet<Book> Books { get; set; } = new HashSet<Book>();
    }
}
