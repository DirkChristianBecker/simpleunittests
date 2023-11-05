using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;

namespace SimpleUnitTests
{
    public class TestSuite
    {
        protected int TestAssertions { get; set; }
        public string Name { get; protected set; }
        public int NoOfTests { get; protected set; }
        
        public TestSuite(string name)
        {
            Name = name;
        }

        internal virtual void Setup()
        {

        }

        internal virtual void TearDown()
        {

        }

        public int GetTestAssertions()
        {
            return TestAssertions;
        }

        public List<MethodInfo> GetTestMethods()
        {
            var r = new List<MethodInfo>();

            foreach (var m in GetType().GetMethods())
            {
                var parameters = m.GetParameters();
                if(parameters.Length > 0)
                {
                    continue;
                }

                if(m.GetCustomAttribute(typeof(TestCaseAttribute)) == null)
                {
                    continue;
                }

                if(m.ReturnParameter.ParameterType != typeof(void))
                {
                    continue;
                }

                if(!m.IsPublic)
                {
                    continue;
                }

                r.Add(m);
            }

            NoOfTests = r.Count;

            return r;
        }

        protected void AssertEqual(int expected, int actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string menber = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new NotEqualException($"{expected} is not equal to {actual}. In {f}:{menber} line {line}. \n {comment}");
        }

        protected void AssertEqual(float expected, float actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string menber = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            var diff = expected - actual;
            if (Math.Abs(diff) <= double.Epsilon)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new NotEqualException($"{expected} is not equal to {actual}. In {f}:{menber} line {line}. \n {comment}");
        }

        protected void AssertEqual(double expected, double actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string menber = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            var diff = expected - actual; 
            if (Math.Abs(diff) <= double.Epsilon)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new NotEqualException($"{expected} is not equal to {actual}. In {f}:{menber} line {line}. \n {comment}");
        }

        protected void AssertEqual(string expected, string actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string menber = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new NotEqualException($"{expected} is not equal to {actual}. In {f}:{menber} line {line}. \n {comment}");
        }

        protected void AssertEqual(bool expected, bool actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string menber = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new NotEqualException($"{expected} is not equal to {actual}. In {f}:{menber} line {line}. \n {comment}");
        }

        protected void AssertEqual<T>(T expected, T actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string menber = "", [CallerLineNumber] int line = 0) where T : class
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new NotEqualException($"{expected} is not equal to {actual}. In {f}:{menber} line {line}. \n {comment}");
        }
    }
}
