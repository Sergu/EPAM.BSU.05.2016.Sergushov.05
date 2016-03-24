using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SortByTitleStradegy : ISortStradegy
    {
        public string GetKey(Book book)
        {
            return book.title;
        }
    }
}
