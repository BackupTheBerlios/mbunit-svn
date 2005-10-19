// QuickGraph Library 
// 
// Copyright (c) 2004 Jonathan de Halleux
//
// This software is provided 'as-is', without any express or implied warranty. 
// 
// In no event will the authors be held liable for any damages arising from 
// the use of this software.
// Permission is granted to anyone to use this software for any purpose, 
// including commercial applications, and to alter it and redistribute it 
// freely, subject to the following restrictions:
//
//		1. The origin of this software must not be misrepresented; 
//		you must not claim that you wrote the original software. 
//		If you use this software in a product, an acknowledgment in the product 
//		documentation would be appreciated but is not required.
//
//		2. Altered source versions must be plainly marked as such, and must 
//		not be misrepresented as being the original software.
//
//		3. This notice may not be removed or altered from any source 
//		distribution.
//		
//		QuickGraph Library HomePage: http://mbunit.tigris.org
//		Author: Jonathan de Halleux

using System;
using System.IO;
using System.Reflection;

using TestDriven.Framework;

using MbUnit.Core;
using MbUnit.Core.Framework;
using MbUnit.Core.Invokers;
using MbUnit.Core.Reports.Serialization;
using MbUnit.Core.Remoting;
using MbUnit.Core.Filters;
using MbUnit.Core.Cons.CommandLine;
using MbUnit.Core.Reports;

namespace MbUnit.AddIn
{
	/// <summary>
	/// MbUnit test runner for the NUnitAddin object.
	/// </summary>
	[Serializable]
	public class MbUnitTestRunner : ITestRunner
	{
        private ITestListener testListener = null;
        private int assemblySetUpCount = 0;
        private int assemblyTearDownCount = 0;
        private int testFixtureSetUpCount = 0;
        private int testFixtureTearDownCount = 0;
        private int failureCount = 0;
        private int successCount = 0;
        private int ignoreCount = 0;
        private int testCount = 0;
        private int skipCount = 0;

        public MbUnitTestRunner()
		{}

        public TestRunResult RunAssembly(ITestListener testListener, Assembly assembly)
        {
            AnyFixtureFilter filter = new AnyFixtureFilter();
            return Run(testListener, assembly, filter);
        }

        public TestRunResult RunNamespace(ITestListener testListener, Assembly assembly, string ns)
        {
            NamespaceFixtureFilter filter = new NamespaceFixtureFilter(ns);
            return Run(testListener, assembly, filter);
        }

        public TestRunResult RunMember(ITestListener testListener, Assembly assembly, MemberInfo member)
        {
            Type type = member as Type;
            if (type != null)
            {
                TypeFixtureFilter filter = new TypeFixtureFilter(type.FullName);
                TypeFilterBase typeFilter = TypeFilters.Type(type.FullName);
                return Run(testListener, assembly, filter, new AnyRunPipeFilter(), typeFilter);
            }
            else
            {
                TypeFixtureFilter filter = new TypeFixtureFilter(member.DeclaringType.FullName);
                TypeFilterBase typeFilter = TypeFilters.Type(member.DeclaringType.FullName);
                ContainsMemberRunPipeFilter runPipeFilter = new ContainsMemberRunPipeFilter(member);

                return Run(testListener, assembly, filter, runPipeFilter, typeFilter);
            }
        }

        protected TestRunResult Run(
            ITestListener testListener, 
            Assembly assembly, 
            IFixtureFilter filter
            )
        {
            return Run(testListener,assembly,filter, new AnyRunPipeFilter(), TypeFilters.Any);
        }

        protected TestRunResult Run(
            ITestListener testListener, 
            Assembly assembly, 
            IFixtureFilter filter,
            RunPipeFilterBase runPipeFilter,
            TypeFilterBase typeFilter
            )
        {
            this.testListener = testListener;

            assemblySetUpCount = 0;
            assemblyTearDownCount = 0;
            testFixtureSetUpCount = 0;
            testFixtureTearDownCount = 0;
            failureCount = 0;
            successCount = 0;
            ignoreCount = 0;
            skipCount = 0;

            string assemblyPath = new Uri(assembly.CodeBase).LocalPath;
			testListener.WriteLine("Test Execution", Category.Info);
			testListener.WriteLine("Exploring " + assembly.FullName, Category.Info);
            testListener.WriteLine(String.Format("MbUnit {0} Addin", typeof(RunPipe).Assembly.GetName().Version),Category.Info);

            try
			{
                using (AssemblyTestDomain domain = new AssemblyTestDomain(assembly))
                {
                    domain.TypeFilter = typeFilter;
                    domain.Filter = filter;
                    domain.RunPipeFilter = runPipeFilter;
                    domain.Load();
                    // check found tests
                    testCount = domain.TestEngine.GetTestCount().RunCount;
                    if (testCount==0)
                    {
                        testListener.WriteLine("No tests found",Category.Info);
                        return TestRunResult.NoTests;
                    }

                    testListener.WriteLine(String.Format("Found {0} tests", testCount),Category.Info);
                    // add listeners
                    domain.TestEngine.FixtureRunner.AssemblySetUp+=new ReportSetUpAndTearDownEventHandler(TestEngine_AssemblySetUp);
                    domain.TestEngine.FixtureRunner.AssemblyTearDown += new ReportSetUpAndTearDownEventHandler(TestEngine_AssemblyTearDown);
                    domain.TestEngine.FixtureRunner.TestFixtureSetUp += new ReportSetUpAndTearDownEventHandler(TestEngine_TestFixtureSetUp);
                    domain.TestEngine.FixtureRunner.TestFixtureTearDown += new ReportSetUpAndTearDownEventHandler(TestEngine_TestFixtureTearDown);
                    domain.TestEngine.FixtureRunner.RunResult += new ReportRunEventHandler(TestEngine_RunResult);

                    try
                    {
                        domain.TestEngine.RunPipes();
                    }
                    finally
                    {
                        if (domain.TestEngine != null)
                        {
                            domain.TestEngine.FixtureRunner.AssemblySetUp -= new ReportSetUpAndTearDownEventHandler(TestEngine_AssemblySetUp);
                            domain.TestEngine.FixtureRunner.AssemblyTearDown -= new ReportSetUpAndTearDownEventHandler(TestEngine_AssemblyTearDown);
                            domain.TestEngine.FixtureRunner.TestFixtureSetUp -= new ReportSetUpAndTearDownEventHandler(TestEngine_TestFixtureSetUp);
                            domain.TestEngine.FixtureRunner.TestFixtureTearDown -= new ReportSetUpAndTearDownEventHandler(TestEngine_TestFixtureTearDown);
                            domain.TestEngine.FixtureRunner.RunResult -= new ReportRunEventHandler(TestEngine_RunResult);
                        }
                    }

                    testListener.WriteLine("[reports] generating HTML report",Category.Info);
                    this.GenerateReports(testListener, assembly, domain.TestEngine.Report.Result);

                    if (domain.TestEngine.Report.Result.Counter.FailureCount > 0)
                    {
                        return TestRunResult.Failure;
                    }
                    else if (domain.TestEngine.Report.Result.Counter.FailureCount > 0)
                    {
                        return TestRunResult.Success;
                    }
                    else
                    {
                        return TestRunResult.NoTests;
                    }
                }
            }
			catch(Exception ex)
			{
                testListener.WriteLine("[critical-failure]",Category.Error);
                testListener.WriteLine(ex.ToString(),Category.Error);
                throw new Exception("Test execution failed", ex);
            }
		}

        private void GenerateReports(ITestListener testListener, Assembly assembly,ReportResult result)
        {
            try
            {
                string outputPath = Path.GetDirectoryName(assembly.Location);
                string nameFormat = assembly.GetName().Name + ".Tests";

                string file = HtmlReport.RenderToHtml(result, outputPath, nameFormat);

                Uri uri = new Uri("file:" + Path.GetFullPath(file));
                testListener.TestResultsUrl(uri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                testListener.WriteLine("failed to create reports",Category.Error);
                testListener.WriteLine(ex.ToString(), Category.Error);
            }
        }

        void RenderSetUpOrTearDownFailure(string context, ReportSetUpAndTearDown setup)
        {
            TestResultSummary summary = new TestResultSummary();
            summary.IsExecuted = true;
            summary.IsFailure = setup.Result == ReportRunResult.Failure;
            summary.IsSuccess = setup.Result == ReportRunResult.Success;
            summary.TotalTests = this.testCount;
            summary.Name = String.Format("{0} {1}",context, setup.Name);
            summary.TimeSpan = new TimeSpan(0, 0, 0, 0, (int)(setup.Duration * 1000));
            if (setup.Exception != null)
            {
                summary.Message = setup.Exception.Message;

                StringWriter sw = new StringWriter();
                ReportException ex = setup.Exception;
                summary.StackTrace = ex.ToString();
            }

            this.testListener.TestFinished(summary);
        }

        void TestEngine_AssemblySetUp(object sender, ReportSetUpAndTearDownEventArgs e)
        {
            this.assemblySetUpCount++;
            if (e.SetUpAndTearDown.Result != ReportRunResult.Success)
                RenderSetUpOrTearDownFailure("[assembly-setup]", e.SetUpAndTearDown);
            else
                testListener.WriteLine("[assembly-setup] success", Category.Info);
        }

        void TestEngine_AssemblyTearDown(object sender, ReportSetUpAndTearDownEventArgs e)
        {
            this.assemblyTearDownCount++;
            if (e.SetUpAndTearDown.Result != ReportRunResult.Success)
                RenderSetUpOrTearDownFailure("[assembly-teardown]", e.SetUpAndTearDown);
            else
                testListener.WriteLine("[assembly-teardown] success", Category.Info);
        }

        void TestEngine_TestFixtureSetUp(object sender, ReportSetUpAndTearDownEventArgs e)
        {
            this.testFixtureSetUpCount++;
            if (e.SetUpAndTearDown.Result != ReportRunResult.Success)
                RenderSetUpOrTearDownFailure("[fixture-setup]", e.SetUpAndTearDown);
            else
                testListener.WriteLine("[fixture-setup] success", Category.Info);
        }

        void TestEngine_TestFixtureTearDown(object sender, ReportSetUpAndTearDownEventArgs e)
        {
            this.testFixtureTearDownCount++;
            if (e.SetUpAndTearDown.Result != ReportRunResult.Success)
                RenderSetUpOrTearDownFailure("[fixture-teardown]", e.SetUpAndTearDown);
            else
                testListener.WriteLine("[fixture-teardown] success", Category.Info);
        }

        void TestEngineSuccess(ReportRun run)
        {
            this.successCount++;

            TestResultSummary summary = new TestResultSummary();
            summary.IsExecuted = true;
            summary.IsFailure = false;
            summary.IsSuccess = true;
            summary.TotalTests = this.testCount;
            summary.Name = run.Name;
            summary.TimeSpan = new TimeSpan(0, 0, 0, 0, (int)(run.Duration * 1000));

            testListener.WriteLine(String.Format("[success] {0}", run.Name), Category.Info);
            this.testListener.TestFinished(summary);
        }

        void TestEngineFailure(ReportRun run)
        {
            this.failureCount++;

            TestResultSummary summary = new TestResultSummary();
            summary.IsExecuted = true;
            summary.IsFailure = true;
            summary.IsSuccess = false;
            summary.TotalTests = this.testCount;
            summary.Name = run.Name;
            summary.TimeSpan = new TimeSpan(0, 0, 0, 0, (int)(run.Duration * 1000));

            if (run.Exception != null)
            {
                ReportException ex = run.Exception;

                summary.Message = run.Exception.Message;
                summary.StackTrace = ex.ToString();
            }

            testListener.WriteLine(String.Format("[failure] {0}", run.Name), Category.Info);
            this.testListener.TestFinished(summary);
        }

        void TestEngineSkip(ReportRun run)
        {
            this.skipCount++;

            TestResultSummary summary = new TestResultSummary();
            summary.IsExecuted = true;
            summary.IsFailure = false;
            summary.IsSuccess = false;
            summary.TotalTests = this.testCount;
            summary.Name = run.Name;
            summary.TimeSpan = new TimeSpan(0, 0, 0, 0, 0);

            if (run.Exception != null)
            {
                ReportException ex = run.Exception;

                summary.Message = run.Exception.Message;
                summary.StackTrace = ex.ToString();
            }

            testListener.WriteLine(String.Format("[skipped] {0}", run.Name), Category.Info);
            this.testListener.TestFinished(summary);
        }

        void TestEngineIgnore(ReportRun run)
        {
            this.ignoreCount++;

            TestResultSummary summary = new TestResultSummary();
            summary.IsExecuted = false;
            summary.IsFailure = false;
            summary.IsSuccess = false;
            summary.TotalTests = this.testCount;
            summary.Name = run.Name;
            summary.TimeSpan = new TimeSpan(0, 0, 0, 0, (int)(run.Duration * 1000));

            testListener.WriteLine(String.Format("[ignored] {0}", run.Name), Category.Info);
            this.testListener.TestFinished(summary);
        }

        void TestEngine_RunResult(object sender, ReportRunEventArgs e)
        {
            switch (e.Run.Result)
            {
                case ReportRunResult.Success:
                    TestEngineSuccess(e.Run);
                    break;
                case ReportRunResult.Failure:
                    TestEngineFailure(e.Run);
                    break;
                case ReportRunResult.Ignore:
                    TestEngineIgnore(e.Run);
                    break;
                case ReportRunResult.Skip:
                    TestEngineSkip(e.Run);
                    break;
            }
        }
    }
}
