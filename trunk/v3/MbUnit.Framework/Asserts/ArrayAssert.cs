using System;
using System.Collections.Generic;
using System.Text;

namespace MbUnit.Framework
{
    public sealed class ArrayAssert
    {
        /// <summary>
        /// A private constructor disallows any instances of this object. 
        /// </summary>
        private ArrayAssert()
        { }

        /// <summary>
        /// Verifies that both array have the same dimension and elements.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreEqual(bool[] expected, bool[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        public static void AreEqual(char[] expected, char[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        public static void AreEqual(byte[] expected, byte[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        public static void AreEqual(int[] expected, int[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }


        public static void AreEqual(long[] expected, long[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        public static void AreEqual(float[] expected, float[] actual, float delta)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i], delta);
            }
        }


        public static void AreEqual(double[] expected, double[] actual, double delta)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i], delta);
            }
        }


        public static void AreEqual(object[] expected, object[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Rank, actual.Rank, "Rank are not equal");
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }	
    }
}
