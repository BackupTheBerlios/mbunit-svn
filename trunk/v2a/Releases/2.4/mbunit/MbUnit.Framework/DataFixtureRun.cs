using System;
using System.Reflection;
using System.Xml;
using System.Collections;

using MbUnit.Core;
using MbUnit.Core.Runs;
using MbUnit.Core.Invokers;
using MbUnit.Core.Framework;

namespace MbUnit.Framework
{

    using QuickGraph.Concepts.Serialization;

	internal sealed class DataFixtureRun : IRun
	{
		public string Name
		{
			get
			{
				return String.Format("DataFixture");
			}
		}

		public bool IsTest
		{
			get
			{
				return false;
			}
		}

		public void Reflect(RunInvokerTree tree, RunInvokerVertex parent, Type t)
		{
			// getting methods
			ICollection mis = TypeHelper.GetAttributedMethods(
				t,
				typeof(ForEachTestAttribute)
				);

			// first get the DataProviderFixtureDecorator...
			foreach(DataProviderFixtureDecoratorAttribute dp in 
				t.GetCustomAttributes(typeof(DataProviderFixtureDecoratorAttribute),true))
			{
				// for each node
				foreach(XmlNode node in dp.GetData())
				{
					// for each test method
					foreach(MethodInfo mi in mis)
					{
						// get attribute
						ForEachTestAttribute fe = 
							(ForEachTestAttribute)TypeHelper.GetFirstCustomAttribute(
								mi,typeof(ForEachTestAttribute));
						// select nodes
						foreach(XmlNode childNode in node.SelectNodes(fe.XPath))
						{
							// create invokers
							IRunInvoker invoker = new ForEachTestRunInvoker(this,mi,fe,childNode);
                            //decorage
                            invoker = DecoratorPatternAttribute.DecoreInvoker(mi, invoker);
                            // add invoker
                            tree.AddChild(parent,invoker);
						}
					}
				}
			}
		}
	}
}
