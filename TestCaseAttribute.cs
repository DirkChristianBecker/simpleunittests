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
        public bool Active { get; private set; }

        public TestCaseAttribute(bool active = true) 
        {
            Description = string.Empty;
            Active = active;
        }

        public TestCaseAttribute(string description) 
        {
            Description = description;
        }
    }
}
