using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnitTests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestCaseAttribute : Attribute
    {
        public string Description { get; private set; }
        public TestCaseAttribute() 
        {
            Description = string.Empty;
        }

        public TestCaseAttribute(string description) 
        {
            Description = description;
        }
    }
}
