using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    enum SearchTag
    {
        byAuthor,
        byPagesCount,
        byTitle
    };
    public interface IRepository
    {
        bool AddBook(Book book);
        bool RemoveBook(Book book);
        IEnumerable<Book> FindByTag(IEnumerable<string> tags);
        void SortBookByTag();
    }
}
