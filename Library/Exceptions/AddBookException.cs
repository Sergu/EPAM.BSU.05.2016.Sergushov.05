using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
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
