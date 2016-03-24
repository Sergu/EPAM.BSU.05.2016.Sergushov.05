using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.Adapters;
using Library.Excptions;

namespace BooksLibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository(new TxtFileWorker("a.txt"));
            repository.AddBook(new Book("Tolstoy", "War and Piece", 365));
            repository.AddBook(new Book("Dostoevsky", "Prestuplenie", 567));
            try
            {
                repository.AddBook(new Book("Tolstoy", "War and Piece", 365));
            }
            catch(AddBookException ex)
            {
                Console.WriteLine(ex.Message);
            }
            repository.AddBook(new Book("Anderson", "Ugly duck", 2000));
            repository.SortBookByTag(SortTag.byAuthor);
            List<Book> books = repository.FindByTag("Ugly");
            repository.RemoveBook(books[0]);
            Console.ReadLine();
        }
    }
}
