using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Tests
{

    [TestClass]
    public class BookManagerTests
    {
        class testRepo : IBookRepository
        {
            public IEnumerable<Book> GetAllBooks()
            {
                return new List<Book>();
            }
        }
        [TestMethod]
        public void BookManager_GetOldestBook_no_books_results_null()
        {
            var bm = new BooksManager(new testRepo());

            var result = bm.GetOldestBook();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void BookManager_GetOldestBook_two_books_second_is_oldest()
        {
            var mock = new Mock<IBookRepository>();
            mock.Setup(x => x.GetAllBooks()).Returns(() =>
            {
                var b1 = new Book() { Title = "EINS", Jahr = DateTime.Now };
                var b2 = new Book() { Title = "ZWEI", Jahr = DateTime.Now.AddDays(-1) };
                return new[] { b1, b2 };
            });
            var bm = new BooksManager(mock.Object);

            var result = bm.GetOldestBook();

            Assert.AreEqual("ZWEI", result.Title);
        }
    }
}
