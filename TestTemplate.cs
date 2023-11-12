using SimpleUnitTests;
using System;

namespace SimpleUnitTests.Tests
{
    public class TestTemplate : TestSuite
    {
        public TestTemplate() : base("Test template")
        {
        }

        [TestCase()]
        public void TestInteger()
        {
            AssertEqual(1, 1);
            AssertNotEqual(1, 2);
        }

        [TestCase()]
        public void TestFloat()
        {
            AssertEqual(1.0f, 1.0f);
            AssertNotEqual(1.0f, 2.0f);
        }

        [TestCase()]
        public void TestDouble()
        {
            AssertEqual(1.0, 1.0);
            AssertNotEqual(1.0, 2.0);
        }

        [TestCase()]
        public void TestString()
        {
            AssertEqual("1.0", "1.0");
            AssertNotEqual("1.0", "2.0");
        }

        [TestCase()]
        public void TestBool()
        {
            AssertEqual(true, true);
            AssertNotEqual(true, false);
        }

        [TestCase(false)]
        public void DeactivatedTest()
        {
            AssertEqual(true, true);
            AssertNotEqual(true, false);
        }

        private void TestHelper(object o)
        {
            if(o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
        }

        [TestCase()]
        public void ExceptionTest()
        {
            AssertException<ArgumentNullException>(() =>
            {
                TestHelper(null);
            });
        }
    }
}
