using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore
{
    /// <summary>
    /// The Program class contains the main entry point of the application and methods for managing the bookstore's inventory.
    /// </summary>
    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nBookstore Inventory Management");
                Console.WriteLine("1. Add a new book to the inventory.");
                Console.WriteLine("2. Display the list of all books in the inventory.");
                Console.WriteLine("3. Display a book by book ID.");
                Console.WriteLine("4. Remove a book from the inventory.");
                Console.WriteLine("5. Exit the program.");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DisplayBooks();
                        break;
                    case "3":
                        DisplayBookById();
                        break;
                    case "4":
                        RemoveBook();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds a new book to the inventory after prompting the user for details.
        /// </summary>
        static void AddBook()
        {
            string title = GetTitle();
            string author = GetAuthor();
            // Modified this to use an enum to help with normalization / integrity
            Book.Genre genre = GetGenre();
            string id = GetID();
            books.Add(new Book(title, author, id, genre));
            Console.WriteLine("Book added successfully!");
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Prompts the user to enter a first and last name for an author, and validates the values before returning the value.
        /// Using a string value to demonstrate understanding, but would probably opt to use a 'Name' struct/class.  In a real DB there would also be a Person class / Author table
        /// </summary>
        /// <returns>The selected genre.</returns>
        private static string GetAuthor()
        {
            while (true) // Loop until valid input is provided
            {
                Console.Write("Enter the author's first and last name: ");
                string input = Console.ReadLine().Trim();

                // Check if input includes at least one space, implying at least two words
                if (!string.IsNullOrWhiteSpace(input) && input.Contains(" "))
                {
                    var parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    // Simple validation to ensure there are at least two parts (first and last name)
                    // This could be enhanced to handle middle names, initials, or titles
                    if (parts.Length >= 2)
                    {
                        return string.Join(" ", parts); // Rejoin parts to handle extra spaces user might have entered
                    }
                }

                Console.WriteLine("Invalid input. Please enter both a first and last name.");
            }
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Displays all books in the inventory.
        /// </summary>
        static void DisplayBooks()
        {
            Console.WriteLine("\n\n-------------- Inventory: -------------\n");
            if (books.Count == 0)
            {
                Console.WriteLine("No books in inventory.");
                return;
            }
            books.Sort((book1, book2) => string.Compare(book1.ID, book2.ID));
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.ID}, Title: {book.Title}, Author: {book.Author}, Genre: {book.eGenre}");
            }

            Console.WriteLine("\n\n");
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Displays a book's details by its ID.
        /// </summary>
        static void DisplayBookById()
        {
            Console.Write("Enter the book's ID to find: ");
            string id = Console.ReadLine();

            var book = books.Find(b => b.ID == id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine($"ID: {book.ID}, Title: {book.Title}, Author: {book.Author}, Genre: {book.eGenre}");
        }

        /// <summary>
        /// Removes a book from the inventory by its ID.
        /// </summary>
        static void RemoveBook()
        {
            Console.Write("Enter the book's ID to remove: ");
            string id = Console.ReadLine();

            var book = books.Find(b => b.ID == id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            books.Remove(book);
            Console.WriteLine("Book removed successfully.");
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Prompts the user to select a genre from the available genres.
        /// Wrote this class with flexibility in mind.  It could be optimized, but with the slight overhead increase it allows for more genres to be added as needed without hard-coding with magic values
        /// </summary>
        /// <returns>The selected genre.</returns>
        static Book.Genre GetGenre()
        {
            Console.WriteLine("Select the book's genre:");

            // Get all values from the Genre enum and display them with an index
            var genres = Enum.GetValues(typeof(Book.Genre));
            int index = 1;
            foreach (var genre in genres)
            {
                Console.WriteLine($"{index++}. {genre}");
            }

            Console.Write("Enter the number corresponding to the genre: ");
            string input = Console.ReadLine();
            int selectedOption;

            // Validate the input to ensure it is a number and within the range of available genres
            while (!int.TryParse(input, out selectedOption) || selectedOption < 1 || selectedOption > genres.Length)
            {
                Console.WriteLine("Invalid selection, please try again.");
                input = Console.ReadLine();
            }

            // Convert the selected option to the corresponding Genre enum value
            return (Book.Genre)(selectedOption - 1);
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Prompts the user to enter an ID for a book and verifies that it doesn't already exist in the list of books.
        /// </summary>
        /// <returns>A valid and unique ID for the book.</returns>
        private static string GetID()
        {
            while (true) // Loop until a valid and unique ID is provided
            {
                Console.Write("Enter the book's ID: ");
                string id = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Invalid ID. Please try again.");
                    continue;
                }

                // Check if the ID already exists in the list of books
                if (books.Any(book => book.ID == id))
                {
                    Console.WriteLine("A book with this ID already exists. Please use a unique ID.");
                    continue;
                }

                return id;
            }
        }
        ///-------------------------------------------------------------------------------------------
        /// <summary>
        /// Prompts the user to enter a title for a book and validates the input.
        /// </summary>
        /// <returns>The entered title.</returns>
        private static string GetTitle()
        {
            while (true) // Loop until a valid title is provided
            {
                Console.Write("Enter the book's title: ");
                string title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Invalid title. Title cannot be empty. Please try again.");
                    continue;
                }

                return title;
            }
        }


    }


}

