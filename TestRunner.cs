using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleUnitTests
{
    public class TestEvent : EventArgs
    {
        public TestSuite TestSuite { get; set; }
    }

    public class TestMethodEvent : EventArgs 
    {
        public MethodInfo Method { get; set; }
    }

    public class TestFailedEvent : EventArgs
    {
        public MethodInfo Method { get; set; }
        public TestRunnerException Exception { get; set; }
    }

    public class TestRunner
    {
        protected List<TestSuite> Tests {  get; set; }
        protected int Failures { get; set; }
        protected List<TestSuite> FailedTestsSuites { get; set; }
        protected List<MethodInfo> FailedMethods { get; set; }
        protected int TestCases = 0;

        protected TestSuite LastTestSuite { get; set; }
        protected string LastErrorMessage { get; set; }

        public event EventHandler<TestEvent> TestStarted;
        public event EventHandler<TestEvent> TestFinished;
        public event EventHandler<TestMethodEvent> TestSucceeded;
        public event EventHandler<TestFailedEvent> TestFailed;

        public event EventHandler TestRunnerStarted;
        public event EventHandler TestRunnerFinished;

        public TestRunner()
        {
            FailedTestsSuites = new List<TestSuite>();
            FailedMethods = new List<MethodInfo>();

            Tests = new List<TestSuite>();
        }

        private void CollectTests()
        {
            Tests.Clear();

            var suites = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var suite in suites)
            {
                if (!suite.IsSubclassOf(typeof(TestSuite)))
                {
                    continue;
                }

                GD.Print($"Found suite {suite.Name}");
                var s = suite.GetConstructor(BindingFlags.Instance | BindingFlags.Public, Type.EmptyTypes);
                if(s == null)
                {
                    GD.Print("No apropriate constructor found");
                    continue;
                }
                var o = (TestSuite) s.Invoke(new object[] {});
                Tests.Add(o);
            }

            GD.Print($"Found {Tests.Count} test suites in the current assembly.");
        }

        private bool Setup(TestSuite s)
        {
            LastTestSuite = s;
            try
            {
                s.Setup();
                if(TestStarted != null)
                {
                    var args = new TestEvent();
                    args.TestSuite = s;
                    TestStarted.Invoke(this, args);
                }
                return true;
            }
            catch (Exception e) 
            {
                LastErrorMessage = e.Message;
                return false;
            }
        }

        private bool TearDown(TestSuite s)
        {
            LastTestSuite = s;
            try
            {
                s.TearDown();
                if (TestFinished != null)
                {
                    var args = new TestEvent();
                    args.TestSuite = s;
                    TestFinished.Invoke(this, args);
                }
                return true;
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message;
                return false;
            }
        }

        private void Test(TestSuite s)
        {
            LastTestSuite = s;
            var methods = s.GetTestMethods();

            foreach (var method in methods)
            {
                TestCases++;

                try
                {
                    method.Invoke(s, null);
                    if(TestSucceeded != null)
                    {
                        var args = new TestMethodEvent();
                        args.Method = method;
                        TestSucceeded.Invoke(this, args);
                    }
                }
                catch(Exception e) 
                {
                    LastErrorMessage = e.Message;
                    Failures++;
                    if(TestFailed != null)
                    {
                        var args = new TestFailedEvent();
                        args.Method = method;

                        if(e.InnerException.GetType() == typeof(TestRunnerException))
                        {
                            args.Exception = (TestRunnerException) e.InnerException;
                        }
                        else
                        {
                            args.Exception = new TestRunnerException("unknwon", "unknown", -1, e.Message);
                        }

                        TestFailed.Invoke(this, args);
                    }

                    FailedMethods.Add(method);
                    GD.PrintErr($"{e.InnerException.Message}");
                }
            }
        }

        private void HandleFailure(TestSuite s)
        {
            Failures++;
            FailedTestsSuites.Add(s);
            GD.PrintErr($"{s} could not be run.");
        }

        public void Run()
        {
            CollectTests();
            TestCases = 0;
            
            FailedTestsSuites.Clear();
            FailedMethods.Clear();

            Failures = 0;

            if(TestRunnerStarted != null)
            {
                TestRunnerStarted.Invoke(this, new EventArgs());
            }
            foreach(TestSuite s in Tests)
            {
                if(!Setup(s)) 
                {
                    HandleFailure(s);
                    continue; 
                }

                Test(s);

                if (!TearDown(s))
                {
                    HandleFailure(s);
                    continue;
                }
            }

            // Show results
            var percentage_failed = (float) Failures / (float)TestCases;

            GD.Print($"Ran {TestCases} tests with {percentage_failed} % failures.");
            if (TestRunnerFinished != null)
            {
                TestRunnerFinished.Invoke(this, new EventArgs());
            }
        }
    }
}
