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
        private string filePath
        {
            get;
            set;
        }
        private IFileWorker fileWorker;
        private List<Book> bookCollection = new List<Book>();
        public Repository(string filePath,IFileWorker fileworker)
        {
            this.filePath = filePath;
            this.fileWorker = fileworker;
        }
        public bool AddBook(Book book)
        {

        }
    }
}
