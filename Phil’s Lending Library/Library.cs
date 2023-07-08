using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilLendingLibrary
{
    public class Library : ILibrary
    {
        public Dictionary<string, Book> books ;

        public Library()
        {
            books = new Dictionary<string, Book>(StringComparer.OrdinalIgnoreCase);
        }

        public int Count => books.Count;

        public void Add(string title, string firstName, string lastName, int numberOfPages)
        {
            var book = new Book { Title = title, FirstName = firstName, LastName = lastName, NumberOfPages = numberOfPages };
            books[title] = book;
        }

        public Book Borrow(string title)
        {
            if (books.TryGetValue(title, out var book))
            {
                books.Remove(title);
                return book;
            }

            return null;
        }

        public void Return(Book book)
        {
            if (book != null && !books.ContainsKey(book.Title))
            {
                books[book.Title] = book;
            }
        }

        public Book SearchByTitle(string title)
        {
            foreach (var book in books.Values)
            {
                if (string.Equals(book.Title, title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
                
            }

            return null;
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return books.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
