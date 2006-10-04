using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using TestDriven.UnitTesting;
using TestDriven.UnitTesting.Compatibility;
using TestDriven.UnitTesting.Exceptions;

namespace MbUnit.Framework.Runners
{
    [CompatibleException("Adapdev.UnitTest.AssertionException", "adapdev.unittest")]
    public class AssertionException : AssertFailExceptionBase
    {
        // Methods
        public extern AssertionException(string message);
    }

    //[AttributeUsage(0x44, AllowMultiple = true, Inherited = true), CompatibleAttribute("adapdev.unittest.CategoryAttribute", "adapdev.unittest")]
    //public class CategoryAttribute : TestDecoratorAttributeBase
    //{
    //    // Methods
    //    public CategoryAttribute(string name);
    //    public override ITestCase Decorate(ITestCase testCase);

    //    // Properties
    //    public string Name { get; }

    //    // Fields
    //    [CompatibleProperty("Name")]
    //    private string name;
    //}

    [CompatibleAttribute("Adapdev.UnitTest.ExpectedExceptionAttribute", "adapdev.unittest")]
    public class ExpectedExceptionAttribute : TestDecoratorAttributeBase
    {
        // Methods
        public extern ExpectedExceptionAttribute(Type exceptionType);
        public extern ExpectedExceptionAttribute(Type exceptionType, string expectedMessage);
        public extern override ITestCase Decorate(ITestCase testCase);

        // Properties
        public extern Type ExceptionType { get; set; }
        public extern string ExpectedMessage { get; set; }

        // Fields
        [CompatibleProperty("ExceptionType")]
        private Type exceptionType;
        [CompatibleProperty("ExpectedMessage")]
        private string expectedMessage;

        // Nested Types
        private class ExpectedExceptionDecorator : DecoratorTestCaseBase
        {
            // Methods
            public extern ExpectedExceptionDecorator(ExpectedExceptionAttribute attribute, ITestCase testCase);
            public extern override void Run(object fixtureInstance);

            // Fields
            private ExpectedExceptionAttribute attribute;
        }
    }

    //[AttributeUsage(0x44, AllowMultiple = false, Inherited = true), CompatibleAttribute("adapdev.unittest.ExplicitAttribute", "adapdev.unittest")]
    //public class ExplicitAttribute : TestDecoratorAttributeBase
    //{
    //    // Methods
    //    public ExplicitAttribute();
    //    public override ITestCase Decorate(ITestCase testCase);

    //    // Nested Types
    //    private class ExplicitDecorator : DecoratorTestCaseBase
    //    {
    //        // Methods
    //        public ExplicitDecorator(ITestCase testCase);
    //        public override void Run(object fixtureInstance);

    //        // Properties
    //        public override bool IsExplicit { get; }
    //    }
    //}

    [CompatibleAttribute("Adapdev.UnitTest.IgnoreAttribute", "adapdev.unittest")]
    public class IgnoreAttribute : TestDecoratorAttributeBase
    {
        // Methods
        public extern IgnoreAttribute();
        public extern IgnoreAttribute(string reason);
        public extern override ITestCase Decorate(ITestCase testCase);

        // Properties
        public extern string Reason { get; }

        // Fields
        [CompatibleProperty("Reason")]
        private string reason;
    }

    [CompatibleException("Adapdev.UnitTest.IgnoreException", "adapdev.unittest")]
    public class IgnoreException : AssertIgnoreExceptionBase
    {
        // Methods
        public extern IgnoreException(string message);
    }

    internal class IgnoreTestCase : TestCaseBase
    {
        // Methods
        public extern IgnoreTestCase(string fixtureName, string name, string reason);
        public extern override void Run(object fixtureInstance);

        // Fields
        private string name;
        private string reason;
    }

    [CompatibleAttribute("Adapdev.UnitTest.SetUpAttribute", "adapdev.unittest"), AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
    public sealed class SetUpAttribute : Attribute
    {
        // Methods
        public extern SetUpAttribute();
    }

    internal class SetUpTearDownTestCaseDecorator : ITestCaseDecorator
    {
        // Methods
        public extern SetUpTearDownTestCaseDecorator(MethodInfo setUpMethod, MethodInfo tearDownMethod);
        public extern ITestCase Decorate(ITestCase testCase);

        // Fields
        private MethodInfo setUpMethod;
        private MethodInfo tearDownMethod;

        // Nested Types
        private class SetUpTearDownDecoratorTestCase : DecoratorTestCaseBase
        {
            // Methods
            public extern SetUpTearDownDecoratorTestCase(ITestCase testCase, MethodInfo setUpMethod, MethodInfo tearDownMethod);
            public extern override void Run(object fixtureInstance);

            // Fields
            private MethodInfo setUpMethod;
            private MethodInfo tearDownMethod;
        }
    }

    [CompatibleAttribute("Adapdev.UnitTest.TearDownAttribute", "adapdev.unittest"), AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
    public sealed class TearDownAttribute : Attribute
    {
        // Methods
        public extern TearDownAttribute();
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true), CompatibleAttribute("Adapdev.UnitTest.TestAttribute", "adapdev.unittest")]
    public sealed class TestAttribute : TestAttributeBase
    {
        // Methods
        public extern TestAttribute();
        public extern override ITestCase[] CreateTests(ITestFixture fixture, MethodInfo method);
    }

    internal sealed class TestFixture : TestFixtureBase
    {
        // Methods
        public extern TestFixture(Type fixtureType);
        private extern void addCategories();
        private extern void addFixtureSetUpTearDown(Type fixtureType);
        private extern void addIsExplicit(Type fixtureType);
        private extern void addSetUpTearDown(Type fixtureType);
        public extern override object CreateInstance();
        public extern override ITestCase[] CreateTestCases();

        // Properties
        public extern override bool IsExplicit { get; }

        // Fields
        private object fixtureObject;
        private bool isExplicit;
    }

    [CompatibleAttribute("Adapdev.UnitTest.TestFixtureAttribute", "adapdev.unittest")]
    public sealed class TestFixtureAttribute : TestFixtureAttributeBase
    {
        // Methods
        public extern TestFixtureAttribute();
        public extern override ITestFixture[] CreateFixtures(Type fixtureType);
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true), CompatibleAttribute(" Adapdev.UnitTest.TestFixtureSetUpAttribute", "adapdev.unittest")]
    public sealed class TestFixtureSetUpAttribute : Attribute, ITestCaseFactory
    {
        // Methods
        public extern TestFixtureSetUpAttribute();
        public extern ITestCase[] CreateTests(ITestFixture fixture, MethodInfo method);

        // Nested Types
        private class TestFixtureSetUpTestCase : MethodTestCase
        {
            // Methods
            public extern TestFixtureSetUpTestCase(string fixtureName, MethodInfo method, object fixtureObject);
            public extern override void Run(object fixture);

            // Fields
            private object fixtureObject;
        }
    }

    [CompatibleAttribute(" Adapdev.UnitTest.TestFixtureTearDownAttribute", "adapdev.unittest"), AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
    public sealed class TestFixtureTearDownAttribute : Attribute, ITestCaseFactory
    {
        // Methods
        public extern TestFixtureTearDownAttribute();
        public extern ITestCase[] CreateTests(ITestFixture fixture, MethodInfo method);

        // Nested Types
        private class TestFixtureTearDownTestCase : MethodTestCase
        {
            // Methods
            public extern TestFixtureTearDownTestCase(string fixtureName, MethodInfo method, object fixtureObject);
            public extern override void Run(object fixture);

            // Fields
            private object fixtureObject;
        }
    }

}
