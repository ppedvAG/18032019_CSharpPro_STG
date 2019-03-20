using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.BookManager2000.Model;

namespace ppedv.BookManager2000.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_can_create_db()
        {
            using (var con = new EfContext())
            {
                con.Database.Exists();
                con.Database.Delete();

                Assert.IsFalse(con.Database.Exists());
                con.Database.Create();
                Assert.IsTrue(con.Database.Exists());
            }
        }


        [TestMethod]
        public void EfContext_can_read_book()
        {
            var b = new Book() { Title = "Test", Jahr = DateTime.Now };


            using (var con = new EfContext())
            {
                con.Books.Add(b);
                con.SaveChanges();
            }
        }
    }
}
