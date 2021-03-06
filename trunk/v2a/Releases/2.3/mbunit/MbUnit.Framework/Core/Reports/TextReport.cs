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
using System.IO;
using MbUnit.Core.Reports.Serialization;

namespace MbUnit.Core.Reports
{
	using MbUnit.Core.Reports.Serialization;
	/// <summary>
	/// Reports MbUnit result in text format
	/// </summary>
	public sealed class TextReport : XslTransformReport
	{
		public TextReport()
		{
		}

		protected override string DefaultExtension
		{
			get { return ".txt"; }
		}

		public override void Render(ReportResult result, TextWriter writer)
		{
			base.Transform = ResourceHelper.ReportTextTransform;
			base.Render(result, writer);
		}

		public static string RenderToText(ReportResult result)
		{
			TextReport textReport = new TextReport();
			return textReport.Render(result);
		}

		public static string RenderToText(ReportResult result, string outputPath, string nameFormat)
		{
			TextReport textReport = new TextReport();
			return textReport.Render(result, outputPath, nameFormat);
		}


	}
}
