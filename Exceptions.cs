using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnitTests
{
    public class TestRunnerException : Exception
    {
        public string File { get; set; }
        public string Caller { get; set; }
        public int Line { get; set; }
        
        public TestRunnerException(string file, string caller, int line, string message) : base(message)
        {
            File = file;
            Caller = caller;
            Line = line;
        }
    }
}
