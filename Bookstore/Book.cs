using System;

namespace Bookstore
{
    /// <summary>
    /// Represents a book with a title, author, genre, and ID.
    /// </summary>
    class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public Genre eGenre { get; private set; }
        public string ID { get; private set; }

        /// <summary>
        /// Defines the genre of a book.
        /// </summary>
        public enum Genre
        {
            Fiction,
            NonFiction,
            ScienceFiction,
            Mystery,
            Fantasy,
        }
        /// <summary>
        /// Initializes a new instance of the Book class.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="id">The unique identifier for the book.</param>
        /// <param name="genre">The genre of the book.</param>
        public Book(string title, string author, string id, Genre genre)
        {
            Title = title;
            Author = author;
            ID = id;
            eGenre = genre;
        }
    }
}
