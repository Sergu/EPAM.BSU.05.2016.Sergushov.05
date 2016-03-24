using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Excptions
{
    public class RemoveBookException : Exception
    {
        public new string Message {get;private set;}
        public RemoveBookException(string message)
        {
            this.Message = message;
        }
    }
}
