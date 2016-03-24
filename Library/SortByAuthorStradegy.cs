using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SortByAuthorStradegy : ISortStradegy
    {
        public string GetKey(Book book)
        {
            return book.author;
        }
    }
}
