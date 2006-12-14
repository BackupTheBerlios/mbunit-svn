﻿using System;
using MbUnit.Core.Framework;
using MbUnit.Framework;
using MbUnit.Core.Remoting;
using MbUnit.Core.Filters;
using MbUnit.Core.Reports.Serialization;

namespace NUnit.Tests.Core
{
    [TestFixture]
    public class FixtureSetupTearDownTest
    {
        private static int setUpCount = 0;
        private static int tearDownCount = 0;
        private static int derivedSetUpCount=0;
        private static int derivedTearDownCount;

        [TestFixture]
        internal class SetUpAndTearDownFixture
        {
            [TestFixtureSetUp]
            public virtual void Init()
            {
                FixtureSetupTearDownTest.setUpCount++;
            }

            [TestFixtureTearDown]
            public virtual void Destroy()
            {
                FixtureSetupTearDownTest.tearDownCount++;
            }

            [Test]
            public void Success() { }

            [Test]
            public void EvenMoreSuccess() { }
        }

        internal class InheritSetUpAndTearDown : SetUpAndTearDownFixture
        {
            [Test]
            public void AnotherTest() { }

            [Test]
            public void YetAnotherTest() { }
        }

        internal class DefineInheritSetUpAndTearDown : SetUpAndTearDownFixture
        {
            [TestFixtureSetUp]
            public override void Init()
            {
                derivedSetUpCount++;
            }

            [TestFixtureTearDown]
            public override void Destroy()
            {
                derivedTearDownCount++;
            }

            [Test]
            public void AnotherTest() { }

            [Test]
            public void YetAnotherTest() { }
        }

        [TestFixture]
        internal class MisbehavingFixtureSetUp
        {
            public bool blowUp = true;

            [TestFixtureSetUp]
            public void willBlowUp()
            {
                if (blowUp)
                    throw new Exception("This was thrown from fixture setup");
            }

            [Test]
            public void nothingToTest()
            {}
        }

        [SetUp]
        public void SetUp()
        {
            setUpCount = 0;
            tearDownCount = 0;
            derivedSetUpCount=0;
            derivedTearDownCount=0;
        }

        protected ReportResult Run(Type fixtureType)
        {
            using (TypeTestDomain domain = new TypeTestDomain(fixtureType))
            {
                domain.Load();
                domain.TestEngine.RunPipes();
                ReportResult result = domain.TestEngine.Report.Result;

                return result;
            }
        }

        [Test]
        public void MakeSureSetUpAndTearDownAreCalled()
        {
            ReportResult result = Run(typeof(SetUpAndTearDownFixture));
            Assert.AreEqual(1, setUpCount);
            Assert.AreEqual(1, tearDownCount);
            Assert.AreEqual(2, result.Counter.RunCount);
            Assert.AreEqual(2, result.Counter.SuccessCount);
        }

        [Test]
        public void CheckInheritedSetUpAndTearDownAreCalled()
        {
            ReportResult result = Run(typeof(SetUpAndTearDownFixture));
            Assert.AreEqual(1, setUpCount);
            Assert.AreEqual(1, tearDownCount);
        }

        [Test]
        public void CheckInheritedSetUpAndTearDownAreNotCalled()
        {
            ReportResult result = Run(typeof(DefineInheritSetUpAndTearDown));
            Assert.AreEqual(0, setUpCount);
            Assert.AreEqual(0, tearDownCount);
            Assert.AreEqual(1, derivedSetUpCount);
            Assert.AreEqual(1, derivedTearDownCount);
        }


        [Test]
        public void HandleErrorInFixtureSetup()
        {
            ReportResult result = Run(typeof(MisbehavingFixtureSetUp));
        }
/*
        [Test]
        public void RerunFixtureAfterSetUpFixed()
        {
            MisbehavingFixtureSetUp testFixture = new MisbehavingFixtureSetUp();
            TestSuite suite = new TestSuite("ASuite");
            suite.Add(testFixture);
            TestSuiteResult result = (TestSuiteResult)suite.Run(NullListener.NULL);

            // should have one suite and one fixture
            ResultSummarizer summ = new ResultSummarizer(result);
            Assert.AreEqual(1, summ.ResultCount);
            Assert.AreEqual(0, summ.TestsNotRun);
            Assert.AreEqual(0, summ.SuitesNotRun);
            TestResult failedResult = ((TestResult)result.Results[0]);
            Assert.IsTrue(failedResult.Executed, "Suite should have executed");

            //fix the blow up in setup
            testFixture.blowUp = false;
            result = (TestSuiteResult)suite.Run(NullListener.NULL);

            // should have one suite and one fixture
            summ = new ResultSummarizer(result);
            Assert.AreEqual(1, summ.ResultCount);
            Assert.AreEqual(0, summ.TestsNotRun);
            Assert.AreEqual(0, summ.SuitesNotRun);
        }

        internal class IgnoreInFixtureSetUp
        {
            [TestFixtureSetUp]
            public void SetUpCallsIgnore()
            {
                Assert.Ignore("TestFixtureSetUp called Ignore");
            }

            [Test]
            public void nothingToTest()
            {
            }
        }

        [Test]
        public void HandleIgnoreInFixtureSetup()
        {
            IgnoreInFixtureSetUp testFixture = new IgnoreInFixtureSetUp();
            TestSuite suite = new TestSuite("ASuite");
            suite.Add(testFixture);
            TestSuiteResult result = (TestSuiteResult)suite.Run(NullListener.NULL);

            // should have one suite and one fixture
            ResultSummarizer summ = new ResultSummarizer(result);
            Assert.AreEqual(0, summ.ResultCount);
            Assert.AreEqual(1, summ.TestsNotRun);
            Assert.AreEqual(1, summ.SuitesNotRun);
            TestSuiteResult ignoredResult = ((TestSuiteResult)result.Results[0]);
            Assert.IsFalse(ignoredResult.Executed, "Suite should not have executed");
            Assert.AreEqual("TestFixtureSetUp called Ignore", ignoredResult.Message);
            Assert.IsNotNull(ignoredResult.StackTrace, "StackTrace should not be null");

            TestResult testResult = ((TestResult)ignoredResult.Results[0]);
            Assert.IsFalse(testResult.Executed, "Testcase should not have executed");
            Assert.AreEqual("TestFixtureSetUp called Ignore", testResult.Message);
        }

        internal class MisbehavingFixtureTearDown
        {
            public bool blowUp = true;

            [TestFixtureTearDown]
            public void willBlowUp()
            {
                if (blowUp)
                    throw new Exception("This was thrown from fixture teardown");
            }

            [Test]
            public void nothingToTest()
            {
            }
        }

        [Test]
        public void HandleErrorInFixtureTearDown()
        {
            MisbehavingFixtureTearDown testFixture = new MisbehavingFixtureTearDown();
            TestSuite suite = new TestSuite("ASuite");
            suite.Add(testFixture);
            TestSuiteResult result = (TestSuiteResult)suite.Run(NullListener.NULL);
            Assert.AreEqual(1, result.Results.Count);
            TestResult failedResult = ((TestResult)result.Results[0]);
            Assert.IsTrue(failedResult.Executed, "Suite should have executed");
            Assert.IsTrue(failedResult.IsFailure, "Suite should have failed");

            Assert.AreEqual("This was thrown from fixture teardown", failedResult.Message);
            Assert.IsNotNull(failedResult.StackTrace, "StackTrace should not be null");

            // should have one suite and one fixture
            ResultSummarizer summ = new ResultSummarizer(result);
            Assert.AreEqual(1, summ.ResultCount);
            Assert.AreEqual(0, summ.TestsNotRun);
            Assert.AreEqual(0, summ.SuitesNotRun);
        }

        [Test]
        public void RerunFixtureAfterTearDownFixed()
        {
            MisbehavingFixtureTearDown testFixture = new MisbehavingFixtureTearDown();
            TestSuite suite = new TestSuite("ASuite");
            suite.Add(testFixture);
            TestSuiteResult result = (TestSuiteResult)suite.Run(NullListener.NULL);
            Assert.AreEqual(1, result.Results.Count);

            // should have one suite and one fixture
            ResultSummarizer summ = new ResultSummarizer(result);
            Assert.AreEqual(1, summ.ResultCount);
            Assert.AreEqual(0, summ.TestsNotRun);
            Assert.AreEqual(0, summ.SuitesNotRun);

            testFixture.blowUp = false;
            result = (TestSuiteResult)suite.Run(NullListener.NULL);
            summ = new ResultSummarizer(result);
            Assert.AreEqual(1, summ.ResultCount);
            Assert.AreEqual(0, summ.TestsNotRun);
            Assert.AreEqual(0, summ.SuitesNotRun);
        }

        internal class SetUpAndTearDownWithTestInName
        {
            internal int setUpCount = 0;
            internal int tearDownCount = 0;

            [TestFixtureSetUp]
            public virtual void TestFixtureSetUp()
            {
                setUpCount++;
            }

            [TestFixtureTearDown]
            public virtual void TestFixtureTearDown()
            {
                tearDownCount++;
            }

            [Test]
            public void Success() { }

            [Test]
            public void EvenMoreSuccess() { }
        }

        [Test]
        public void HandleSetUpAndTearDownWithTestInName()
        {
            SetUpAndTearDownWithTestInName testFixture = new SetUpAndTearDownWithTestInName();
            TestSuite suite = new TestSuite("SetUpAndTearDownSuite");
            suite.Add(testFixture);
            suite.Run(NullListener.NULL);

            Assert.AreEqual(1, testFixture.setUpCount);
            Assert.AreEqual(1, testFixture.tearDownCount);
            Assert.AreEqual(2, suite.CountTestCases());
            Assert.AreEqual(2, ((TestSuite)suite.Tests[0]).Tests.Count);
        }

        [Test]
        public void RunningSingleMethodCallsSetUpAndTearDown()
        {
            SetUpAndTearDownFixture testFixture = new SetUpAndTearDownFixture();
            TestSuite suite = new TestSuite("SetUpAndTearDownSuite");
            suite.Add(testFixture);
            TestSuite fixtureSuite = (TestSuite)suite.Tests[0];
            NUnit.Core.TestCase testCase = (NUnit.Core.TestCase)fixtureSuite.Tests[0];

            suite.Run(NullListener.NULL);

            Assert.AreEqual(1, testFixture.setUpCount);
            Assert.AreEqual(1, testFixture.tearDownCount);
        }

        [Ignore("Do Not Run This")]
        internal class IgnoredFixture
        {
            public bool setupCalled = false;
            public bool teardownCalled = false;

            [TestFixtureSetUp]
            public virtual void ShouldNotRun()
            {
                setupCalled = true;
            }

            [TestFixtureTearDown]
            public virtual void NeitherShouldThis()
            {
                teardownCalled = true;
            }

            [Test]
            public void Success() { }

            [Test]
            public void EvenMoreSuccess() { }
        }

        [Test]
        public void IgnoredFixtureShouldNotCallFixtureSetUpOrTearDown()
        {
            IgnoredFixture testFixture = new IgnoredFixture();
            TestSuite suite = new TestSuite("IgnoredFixtureSuite");
            suite.Add(testFixture);
            TestSuite fixtureSuite = (TestSuite)suite.Tests[0];
            NUnit.Core.TestCase testCase = (NUnit.Core.TestCase)fixtureSuite.Tests[0];

            fixtureSuite.Run(NullListener.NULL);
            Assert.IsFalse(testFixture.setupCalled, "TestFixtureSetUp called running fixture");
            Assert.IsFalse(testFixture.teardownCalled, "TestFixtureTearDown called running fixture");

            suite.Run(NullListener.NULL);
            Assert.IsFalse(testFixture.setupCalled, "TestFixtureSetUp called running enclosing suite");
            Assert.IsFalse(testFixture.teardownCalled, "TestFixtureTearDown called running enclosing suite");

            testCase.Run(NullListener.NULL);
            Assert.IsFalse(testFixture.setupCalled, "TestFixtureSetUp called running a test case");
            Assert.IsFalse(testFixture.teardownCalled, "TestFixtureTearDown called running a test case");
        }

        internal class FixtureWithNoTests
        {
            internal bool setupCalled = false;
            internal bool teardownCalled = false;

            [TestFixtureSetUp]
            public virtual void Init()
            {
                setupCalled = true;
            }

            [TestFixtureTearDown]
            public virtual void Destroy()
            {
                teardownCalled = true;
            }
        }

        [Test]
        public void FixtureWithNoTestsShouldNotCallFixtureSetUpOrTearDown()
        {
            FixtureWithNoTests testFixture = new FixtureWithNoTests();
            TestSuite suite = new TestSuite("NoTestsFixtureSuite");
            suite.Add(testFixture);
            TestSuite fixtureSuite = (TestSuite)suite.Tests[0];

            fixtureSuite.Run(NullListener.NULL);
            Assert.IsFalse(testFixture.setupCalled, "TestFixtureSetUp called running fixture");
            Assert.IsFalse(testFixture.teardownCalled, "TestFixtureTearDown called running fixture");

            suite.Run(NullListener.NULL);
            Assert.IsFalse(testFixture.setupCalled, "TestFixtureSetUp called running enclosing suite");
            Assert.IsFalse(testFixture.teardownCalled, "TestFixtureTearDown called running enclosing suite");
        }
        */
    }
}
