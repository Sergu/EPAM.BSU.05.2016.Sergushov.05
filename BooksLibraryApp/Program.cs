using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.Adapters;
using Library.Exceptions;
using Library.Repositories;

namespace BooksLibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" txtRepository test ----------------------------------------------");
            Service repository = new Service(new TxtRepository("a.txt"));
            repository.AddBook(new Book("Tolstoy", "War and Piece", 365));
            repository.AddBook(new Book("Dostoevsky", "Prestuplenie", 567));
            try
            {
                repository.AddBook(new Book("Tolstoy", "War and Piece", 365));
            }
            catch (AddBookException ex)
            {
                Console.WriteLine(ex.Message);
            }
            repository.AddBook(new Book("Anderson", "Ugly duck", 2000));
            repository.SortBookByTag(new SortByPageCount());
            List<Book> books = repository.FindByTag("Ugly");
            repository.RemoveBook(books[0]);
            repository.SortBookByTag(new SortByTitleStradegy());

            Console.WriteLine(" BinaryRepository test  ----------------------------------------------");

            IService binservice = new Service(new BinaryRepository("binFile.bin"));
            try
            {
                binservice.AddBook(new Book("tolstoy", "war and piece", 30000));
                binservice.AddBook(new Book("Dostoevsky", "Killing old women", 3999));
                binservice.RemoveBook(new Book("Dostoevsky", "Killing old women", 3999));
                binservice.AddBook(new Book("Dostoevsky", "Killing old women", 3999));
                binservice.SortBookByTag(new SortByAuthorStradegy());
                List<Book> findedBooks = binservice.FindByTag("old");
                foreach(Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            catch (AddBookException ex)
            {
                Console.WriteLine("the same book is exist");
            }

            Console.WriteLine(" XmlRepository test  ----------------------------------------------");

            IService xmlService = new Service(new XmlRepository("xmlFile.xml"));
            try
            {
                xmlService.AddBook(new Book("tolstoy", "war and piece", 30000));
                xmlService.AddBook(new Book("Dostoevsky", "Killing old women", 3999));
                xmlService.SortBookByTag(new SortByPageCount());
            }
            catch(AddBookException ex)
            {
                Console.WriteLine("the same book is exist");
            }




            Console.ReadLine();
        }
    }
}
