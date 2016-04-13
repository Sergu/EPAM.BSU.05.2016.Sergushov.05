using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library.Adapters;
using Library.Exceptions;
using NLog;

namespace Library
{
    public class Service : IService
    {
        private IRepository fileWorker;
        private List<Book> bookCollection = new List<Book>();
        private Logger logger = LogManager.GetCurrentClassLogger();
        public Service(IRepository fileworker)
        {
            this.fileWorker = fileworker;
            //bookCollection = new List<Book>(this.fileWorker.ReadFromFile());
        }
        public bool AddBook(Book addedBook)
        {
            bookCollection.Clear();
            bookCollection = new List<Book>(fileWorker.ReadFromFile());
            
            foreach(Book book in bookCollection)
            {
                if (book.Equals(addedBook))
                {
                    throw new AddBookException("this book already exist");
                }
            }
            Book copiedBook = addedBook.DeepCopy();
            bookCollection.Add(copiedBook);
            fileWorker.ReWriteBooksToFile(bookCollection);
            logger.Log(new LogEventInfo(LogLevel.Trace,"book added: ", copiedBook.ToString()));
            return true;
        }
        public bool RemoveBook(Book removedBook)
        {
            bookCollection.Clear();
            bookCollection = new List<Book>(fileWorker.ReadFromFile());
            bool wasRemoved = bookCollection.Remove(removedBook);
            if (wasRemoved == false)
            {
                throw new RemoveBookException("current book doesn't exist");
            }
            logger.Log(new LogEventInfo(LogLevel.Trace, "book removed: ",wasRemoved.ToString()));
            fileWorker.ReWriteBooksToFile(bookCollection);
            return true;
        }
        public List<Book> FindByTag(string tag)
        {
            bookCollection.Clear();
            bookCollection = new List<Book>(fileWorker.ReadFromFile());
            List<Book> findedBooks = new List<Book>();
            foreach(Book book in bookCollection)
            {
                if(book.ToString().Contains(tag))
                {
                    findedBooks.Add(book.DeepCopy());
                }
            }
            logger.Log(new LogEventInfo(LogLevel.Trace, "find books: ", string.Format("by tag: {0}  was found {1} books, ",tag, findedBooks.ToString())));
            return findedBooks;
        }
        public void SortBookByTag(IComparer<Book> stradegy)
        {
            if (stradegy == null)
                throw new SortBooksException("stradegy undefined");
            bookCollection.Clear();
            bookCollection = new List<Book>(fileWorker.ReadFromFile());
            Book[] bookArray = bookCollection.ToArray();
            SortBooks(bookArray, stradegy);
            bookCollection = bookArray.ToList<Book>();
            fileWorker.ReWriteBooksToFile(bookCollection);
        }
        private void SortBooks(Book[] bookArray, IComparer<Book> stradegy)
        {
            for (int i = 0; i < bookArray.Length - 1; i++)
            {
                for (int j = 0; j < bookArray.Length - i - 1; j++)
                {
                    if(stradegy.Compare(bookArray[j],bookArray[j+1])>0)
                    {
                        Swap(ref bookArray[j], ref bookArray[j + 1]);
                    }
                }
            }
        }
        private void Swap(ref Book book1,ref Book book2)
        {
            Book temp = book1;
            book1 = book2;
            book2 = temp;
        }
    }
}
