using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Adapters;
using Library.Exceptions;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Repositories
{
    public class BinaryRepository : IRepository
    {
        public string Filepath { get; private set; }
        public BinaryRepository(string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
            finally
            {
                fs.Close();
            }
            this.Filepath = filePath;
        }
        public bool ReWriteBooksToFile(IEnumerable<Book> books)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream s = null;
            try
            {
                s = new FileStream(Filepath, FileMode.Create, FileAccess.Write);
                formatter.Serialize(s, books);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
            finally
            {
                s.Close();
            }
            return true;
        }
        public bool WriteBookToFile(Book book)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream s = File.OpenWrite(Filepath))
                formatter.Serialize(s, book);
            return true;
        }
        public IEnumerable<Book> ReadFromFile()
        {
            IEnumerable<Book> books = new List<Book>();
            IFormatter formatter = new BinaryFormatter();
            FileStream s = null;
            try
            {
                s = new FileStream(Filepath, FileMode.Open, FileAccess.Read);
                books = (IEnumerable<Book>)formatter.Deserialize(s);
            }
            catch (Exception ex)
            {
                return books;
            }
            finally
            {
                s.Close();
            }
            return books;
        }
    }
}
