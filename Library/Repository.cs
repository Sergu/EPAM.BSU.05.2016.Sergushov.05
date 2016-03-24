using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library.Adapters;
using Library.Excptions;

namespace Library
{
    public class Repository : IRepository
    {
        private IFileWorker fileWorker;
        private List<Book> bookCollection = new List<Book>();
        public Repository(IFileWorker fileworker)
        {
            this.fileWorker = fileworker;
            bookCollection = this.fileWorker.ReadFromFile();
        }
        public bool AddBook(Book addedBook)
        {
            bookCollection.Clear();
            bookCollection = fileWorker.ReadFromFile();
            
            foreach(Book book in bookCollection)
            {
                if (book.Equals(book))
                {
                    throw new AddBookException("this book already exist");
                }
            }
            Book copiedBook = addedBook.DeepCopy();
            fileWorker.WriteBookToFile(copiedBook);
            bookCollection.Add(copiedBook);
            return true;
        }
        public bool RemoveBook(Book removedBook)
        {
            bookCollection.Clear();
            bookCollection = fileWorker.ReadFromFile();
            bool wasRemoved = bookCollection.Remove(removedBook);
            if (wasRemoved == false)
            {
                throw new RemoveBookException("current book doesn't exist");
            }
            fileWorker.ReWriteBooksToFile(bookCollection);
            return true;
        }
        public List<Book> FindByTag(string tag)
        {
            bookCollection.Clear();
            bookCollection = fileWorker.ReadFromFile();
            List<Book> findedBooks = new List<Book>();
            foreach(Book book in bookCollection)
            {
                if(book.ToString().Contains(tag))
                {
                    findedBooks.Add(book.DeepCopy());
                }
            }
            return findedBooks;
        }
        public void SortBookByTag(SortTag tag)
        {
            ISortStradegy sortStradegy;
            switch (tag){
                case SortTag.byAuthor:
                    sortStradegy = new SortByAuthorStradegy();
                    break;
                case SortTag.byTitle:
                    sortStradegy = new SortByTitleStradegy();
                    break;
                case SortTag.byPagesCount:
                    sortStradegy = new SortByPageCount();
                    break;
                default:
                    sortStradegy = new SortByAuthorStradegy();
                    break;
            }
            bookCollection.Clear();
            bookCollection = fileWorker.ReadFromFile();
            Book[] bookArray = bookCollection.ToArray();
            SortBooks(bookArray, sortStradegy);
            bookCollection = bookArray.ToList<Book>();
            fileWorker.ReWriteBooksToFile(bookCollection);
        }
        private void SortBooks(Book[] bookArray,ISortStradegy stradegy)
        {
            for (int i = 0; i < bookArray.Length - 1; i++)
            {
                for (int j = 0; j < bookArray.Length - i - 1; j++)
                {
                    string firstKey = stradegy.GetKey(bookArray[j]);
                    string secKey = stradegy.GetKey(bookArray[j + 1]);
                    if (firstKey.CompareTo(secKey)>0)
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
