namespace PhilLendingLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var library = new Library();
                var backpack = new Backpack<Book>();
                UserInterface(library, backpack);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message); 
            }
          
        }

        static void UserInterface(Library library, Backpack<Book> backpack)
        {

            LoadBooks(library); // Add some initial books to the library

            while (true)
            {
                Console.WriteLine("--- Welcome to Phil's Lending Library ^_^ ---");
                Console.WriteLine("1. View all Books");
                Console.WriteLine("2. Add a Book");
                Console.WriteLine("3. Borrow a book");
                Console.WriteLine("4. Return a book");
                Console.WriteLine("5. View Book Bag");
                Console.WriteLine("6. Search a Book by Title");
                Console.WriteLine("7. Exit");
                Console.WriteLine("---------------------------------------------");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();
                

                switch (choice)
                {
                    case "1":
                        ViewAllBooks(library);
                        break;
                    case "2":
                        AddBook(library);
                        break;
                    case "3":
                        BorrowBook(library, backpack);
                        break;
                    case "4":
                        ReturnBook(library, backpack);
                        break;
                    case "5":
                        ViewBookBag(backpack);
                        break;
                    case "6":
                        SearchBookByTitle(library);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
        static void LoadBooks(Library library)
        {
            library.Add("The 7 Habits of Highly Effective People", "Stephen", "Covey", 381);
            library.Add("Inspiration from the Pen", "Mustafa", "Rafiee", 128);
            library.Add("Reviving the Sciences of Religion", "AbuHamed", "Ghazali", 1266);
            library.Add("The Alchemist", "Paulo", "Coelho", 208);
            library.Add("Sophie's World", "Jostein", "Gaarder", 514);
            library.Add("Smart reading", "Saged", "Abdali", 108);
        }

        static void ViewAllBooks(Library library)
        {
            Console.WriteLine("All Books:");

            foreach (var book in library)
            {
                Console.WriteLine($"- {book.Title} by {book.FirstName} {book.LastName}");
            }
        }

        static void AddBook(Library library)
        {
            Console.Write("Enter the title of the book: ");
            var title = Console.ReadLine()!;
            Console.Write("Enter the author first name of the book: ");
            var authorFN = Console.ReadLine()!;
            Console.Write("Enter the author last name of the book: ");
            var authorLN = Console.ReadLine()!;
            Console.Write("Enter the number of pages of the book: ");
            var NumberOfPages = Convert.ToInt32( Console.ReadLine());


            library.Add(title, authorFN, authorLN, NumberOfPages);
            Console.WriteLine("Book added to the library.");
        }

        static void BorrowBook(Library library, Backpack<Book> backpack)
        {
            Console.Write("Enter the title of the book to borrow: ");
            var title = Console.ReadLine();

            var book = library.Borrow(title);
            if (book != null)
            {
                backpack.Pack(book);
                Console.WriteLine("Book borrowed and added to your backpack.");
            }
            else
            {
                Console.WriteLine("Book not found in the library.");
            }
        }

       static void ReturnBook(Library library, Backpack<Book> backpack)
{
    if (!backpack.Any())
    {
        Console.WriteLine("Your backpack is empty.");
        return;
    }

    Console.WriteLine("Book Bag:");
    int index = 1;
    foreach (var item in backpack)
    {
        Console.WriteLine($"{index++}. {item.Title} by {item.FirstName} {item.LastName}");
    }

    Console.Write("Enter the number of the book you want to return: ");
    if (int.TryParse(Console.ReadLine(), out int selectedIndex))
    {
        try
        {
            if (selectedIndex-1 >= 0 && selectedIndex-1 < backpack.Count())
            {
                var returnedBook = backpack.Unpack(selectedIndex-1);
                library.Return(returnedBook);
                Console.WriteLine("Book returned to the library.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Invalid index.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid index.");
    }
}

        static void ViewBookBag(Backpack<Book> backpack)
        {
           if (!backpack.Any())
           {
              Console.WriteLine("Your backpack is empty.");
               return;
           }

               Console.WriteLine("Book Bag:");

            int index = 1;
            foreach (var item in backpack)
            {
                Console.WriteLine($"{index++}. {item.Title}");
            }
        }

        static void SearchBookByTitle(Library library)
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine()!;

            var book = library.SearchByTitle(title);
            if (book != null)
            {
                Console.WriteLine("Book found:");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.FirstName} {book.LastName}");
                Console.WriteLine($"Number of Pages: {book.NumberOfPages}");
            }
            else
            {
                Console.WriteLine("Book you searched not found!.");
            }
        }

    }
}