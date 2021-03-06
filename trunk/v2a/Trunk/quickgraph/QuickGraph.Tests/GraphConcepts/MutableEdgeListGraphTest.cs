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

namespace QuickGraph.UnitTests.GraphConcepts
{
	using MbUnit.Core.Exceptions;
	using MbUnit.Core.Framework;
	using MbUnit.Framework;

	using QuickGraph.UnitTests.Generators;
	using QuickGraph.Concepts;
	using QuickGraph.Concepts.Predicates;
	using QuickGraph.Concepts.Modifications;
	using QuickGraph.Exceptions;

	[TypeFixture(typeof(IMutableEdgeListGraph),"Fixture for the IMutableEdgeListGraph concept")]
	[ProviderFactory(typeof(AdjacencyGraphGenerator),typeof(IMutableEdgeListGraph))]
	[ProviderFactory(typeof(BidirectionalGraphGenerator),typeof(IMutableEdgeListGraph))]
	[ProviderFactory(typeof(CustomAdjacencyGraphGenerator),typeof(IMutableEdgeListGraph))]
	[Pelikhan]
	public class MutableEdgeListGraphTest
	{
		private Random rnd = new Random();
		
		public Random Rnd
		{
			get
			{
				return this.rnd;
			}
		}

		[Test]
		public void RemoveEdgeIf(IMutableEdgeListGraph g)
		{
			if (g.EdgesEmpty)
				return;

			IEdge e = RandomGraph.Edge(g,Rnd);

			g.RemoveEdgeIf(new DummyEdgeEqualPredicate(e,false));
			g.RemoveEdgeIf(new DummyEdgeEqualPredicate(e,true));
		}
	}
}
