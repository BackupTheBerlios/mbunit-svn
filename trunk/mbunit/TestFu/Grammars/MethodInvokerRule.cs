using System;

namespace TestFu.Grammars
{
	public delegate void MethodInvoker();
	/// <summary>
	/// A rule that executes a <see cref="MethodInvokerRule"/>.
	/// </summary>	
	public class MethodInvokerRule : RuleBase
	{
		private MethodInvoker methodInvoker;
		
		/// <summary>
		/// Creates a new instance around a <see cref="MethodInvokerRule"/>
		/// </summary>
		/// <param name="methodDelegate">
		/// <see cref="MethodInvokerRule"/> to attach.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="methodDelegate"/> is a null reference.
		/// </exception>
		public MethodInvokerRule(MethodInvoker methodInvoker)
			:base(true)
		{
			if (methodInvoker==null)
				throw new ArgumentNullException("methodInvoker");
			this.methodInvoker=methodInvoker;
			this.Name = methodInvoker.Method.Name;
		}
		
		/// <summary>
		/// Invokes the <see cref="MethodInvokerRule"/> instance.
		/// </summary>
		/// <param name="token">
		/// Autorization token
		/// </param>
		public override void Produce(IProductionToken token)
		{
			this.methodInvoker();
			this.OnAction();
		}
	}
}
