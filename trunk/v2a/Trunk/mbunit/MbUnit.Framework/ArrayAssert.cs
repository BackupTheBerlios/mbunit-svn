// MbUnit Test Framework
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
//		MbUnit HomePage: http://www.mbunit.org
//		Author: Jonathan de Halleux


using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

using MbUnit.Core.Exceptions;

namespace MbUnit.Framework
{
	/// <summary>
	/// Array Assertion class
	/// </summary>
	public sealed class ArrayAssert
	{
		/// <summary>
		/// A private constructor disallows any instances of this object. 
		/// </summary>
		private ArrayAssert()
		{}

		/// <summary>
		/// Verifies that both array have the same dimension and elements.
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="actual"></param>
		public static void AreEqual(bool[] expected, bool[] actual)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}

		public static void AreEqual(char[] expected, char[] actual)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}

		public static void AreEqual(byte[] expected, byte[] actual)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}
						
		public static void AreEqual(int[] expected, int[] actual)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}

		
		public static void AreEqual(long[] expected, long[] actual)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}

		public static void AreEqual(float[] expected, float[] actual, float delta)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i],delta);
			}
		}


		public static void AreEqual(double[] expected, double[] actual, double delta)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i],delta);
			}
		}	
		

		public static void AreEqual(object[] expected, object[] actual)
		{
			if(expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			Assert.AreEqual(expected.Rank,actual.Rank,"Rank are not equal");
			Assert.AreEqual(expected.Length,actual.Length);
			for(int i = 0;i<expected.Length;++i)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}		
	}
}
