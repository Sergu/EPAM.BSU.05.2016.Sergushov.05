using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library.Adapters
{
    class TxtFileWorker : IFileWorker
    {
        public string filePath { get; private set; }
        public TxtFileWorker(string filePath)
        {
            this.filePath = filePath;
        }
        public IEnumerable<Book> ReadFromFile()
        {
            List<string> listBooks = new List<string>();
            FileStream fs = new FileStream(filePath, FileMode.Open,FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            string str;
            try
            {
                while(str = reader.ReadString())
                {
                    str = reader.ReadString()
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                reader.Close();
                fs.Close();
            }
        }
    }
}
