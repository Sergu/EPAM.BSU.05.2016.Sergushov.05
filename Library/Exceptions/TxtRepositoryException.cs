using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    public class TxtRepositoryException : Exception
    {
        public new string Message { get; private set; }
        public TxtRepositoryException(string exception)
        {
            Message = exception;
        }
    }
}
