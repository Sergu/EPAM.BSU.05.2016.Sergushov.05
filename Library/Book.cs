using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book : IEquatable<Book>
    {
        public string author { get; private set; }
        public string title { get; private set; }
        public int pages { get; private set; }
        public static string[] separator { get; private set; }
        public Book(string author,string title,int pages)
        {
            this.author = author;
            this.title = title;
            this.pages = pages;
            separator = new string[] {"@@@"};
        }
        public bool Equals(Book book)
        {
            if (ReferenceEquals(null, book))
                return false;
            if (ReferenceEquals(this, book))
                return true;
            if ((this.author == book.author) && (this.title == book.title) && (this.pages == book.pages))
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Book book = obj as Book;
            if (book == null)
                return false;
            else
            {
                return this.Equals(book);
            }
        }
        public Book DeepCopy()
        {
            return new Book(this.author, this.title, this.pages);
        }
        public override string ToString()
        {
            return string.Format("Author:{0}{1}Title:{2}{3}Pages:{4}",author,separator,title,separator,pages);
        }
    }
}
