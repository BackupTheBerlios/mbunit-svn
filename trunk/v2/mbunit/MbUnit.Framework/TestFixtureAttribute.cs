using System;
using MbUnit.Core;
using MbUnit.Core.Framework;
using System.Diagnostics;

namespace MbUnit.Framework 
{
	using MbUnit.Core.Runs;
	using MbUnit.Core.Reflection;

	/// <summary>
	/// Simple Test Pattern fixture.
	/// </summary>
	/// <include file="MbUnit.Framework.doc.xml" path="doc/remarkss/remarks[@name='TestFixtureAttribute']"/>
	/// <include file="MbUnit.Framework.doc.xml" path="doc/examples/example[@name='GraphicsBitmap']"/>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    [RunFactory(typeof(TestRun))]
    [RunFactory(typeof(RowRun))]
    [RunFactory(typeof(CombinatorialRun))]
	public sealed class TestFixtureAttribute : TestFixturePatternAttribute 
	{
        private static volatile Object syncRoot = new Object();
        private static SequenceRun runs = null;
        private static ParallelRun testRuns = null;
        /// <summary>
		/// Default constructor
		/// </summary>
		/// <remarks>
		/// </remarks>
		public TestFixtureAttribute()
		:base()
		{}
		
		/// <summary>
		/// Constructor with a fixture description
		/// </summary>
		/// <param name="description">fixture description</param>
		/// <remarks>
		/// </remarks>
		public TestFixtureAttribute(string description)
		:base(description)
		{}
		
		/// <summary>
		/// Creates the execution logic
		/// </summary>
		/// <remarks>
		/// See summary.
		/// </remarks>
		/// <returns>A <see cref="IRun"/> instance that represent the type
		/// test logic.
		/// </returns>
		/// <include file="MbUnit.Framework.doc.xml" path="doc/examples/example[@name='GraphicsBitmap']"/>
		public override IRun GetRun()
		{
            if (runs != null)
                    return runs;
            lock (syncRoot)
            {
                CreateRun();
            }
            return runs;
        }

        public static ParallelRun TestRuns
        {
            get
            {
                lock (syncRoot)
                {
                    if (runs == null)
                        CreateRun();
                    return testRuns;
                }
            }
        }

        private static SequenceRun CreateRun()
        {
            runs = new SequenceRun();

            // setup
            OptionalMethodRun setup = new OptionalMethodRun(typeof(SetUpAttribute), false);
            runs.Runs.Add(setup);

            testRuns = new ParallelRun();
            runs.Runs.Add(testRuns);

            foreach (RunFactoryAttribute runFactory in
                typeof(TestFixtureAttribute).GetCustomAttributes(typeof(RunFactoryAttribute), true)
                )
            {
                IRun run = runFactory.CreateRun();
                testRuns.Runs.Add(run);
            }

            // tear down
            OptionalMethodRun tearDown = new OptionalMethodRun(typeof(TearDownAttribute), false);
            runs.Runs.Add(tearDown);

            return runs;
        }
    }
}
