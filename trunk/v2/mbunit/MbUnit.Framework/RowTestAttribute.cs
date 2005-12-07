using System;
using MbUnit.Core.Framework;

namespace MbUnit.Framework
{
	[AttributeUsage(AttributeTargets.Method,AllowMultiple=false,Inherited=true)]
	public sealed class RowTestAttribute : TestPatternAttribute
	{
        public RowTestAttribute() 
        { }
    }
}
