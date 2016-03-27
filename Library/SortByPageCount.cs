using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SortByPageCount : ISortStradegy
    {
        public string GetKey(Book book)
        {
            return book.pages.ToString();
        }
        public int Compare(Book x, Book y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
                return 0;
            if (x == null)
                return -1;
            if (y == null)
            {
                return 1;
            }
            return int.Parse(GetKey(x)) - int.Parse(GetKey(y));
        }
    }
}
