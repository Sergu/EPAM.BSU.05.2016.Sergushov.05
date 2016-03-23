using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Adapters
{
    public interface IFileWorker
    {
        IEnumerable<Book> ReadFromFile();
        bool WriteToFile(IEnumerable<Book> books);
    }
}
