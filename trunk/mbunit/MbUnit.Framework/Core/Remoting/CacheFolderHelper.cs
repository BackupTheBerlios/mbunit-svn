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

namespace MbUnit.Core.Remoting
{
	public sealed class CacheFolderHelper
	{
		private CacheFolderHelper()
		{}

		public static void DeleteDir(string path)
		{
			DirectoryInfo info = new DirectoryInfo(path);
			DeleteDir(info);
		}
		
		/// <summary>
		/// Helper method to delete the cache dir. This method deals 
		/// with a bug that occurs when pdb files are marked read-only.
		/// </summary>
		/// <param name="cacheDir"></param>
		public static void DeleteDir( DirectoryInfo cacheDir )
		{
			if(cacheDir.Exists)
			{
				foreach( DirectoryInfo dirInfo in cacheDir.GetDirectories() )
				{
					dirInfo.Attributes &= ~FileAttributes.ReadOnly;
					DeleteDir( dirInfo );
				}

				foreach( FileInfo fileInfo in cacheDir.GetFiles() )
				{
					fileInfo.Attributes &= ~FileAttributes.ReadOnly;
				}

				cacheDir.Delete(true);
			}
		}
	}
}
