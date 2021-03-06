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

namespace QuickGraph.UnitTests.Generators
{
	using QuickGraph.Concepts;
	using QuickGraph.Concepts.Modifications;
	using QuickGraph.Concepts.Traversals;
	using QuickGraph.Representations;
	using QuickGraph.Providers;

	/// <summary>
	/// Description r�sum�e de AdjacencyGraphGenerator.
	/// </summary>
	public class AdjacencyGraphGenerator
	{
		public AdjacencyGraph EmptyGraph
		{
			get
			{
				return new AdjacencyGraph(true);
			}
		}

		public AdjacencyGraph Tree
		{
			get
			{
				AdjacencyGraph g =  new AdjacencyGraph(true);
				IVertex u = g.AddVertex();
				IVertex v = g.AddVertex();
				IVertex w = g.AddVertex();
				g.AddEdge(u,v);
				g.AddEdge(v,w);
				g.AddEdge(u,w);

				return g;
			}
		}

		public AdjacencyGraph CyclicGraph
		{
			get
			{
				AdjacencyGraph g =  new AdjacencyGraph(true);
				IVertex u = g.AddVertex();
				IVertex v = g.AddVertex();
				IVertex w = g.AddVertex();
				g.AddEdge(u,v);
				g.AddEdge(v,w);
				g.AddEdge(w,u);

				return g;
			}
		}
	}
}
