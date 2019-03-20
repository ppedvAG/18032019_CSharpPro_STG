using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calculator
{
    public class BooksManager
    {
        public IBookRepository Repo { get; }

        public BooksManager(IBookRepository repo)
        {
            this.Repo = repo;
        }

        public IEnumerable<Book> GetAllBookTitles()
        {
            return Repo.GetAllBooks().OrderBy(x => x.Jahr).ThenBy(x => x.Title);
        }

        public Book GetOldestBook()
        {
            //Linq Query
            var query = from b in Repo.GetAllBooks()
                        orderby b.Jahr
                        select b;

            return query.FirstOrDefault();

            //Linq Lambda
            //return Repo.GetAllBooks().OrderBy(x => x.Jahr).FirstOrDefault();
        }
    }

    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
    }

    public class GoogleBookApIRepo : IBookRepository
    {
        public IEnumerable<Book> GetAllBooks()
        {
            //todo lade book per JSON
            yield return new Book() { Id = 1, Title = "Buch#1", Jahr = DateTime.Now.AddDays(4) };
            Thread.Sleep(2000);
            yield return new Book() { Id = 2, Title = "Buch#2", Jahr = DateTime.Now };
            Thread.Sleep(2000);
            yield return new Book() { Id = 3, Title = "Buch#3", Jahr = DateTime.Now.AddDays(-4) };
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Jahr { get; set; }
    }
}
