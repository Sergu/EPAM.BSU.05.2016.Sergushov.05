using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Adapters
{
    public interface IRepository
    {
        IEnumerable<Book> ReadFromFile();
        bool ReWriteBooksToFile(IEnumerable<Book> books);
        bool WriteBookToFile(Book book);
    }
}
