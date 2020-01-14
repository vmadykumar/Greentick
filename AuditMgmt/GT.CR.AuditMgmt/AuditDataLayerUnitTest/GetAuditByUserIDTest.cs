using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
using AuditMgmt.DataLayer;
using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.DataLayer.DataImplementationLayer;

namespace AuditDataLayerUnitTest
{
    [TestClass]
    public class GetAuditByUserIDTest
    {


        //public void InitContext()
        //{
        //    var builder = new DbContextOptionsBuilder<AuditContext>().UseSqlServer("");
        //}
        [TestMethod]
        [DataRow("UU0000001", "Auditor", "UU0000011", 24)]
        [DataRow("UU0000001", "Manager", "UU0000011", 24)]
        public void Returns_ValidCount(string userID, string Role, string UUIDs, int count)
        {
            var audits = new Audit[24];
            for (int i = 0; i < 23; i++)
            {

               // audits[i] = new Audit() { UserID = UUIDs };
            }
            #region Initialisation
            var builder = new DbContextOptionsBuilder<AuditContext>().UseSqlServer("abc");
            // Arrange 
            var mockSet = new Mock<DbSet<Audit>>();
            var mockContext = new Mock<AuditContext>(builder.Options);
            mockSet.As<IQueryable<Audit>>().Setup(m => m.GetEnumerator()).Returns(audits.AsQueryable().GetEnumerator());
            mockContext.Setup(c => c.Audit).Returns(mockSet.Object);
            #endregion
            //Act
            //var repository = new AuditRepository(mockContext.Object, null);
           // var actual = repository.GetAuditsByUserID(userID, new List<string>() { UUIDs }, Role);

            //Assert
           // Assert.AreEqual(count, actual.Count());

        }


        //[TestMethod]
        //[DataRow("UU0000001", "Auditor", "UU0000011", 24)]
        //[DataRow("UU0000001", "Manager", "UU0000011", 24)]
        //public void Dummy(string userID, string Role, string UUIDs, int count)
        //{
        //    {
        //        // Arrange - We're mocking our dbSet & dbContext
        //        // in-memory data
        //        IQueryable<Book> books = new List<Book>
        //    {
        //        new Book
        //        {
        //            Title = "Hamlet",
        //            Author = "William Shakespeare"
        //        },
        //        new Book
        //        {
        //            Title = "A Midsummer Night's Dream",
        //            Author = "William Shakespeare"
        //        }

        //    }.AsQueryable();

        //        // To query our database we need to implement IQueryable 
        //        var mockSet = new Mock<DbSet<Book>>();
        //        mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
        //        mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
        //        mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
        //        mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

        //        var mockContext = new Mock<BookContext>();
        //        mockContext.Setup(c => c.Books).Returns(mockSet.Object);

        //        // Act - fetch books
        //        var repository = new BookRepository(mockContext.Object);
        //        var actual = repository.FetchBooks();

        //        // Asset
        //        // Ensure that 2 books are returned and
        //        // the first one's title is "Hamlet"
        //        Assert.AreEqual(2, actual.Count());
        //        Assert.AreEqual("Hamlet", actual.First().Title);
        //    }
    }
}

