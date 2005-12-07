using System;
using MbUnit.Core.Framework;

namespace MbUnit.Framework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class RowAttribute : TestPatternAttribute
    {
        private Object[] row = null;
        private Type expectedException = null;
        public RowAttribute(
            Object a,
            Object b)
        {
            this.row = new Object[] { a, b };
        }
        public RowAttribute(
            Object a,
            Object b,
            Object c)
        {
            this.row = new Object[] { a, b, c };
        }
        public RowAttribute(
            Object a,
            Object b,
            Object c,
            Object d)
        {
            this.row = new Object[] { a, b, c, d };
        }
        public RowAttribute(
            Object a,
            Object b,
            Object c,
            Object d,
            Object e)
        {
            this.row = new Object[] { a, b, c, d, e };
        }
        public RowAttribute(
            Object a,
            Object b,
            Object c,
            Object d,
            Object e,
            Object f)
        {
            this.row = new Object[] { a, b, c, d, e, f };
        }

        public RowAttribute(
             Object a,
            Object b,
            Object c,
            Object d,
            Object e,
            Object f,
            Object h)
        {
            this.row = new Object[] { a, b, c, d, e, f, h };
        }

        public RowAttribute(
             Object a,
            Object b,
            Object c,
            Object d,
            Object e,
            Object f,
            Object h,
            Object i)
        {
            this.row = new Object[] { a, b, c, d, e, f, h, i };
        }

        public Type ExpectedException
        {
            get
            {
                return this.expectedException;
            }
            set
            {
                this.expectedException = value;
            }
        }

        public Object[] GetRow()
        {
            return this.row;
        }
    }
}
