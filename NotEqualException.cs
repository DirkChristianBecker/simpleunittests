using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnitTests
{
    public class NotEqualException : Exception
    {
        public NotEqualException(string message) : base(message) 
        {

        }
    }
}
