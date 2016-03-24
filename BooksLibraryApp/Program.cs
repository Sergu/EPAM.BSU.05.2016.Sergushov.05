using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.Adapters;

namespace BooksLibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository(new TxtFileWorker("a.txt"));
            repository.AddBook(new Book("Tolstoy", "War and Piece", 365));
            repository.AddBook(new Book("Dostoevsky", "Prestuplenie", 567))

            Console.ReadLine();
        }
    }
}
