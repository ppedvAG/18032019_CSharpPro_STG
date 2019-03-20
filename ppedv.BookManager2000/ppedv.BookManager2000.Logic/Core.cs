using ppedv.BookManager2000.Model;
using ppedv.BookManager2000.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.BookManager2000.Logic
{
    public class Core
    {
        public IRepository Repository { get; }


        public Book GetBookWithLongestAutorenNamen()
        {
            return Repository.GetAll<Book>().OrderBy(x => x.Autoren.Sum(y => y.Name.Length)).FirstOrDefault();
        }


        public Core(IRepository repo)
        {
            this.Repository = repo;
        }

        public Core() : this(new Data.EF.EfRepository())
        { }

    }
}
