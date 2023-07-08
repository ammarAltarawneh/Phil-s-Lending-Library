using PhilLendingLibrary;

namespace LibraryTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddBook() 
        {
            // Arrange
            var library = new Library();

            // Act
            library.Add("Title 1", "First Name 1", "Last Name 1", 100);

            // Assert
            Assert.Single(library);
            Assert.Contains("Title 1", library.books.Keys);
        }

        [Fact]
        public void TestBorrowMissingBook() 
        {
            // Arrange
            var library = new Library();
            library.Add("Title 1", "First Name 1", "Last Name 1", 100);

            // Act
            var borrowedBook = library.Borrow("Title 4");

            // Assert
            Assert.Null(borrowedBook);

        }

        [Fact]
        public void TestBorrowExistingBook() 
        {
            // Arrange
            var library = new Library();
            library.Add("Title 1", "First Name 1", "Last Name 1", 100);
            library.Add("Title 2", "First Name 2", "Last Name 2", 200);

            // Act
            var borrowedBook = library.Borrow("Title 1");

            // Assert
            Assert.NotNull(borrowedBook);
            Assert.DoesNotContain(borrowedBook, library.books.Values);
            Assert.DoesNotContain("Title 1", library.books.Keys);
            Assert.Equal(1, library.books.Count());
        }

        [Fact]
        public void TestReturnBook() 
        {
            // Arrange
            var library = new Library();
            library.Add("Title 1", "First Name 1", "Last Name 1", 100);
            library.Add("Title 2", "First Name 2", "Last Name 2", 200);

            // Act
            var borrowedBook = library.Borrow("Title 1");
            library.Return(borrowedBook);

            // Assert
            Assert.Contains(borrowedBook.Title, library.books.Keys);
            Assert.Equal(2, library.books.Count());
        }

        [Fact]
        public void TestPackAndUnpack() 
        {
            // Arrange
            var backpack = new Backpack<Book>();
            var book1 = new Book { Title = "Title 1", FirstName = "First Name 1", LastName = "Last Name 1", NumberOfPages = 100 };
            var book2 = new Book { Title = "Title 2", FirstName = "First Name 2", LastName = "Last Name 2", NumberOfPages = 200 };
            // Act
            backpack.Pack(book1);
            backpack.Pack(book2);

            // Assert
            Assert.Equal(2, backpack.Count());
            Assert.Contains(book1, backpack);
            Assert.Contains(book2, backpack);

            // Act
            var unpackedBook = backpack.Unpack(0);

            // Assert
            Assert.Equal(book1, unpackedBook);
            Assert.Single(backpack);
            Assert.Contains(book2, backpack);
        }
    }
}