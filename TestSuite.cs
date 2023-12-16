using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Threading.Tasks;

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

        public virtual void Setup()
        {

        }

        public virtual void TearDown()
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

                var attrib = m.GetCustomAttribute(typeof(TestCaseAttribute)) as TestCaseAttribute;
                if (attrib == null || !attrib.Active)
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

        protected void AssertEqual(int expected, int actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(int expected, int actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(bool expected, bool actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(bool expected, bool actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(float expected, float actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            var diff = expected - actual;
            if (Math.Abs(diff) <= double.Epsilon)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(float expected, float actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            var diff = expected - actual;
            if (Math.Abs(diff) >= double.Epsilon)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(double expected, double actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            var diff = expected - actual;
            if (Math.Abs(diff) <= double.Epsilon)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(double expected, double actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            var diff = expected - actual;
            if (Math.Abs(diff) >= double.Epsilon)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(Guid expected, Guid actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(Guid expected, Guid actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(Vector2 expected, Vector2 actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(Vector2 expected, Vector2 actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(Vector3 expected, Vector3 actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(Vector3 expected, Vector3 actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(Vector4 expected, Vector4 actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(Vector4 expected, Vector4 actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual(string expected, string actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual(string expected, string actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertEqual<T>(T expected, T actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) where T : class
        {
            TestAssertions++;
            if (expected == actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is not equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertNotEqual<T>(T expected, T actual, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) where T : class
        {
            TestAssertions++;
            if (expected != actual)
            {
                return;
            }

            var f = Path.GetFileName(file);
            throw new TestRunnerException(file, member, line, $"{expected} is equal to {actual}. In {f}:{member} line {line}. {comment}");
        }

        protected void AssertException<T>(Action act, string comment = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            TestAssertions++;
            try
            {
                act.Invoke();
                throw new TestRunnerException(file, member, line, $"Expected exception of type {typeof(T).Name} but the action did not throw an exception. In {file}:{member} line {line}. {comment}");
            }
            catch (Exception ex) 
            {
                if(ex.GetType() == typeof(T))
                {
                    return;
                }

                throw new TestRunnerException(file, member, line, $"Expected exception of type {typeof(T).Name} but got {ex.InnerException.GetType().Name}. In {file}:{member} line {line}. {comment}");
            }
        }
    }
}
