using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IService
    {
        bool AddBook(Book addedBook);
        bool RemoveBook(Book removedBook);
        List<Book> FindByTag(string tag);
        void SortBookByTag(ISortStradegy stradegy);
    }
}
