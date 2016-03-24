using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Adapters
{
    public interface IFileWorker
    {
        List<Book> ReadFromFile();
        bool ReWriteBooksToFile(List<Book> books);
        bool WriteBookToFile(Book book);
    }
}
