using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace MbUnit.Framework
{
    public sealed class SerialAssert
    {
        #region Hiding constructor
        private SerialAssert()
        { }
        #endregion

        /// <summary>
        /// Verifies that the type is serializable with the XmlSerializer object.
        /// </summary>
        /// <param name="t">
        /// type to test.
        /// </param>
        public static void IsXmlSerializable(Type t)
        {
            Assert.IsNotNull(t);
            XmlSerializer ser = new XmlSerializer(t);
        }

        /// <summary>
        /// Serializes and deserialies to/from XML and checks that the results are the same.
        /// </summary>
        /// <param name="o">
        /// Object to test
        /// </param>
        /// <param name="comparer">
        /// Compare to check that object is the same
        /// </param>
        public static void TwoWaySerialization(Object o)
        {
            Assert.IsNotNull(o);
            XmlSerializer ser = new XmlSerializer(o.GetType());

            // create xml writer
            StringWriter sw = new StringWriter();
            XmlTextWriter xsw = new XmlTextWriter(sw);
            xsw.Formatting = Formatting.Indented;
            ser.Serialize(xsw, o);
            xsw.Flush();
            xsw.Close();

            // deserialize
            StringReader sr = new StringReader(sw.ToString());
            XmlTextReader xsr = new XmlTextReader(sw.ToString());
            Object oReturn = ser.Deserialize(xsr);

            // compare both
            Assert.AreEqual(o, oReturn);
        }

        public static string OneWaySerialization(Object o)
        {
            Assert.IsNotNull(o);
            XmlSerializer ser = new XmlSerializer(o.GetType());

            // create xml writer
            StringWriter sw = new StringWriter();
            XmlTextWriter xsw = new XmlTextWriter(sw);
            xsw.Formatting = Formatting.Indented;
            ser.Serialize(xsw, o);
            xsw.Flush();
            xsw.Close();

            // try parsing
            StringReader sr = new StringReader(sw.ToString());
            XmlTextReader wr = new XmlTextReader(sr);
            XmlDocument doc = new XmlDocument();
            doc.Load(wr);

            return sw.ToString();
        }
    }
}
