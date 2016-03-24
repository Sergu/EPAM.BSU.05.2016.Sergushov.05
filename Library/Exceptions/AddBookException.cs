using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Excptions
{
    public class AddBookException : Exception
    {
        public new string Message {get;private set;}
        public AddBookException(string message)
        {
            this.Message = message;
        }
    }
}
