using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Adapters;
using Library.Exceptions;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Repositories
{
    public class XmlRepository : IRepository
    {
        public string Filepath { get; private set; }
        public XmlRepository(string filePath)
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
            XmlWriter writer = null;
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(IEnumerable<Book>));
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
                writer = XmlWriter.Create(Filepath, settings);
                //foreach (Book book in books)
                //{
                //    serializer.WriteObject(writer,book);
                //}
                serializer.WriteObject(writer, books);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
            finally
            {
                writer.Close();
            }
            return true;
        }
        public bool WriteBookToFile(Book book)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Book));
            using (XmlWriter w = XmlWriter.Create(Filepath))
                serializer.WriteObject(w, book);
            return true;
        }
        public IEnumerable<Book> ReadFromFile()
        {
            List<Book> books = new List<Book>();
            DataContractSerializer serializer = new DataContractSerializer(typeof(IEnumerable<Book>));
            XmlReader reader = null;
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
                reader = XmlReader.Create(Filepath);
                try
                {
                    books = new List<Book>((IEnumerable<Book>)serializer.ReadObject(reader));
                }
                catch (Exception ex)
                {
                    return books;
                }
                finally
                {
                    reader.Close();
                }
                return books;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }
    }
}
