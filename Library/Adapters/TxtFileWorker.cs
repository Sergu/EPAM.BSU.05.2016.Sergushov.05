using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library.Exceptions;

namespace Library.Adapters
{
    public class TxtFileWorker : IFileWorker
    {
        public string filePath
        {
            get;
            private set;
            //try
            //{
            //    filePath = value;
            //    FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.ReadWrite);
            //}
            //catch(FileNotFoundException ex)
            //{
            //    throw new TxtFileWorkerException(ex.Message);
            //}

        }
        public TxtFileWorker(string filePath)
        {
            this.filePath = filePath;
        }
        public bool ReWriteBooksToFile(List<Book> books)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fs.Close();
            foreach(Book book in books)
            {
                WriteBookToFile(book);
            }
            return true;
        }
        public bool WriteBookToFile(Book book)
        {
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(Encoding.GetEncoding(866).GetBytes(book.ToString()+"\r\n"));
            }
            catch (Exception ex)
            {
                throw new TxtFileWorkerException(ex.Message);
            }
            finally{
                fs.Close();
                bw.Close();
            }
            return true;
        }
        public List<Book> ReadFromFile()
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                try
                {
                    while (true)
                    {
                        char symbol = reader.ReadChar();
                        builder.Append(symbol);
                    }
                }
                catch (EndOfStreamException ex)
                {

                }
                finally
                {
                    reader.Close();
                    fs.Close();
                }
            }
            catch(Exception ex)
            {
                TxtFileWorkerException exception = new TxtFileWorkerException(ex.Message);
                throw exception;
            }
            string[] stringArray = builder.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return ParseStrings(stringArray);
        }
        private List<Book> ParseStrings(string[] listStrings)
        {
            List<Book> books = new List<Book>();
            foreach(string str in listStrings)
            {
                Book book = ParseString(str);
                if (book != null)
                    books.Add(book);
            }
            return books;
        }
        private Book ParseString(string parsedStr)
        {
            string[] stringArr = parsedStr.Split(Book.separator, StringSplitOptions.None);
            Book newBook;
            string author = "";
            string title = "";
            int pages = 0;
            foreach(string str in stringArr)
            {
                string[] strArr = str.Split(new String[] { "Author:" }, StringSplitOptions.None);
                if (strArr.Length > 1)
                {
                    for(int i = 1;i<strArr.Length;i++)
                    {
                        string s = strArr[i];
                        author = author + s;
                    }
                    continue;
                }
                strArr = str.Split(new String[] { "Title:" }, StringSplitOptions.None);
                if(strArr.Length > 1)
                {
                    for (int i = 1; i < strArr.Length; i++)
                    {
                        string s = strArr[i];
                        title = title + s;
                    }
                    continue;
                }
                strArr = str.Split(new String[] { "Pages:" }, StringSplitOptions.None);
                if (strArr.Length > 1)
                {
                    for (int i = 1; i < strArr.Length; i++)
                    {
                        int numb;
                        if (!int.TryParse(strArr[i], out numb))
                        {
                            throw new TxtFileWorkerException("Incorrect pageCount format");
                        }  
                        pages = pages + numb;
                    }
                }
            }
            if((author != "")&&(title != "")&&(pages != 0))
            {
                newBook = new Book(author, title, pages);
            }
            else
            {
                throw new TxtFileWorkerException("Incorrect Book format");
            }
            return newBook;
        }
    }
}
