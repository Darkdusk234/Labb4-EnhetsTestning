﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4_EnhetsTestning
{
    public class LibrarySystem
    {
        private List<Book> books;

        public LibrarySystem()
        {
            books = new List<Book>();
            // Add some initial books
            books.Add(new Book("1984", "George Orwell", "9780451524935", 1949));
            books.Add(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084", 1960));
            books.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565", 1925));
            books.Add(new Book("The Hobbit", "J.R.R. Tolkien", "9780547928227", 1937));
            books.Add(new Book("Pride and Prejudice", "Jane Austen", "9780141439518", 1813));
            books.Add(new Book("The Catcher in the Rye", "J.D. Salinger", "9780316769488", 1951));
            books.Add(new Book("Lord of the Flies", "William Golding", "9780399501487", 1954));
            books.Add(new Book("Brave New World", "Aldous Huxley", "9780060850524", 1932));
        }

        public bool AddBook(Book book)
        {
            if(book.ISBN == null || book.ISBN.Length == 0)
            {
                return false;
            }
            else if(!long.TryParse(book.ISBN, out long result))
            {
                return false;
            }
            else if (SearchByISBN(book.ISBN) != null)
            {
                return false;
            }
            books.Add(book);
            return true;
        }

        public bool RemoveBook(string isbn)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && !book.IsBorrowed)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public Book SearchByISBN(string isbn)
        {
            if (isbn != null && isbn != String.Empty)
            {
                return books.Where(b => b.ISBN.Contains(isbn)).FirstOrDefault();
            }
            return null;
        }

        public List<Book> SearchByTitle(string title)
        {
            if (title != null && title != String.Empty)
            {
                return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return null;
        }

        public List<Book> SearchByAuthor(string author)
        {
            if (author != null && author != String.Empty)
            {
                return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return null;
        }

        public bool BorrowBook(string isbn)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                book.BorrowDate = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool ReturnBook(string isbn)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                book.BorrowDate = null;
                return true;
            }
            return false;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public decimal CalculateLateFee(string isbn, int daysLate)
        {
            if (daysLate <= 0)
                return 0;

            Book book = SearchByISBN(isbn);
            if (book == null)
                return 0;
            else if (!book.IsBorrowed)
                return 0;

            decimal feePerDay = 0.5m;
            return daysLate * feePerDay;
        }

        public bool IsBookOverdue(string isbn, int loanPeriodDays)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && book.IsBorrowed && book.BorrowDate.HasValue)
            {
                TimeSpan borrowedFor = DateTime.Now - book.BorrowDate.Value;
                return borrowedFor.Days > loanPeriodDays;
            }
            return false;
        }
    }
}
