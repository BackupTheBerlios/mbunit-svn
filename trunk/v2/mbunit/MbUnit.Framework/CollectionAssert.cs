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

// MbUnit project.
// http://www.mbunit.org

using MbUnit.Framework;
using System;
using System.Collections;

namespace MbUnit.Framework
{
	/// <summary>
	/// Assertion helper for the <see cref="ICollection"/> class.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class contains static helper methods to verify assertions on the
	/// <see cref="ICollection"/> class.
	/// </para>
	/// </remarks>
	public sealed class CollectionAssert
	{
		#region Private constructor
		private CollectionAssert()
		{}		
		#endregion
		/// <summary>
		/// Verifies that the property value <see cref="ICollection.Count"/>
		/// of <paramref name="expected"/> and <paramref name="actual"/> are equal.
		/// </summary>
		/// <param name="expected">
		/// Instance containing the expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreCountEqual(
			ICollection expected,
			ICollection actual
			)
		{
			if (expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			AreCountEqual(expected.Count,actual);
		}
		
		/// <summary>
		/// Verifies that the property value <see cref="ICollection.Count"/>
		/// of <paramref name="actual"/> is equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">
		/// Expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreCountEqual(
			System.Int32 expected,
			ICollection actual
			)
		{
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected,actual.Count,
						"Property Count not equal");
			
		}		
		/// <summary>
		/// Verifies that the property value <see cref="ICollection.SyncRoot"/>
		/// of <paramref name="expected"/> and <paramref name="actual"/> are equal.
		/// </summary>
		/// <param name="expected">
		/// Instance containing the expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreSyncRootEqual(
			ICollection expected,
			ICollection actual
			)
		{
			if (expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			AreSyncRootEqual(expected.SyncRoot,actual);
		}
		
		/// <summary>
		/// Verifies that the property value <see cref="ICollection.SyncRoot"/>
		/// of <paramref name="actual"/> is equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">
		/// Expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreSyncRootEqual(
			System.Object expected,
			ICollection actual
			)
		{
			if (expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected,actual.SyncRoot,
						"Property SyncRoot not equal");
			
		}		
		/// <summary>
		/// Verifies that the property value <see cref="ICollection.IsSynchronized"/>
		/// is true.
		/// </summary>
		/// <param name="actual">
		/// Instance containing the expected value.
		/// </param>
		public static void IsSynchronized(
			ICollection actual
			)
		{		
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.IsSynchronized,
						  "Property IsSynchronized is false");
		}

		/// <summary>
		/// Verifies that the property value <see cref="ICollection.IsSynchronized"/>
		/// is false.
		/// </summary>
		/// <param name="actual">
		/// Instance containing the expected value.
		/// </param>
		public static void IsNotynchronized(
			ICollection actual
			)
		{		
			Assert.IsNotNull(actual);
			Assert.IsFalse(actual.IsSynchronized,
						  "Property IsSynchronized is true");			
		}

		/// <summary>
		/// Verifies that the property value <see cref="ICollection.IsSynchronized"/>
		/// of <paramref name="expected"/> and <paramref name="actual"/> are equal.
		/// </summary>
		/// <param name="expected">
		/// Instance containing the expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreIsSynchronizedEqual(
			ICollection expected,
			ICollection actual
			)
		{
			if (expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			AreIsSynchronizedEqual(expected.IsSynchronized,actual);
		}
		
		/// <summary>
		/// Verifies that the property value <see cref="ICollection.IsSynchronized"/>
		/// of <paramref name="actual"/> is equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">
		/// Expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreIsSynchronizedEqual(
			System.Boolean expected,
			ICollection actual
			)
		{			
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected,actual.IsSynchronized,
						"Property IsSynchronized not equal");
			
		}		
		
		/// <summary>
		/// Verifies that <paramref name="expected"/> and <paramref name="actual"/>
		/// are equal collections. Element count and element wize equality is verified.
		/// </summary>
		/// <param name="expected">
		/// Expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreEqual(
			ICollection expected,
			ICollection actual
			)
		{
			if (expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			AreCountEqual(expected,actual);		
			AreElementsEqual(expected, actual);
		}

		/// <summary>
		/// Verifies that <paramref name="expected"/> and <paramref name="actual"/>
		/// are equal collections. Element count and element wize equality is verified.
		/// </summary>
		/// <param name="expected">
		/// Expected value.
		/// </param>
		/// <param name="actual">
		/// Instance containing the tested value.
		/// </param>
		public static void AreElementsEqual(
			IEnumerable expected,
			IEnumerable actual
			)
		{
			if (expected==null && actual==null)
				return;
			
			Assert.IsNotNull(expected);
			Assert.IsNotNull(actual);
			
			IEnumerator expectedEn = null;
			IEnumerator actualEn = null;
			try
			{
				expectedEn = expected.GetEnumerator();
				actualEn = actual.GetEnumerator();				
				
				bool exMoveNext;
				bool acMoveNext;
				do
				{
					exMoveNext = expectedEn.MoveNext();
					acMoveNext = actualEn.MoveNext();
					Assert.AreEqual(exMoveNext,acMoveNext,
					                "Collection do not have the same number of elements");
					
					if (exMoveNext)
					{
						Assert.AreEqual(expectedEn.Current,actualEn.Current,
						                "Element of the collection different");
					}
				} while(exMoveNext);
			}
			catch(Exception)
			{
				throw;
			}
			finally
			{
				IDisposable disp = expectedEn as IDisposable;
				if (disp!=null)
					disp.Dispose();
				disp = actualEn as IDisposable;
				if (disp!=null)
					disp.Dispose();
								
			}				
		}

		/// <summary>
		/// Verifies that the <see cref="ICollection.Count"/> property
		/// is synchronized with the number of iterated elements.
		/// </summary>
		/// <param name="col">
		/// Collection to test
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="col"/> is a null reference (Nothing in Visual Basic)
		/// </exception>
		public static void IsCountCorrect(ICollection col)
		{
			if (col==null)
				throw new ArgumentNullException("col");
			int i =0;
			foreach(Object o in col)
				++i;
			Assert.AreEqual(i,col.Count);
		}
	}
}
