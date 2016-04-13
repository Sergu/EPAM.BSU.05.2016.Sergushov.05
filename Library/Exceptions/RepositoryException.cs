using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    public class RepositoryException : Exception
    {
        public new string Message { get; private set; }
        public RepositoryException(string exception)
        {
            Message = exception;
        }
    }
}
