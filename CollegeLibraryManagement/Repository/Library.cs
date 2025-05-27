using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using CollegeLibraryManagement.Model;
using CollegeLibraryManagement.Utility;
using CollegeLibraryManagement.Service;
using System.Linq.Expressions;

namespace CollegeLibraryManagement.Repository
{
    public class Library:ILibrary

    {

        //dictionary for storing books
        public static Dictionary<string, Book> books = new Dictionary<string, Book>();

        //unique id number generation
        private static int idnum = 1000;

        
        #region Add Book
        public void AddBook()
        {
            try
            {
                // Title
                string title;
                while (true)
                {
                    Console.Write("Enter Title: ");
                    title = Console.ReadLine();
                    if (CustomValidations.IsValidTitleOrAuthor(title))
                        break;

                    Console.WriteLine("Invalid title. enter again");
                }

                // Author
                string author;
                while (true)
                {
                    Console.Write("Enter Author: ");
                    author = Console.ReadLine();
                    if (CustomValidations.IsValidTitleOrAuthor(author))
                        break;

                    Console.WriteLine("Invalid author. enter again");
                }

                // Published Date
                DateTime publishedDate;
                while (true)
                {
                    Console.Write("Enter Published Date (YYYY-MM-DD): ");
                    if (CustomValidations.IsValidPublishedDate(Console.ReadLine(), out publishedDate))
                        break;

                    Console.WriteLine("Invalid date enter again.");
                }
                //automatic isbn creation
                    string isbn = $"LB{idnum++}";
                    Console.WriteLine($"Generated ISBN (Library Book Id): {isbn}");

                //adding book to dictionary
                    Book newBook = new Book
                    {
                        ISBN = isbn,
                        Title = title,
                        Author = author,
                        PublishedDate = publishedDate
                    };

                    books.Add(isbn, newBook);
                    Console.WriteLine("Book added successfully.");  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        #endregion

        #region Remove Book
        public void RemoveBook()
        {
            try
            {
                //enter isbn
                Console.Write("Enter ISBN to delete: ");
                string isbn = Console.ReadLine();

                //check if isbn exsist or not and delete
                if (books.ContainsKey(isbn))
                {
                    books.Remove(isbn);
                    Console.WriteLine("Book deleted successfully.");
                }
                else
                {
                    throw new UserDefinedException(isbn);

                }
            }
            catch (UserDefinedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        #endregion

        #region List All Books
        public void ListAllBooks()
        {
            try
            {
                //check  if books are there or not and print the details of available books in library
                if (books.Count == 0)
                {
                    Console.WriteLine("No books in the library.");
                }
                else
                {
                    foreach (var book in books.Values)
                    {
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine($"ISBN: {book.ISBN}");
                        Console.WriteLine($"Title: {book.Title}");
                        Console.WriteLine($"Author: {book.Author}");
                        Console.WriteLine($"Published Date: {book.PublishedDate}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Search by ISBN
        public void SearchByISBN()
        {
            try
            {
                //input isbn
                Console.Write("Enter ISBN to search: ");
                string isbn = Console.ReadLine();

                //if avilable show the details
                if (books.TryGetValue(isbn, out Book book))
                {
                    Console.WriteLine($"ISBN: {book.ISBN}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    Console.WriteLine($"Published Date: {book.PublishedDate}");
                }
                else
                {
                    throw new UserDefinedException(isbn);

                }
            }
            catch (UserDefinedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Edit Book

        public void EditBook()
        {
            
                Console.Write("Enter ISBN to update: ");
                string isbn = Console.ReadLine();
            try { 
                if (!books.ContainsKey(isbn))
                    throw new UserDefinedException(isbn);

                if (books.ContainsKey(isbn))
                {
                    Book book = books[isbn];

                    // Update Title
                    Console.Write("New Title (leave blank to keep unchanged): ");
                    string newTitle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        if (CustomValidations.IsValidTitleOrAuthor(newTitle))
                        {
                            book.Title = newTitle;
                        }
                        else
                        {
                            Console.WriteLine("Invalid title. Update skipped.");
                        }
                    }

                    // Update Author
                    Console.Write("New Author (leave blank to keep unchanged): ");
                    string newAuthor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newAuthor))
                    {
                        if (CustomValidations.IsValidTitleOrAuthor(newAuthor))
                        {
                            book.Author = newAuthor;
                        }
                        else
                        {
                            Console.WriteLine("Invalid author. Update skipped.");
                        }
                    }

                    // Update Published Date
                    Console.Write("New Published Date (yyyy-MM-dd, blank to keep unchanged): ");
                    string newDateInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newDateInput))
                    {
                        if (CustomValidations.IsValidPublishedDate(newDateInput, out DateTime newPublishedDate))
                        {
                            book.PublishedDate = newPublishedDate;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date. Update skipped.");
                        }
                    }

                    Console.WriteLine("Book updated successfully.");
                }
                
            }
            catch (UserDefinedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        #endregion

        #region Search by Author
        public void SearchByAuthor()
        {
            // input isbn
            Console.Write("Enter Author name to search: ");
            string author = Console.ReadLine();
            bool found = false;
            try
            {
                //check vaildation
                foreach (var book in books.Values)
                {
                    if (book.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"ISBN: {book.ISBN}");
                        Console.WriteLine($"Title: {book.Title}");
                        Console.WriteLine($"Published Date: {book.PublishedDate}");
                        Console.WriteLine("----------------------------------");
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No books found for the given author.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        #endregion

        #region Search by Title
        public void SearchByTitle()
        {
            try
            {
                //input isbn
                Console.Write("Enter title of book to search: ");
                string title = Console.ReadLine();
                bool found = false;

                //validation
                foreach (var book in books.Values)
                {
                    if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"ISBN: {book.ISBN}");
                        Console.WriteLine($"Title: {book.Title}");
                        Console.WriteLine($"Published Date: {book.PublishedDate}");
                        Console.WriteLine("----------------------------------");
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No books found for the given title.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}

