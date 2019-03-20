using System;
using System.Collections.Generic;

namespace ppedv.BookManager2000.Model
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public DateTime Jahr { get; set; }
        public virtual HashSet<Person> Autoren { get; set; } = new HashSet<Person>();
    }
}
