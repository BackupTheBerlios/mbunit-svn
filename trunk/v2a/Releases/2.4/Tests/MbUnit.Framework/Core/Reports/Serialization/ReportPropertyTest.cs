﻿#region Includes
using System;
using System.Collections;
using System.IO;
using MbUnit.Core.Framework;
using MbUnit.Framework;
#endregion

using MbUnit.Core.Reports.Serialization;

namespace MbUnit.Framework.Tests.Core.Reports.Serialization
{
    /// <summary>
    /// <see cref="TestFixture"/> for the <see cref="ReportProperty"/> class.
    /// </summary>
    [TestFixture]
    public class ReportPropertyTest
    {
        #region Tests
        [Test]
        public void Serialize()
        {
            SerialAssert.IsXmlSerializable(typeof(ReportProperty));
        }
        #endregion
    }
}

