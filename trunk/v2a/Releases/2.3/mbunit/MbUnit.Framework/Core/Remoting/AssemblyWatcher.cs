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

#region Copyright (c) 2002-2003, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig
#endregion

using System;
using System.IO;
using System.Text;
using System.Timers;
using System.Collections;

namespace MbUnit.Core.Remoting
{
	/// <summary>
	/// AssemblyWatcher keeps track of one or more assemblies to 
	/// see if they have changed. It incorporates a delayed notification
	/// and uses a standard event to notify any interested parties
	/// about the change. The path to the assembly is provided as
	/// an argument to the event handler so that one routine can
	/// be used to handle events from multiple watchers.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Code takened from NUnit.
	/// <code>
	/// /************************************************************************************
	/// '
	/// ' Copyright  2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
	/// ' Copyright  2000-2002 Philip A. Craig
	/// '
	/// ' This software is provided 'as-is', without any express or implied warranty. In no 
	/// ' event will the authors be held liable for any damages arising from the use of this 
	/// ' software.
	/// ' 
	/// ' Permission is granted to anyone to use this software for any purpose, including 
	/// ' commercial applications, and to alter it and redistribute it freely, subject to the 
	/// ' following restrictions:
	/// '
	/// ' 1. The origin of this software must not be misrepresented; you must not claim that 
	/// ' you wrote the original software. If you use this software in a product, an 
	/// ' acknowledgment (see the following) in the product documentation is required.
	/// '
	/// ' Portions Copyright  2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
	/// ' or Copyright  2000-2002 Philip A. Craig
	/// '
	/// ' 2. Altered source versions must be plainly marked as such, and must not be 
	/// ' misrepresented as being the original software.
	/// '
	/// ' 3. This notice may not be removed or altered from any source distribution.
	/// '
	/// '***********************************************************************************/
	/// </code>
	/// </para>
	/// </remarks>
	public class AssemblyWatcher
	{
		private ArrayList fileWatchers = new ArrayList();

		protected System.Timers.Timer timer;
		protected string changedAssemblyPath; 

		public delegate void AssemblyChangedHandler(String fullPath);
		public event AssemblyChangedHandler AssemblyChangedEvent;

		public AssemblyWatcher()
		{
			timer = new System.Timers.Timer(1000);
			timer.AutoReset=false;
			timer.Enabled=false;
			timer.Elapsed+=new ElapsedEventHandler(OnTimer);
		}

		public void Add(string filePath)
		{
			FileWatcher fw = new FileWatcher(new FileInfo( filePath ));
				
			fw.Watcher.Path = fw.Info.DirectoryName;
			fw.Watcher.Filter = fw.Info.Name;
			fw.Watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite;
			fw.Watcher.Changed+=new FileSystemEventHandler(OnChanged);
			fw.Watcher.EnableRaisingEvents = false;

			this.fileWatchers.Add(fw);
		}

		public void Remove(string filePath)
		{
			foreach(FileWatcher fw in this.fileWatchers)
			{
				fw.Watcher.Changed-=new FileSystemEventHandler(OnChanged);
				this.fileWatchers.Remove(fw);
			}
		}

		public void Clear()
		{
			this.Stop();
			foreach(FileWatcher fw in this.fileWatchers)
			{
				fw.Watcher.Changed-=new FileSystemEventHandler(OnChanged);
			}
			this.fileWatchers.Clear();
		}

		public FileInfo GetFileInfo( int index )
		{
			return ((FileWatcher)fileWatchers[index]).Info;
		}

		public void Start()
		{
			EnableWatchers( true );
		}

		public void Stop()
		{
			EnableWatchers( false );
		}

		private void EnableWatchers( bool enable )
		{
			foreach( FileWatcher watcher in this.fileWatchers )
				watcher.Watcher.EnableRaisingEvents = enable;
		}

		protected void OnTimer(Object source, ElapsedEventArgs e)
		{
			lock(this)
			{
				PublishEvent();
				timer.Enabled=false;
			}
		}
		
		protected void OnChanged(object source, FileSystemEventArgs e)
		{
			changedAssemblyPath = e.FullPath;
			if ( timer != null )
			{
				lock(this)
				{
					if(!timer.Enabled)
						timer.Enabled=true;
					timer.Start();
				}
			}
			else
			{
				PublishEvent();
			}
		}
	
		protected void PublishEvent()
		{
			if ( AssemblyChangedEvent != null )
				AssemblyChangedEvent( changedAssemblyPath );
		}

		public class FileWatcher
		{
			private FileSystemWatcher watcher = new FileSystemWatcher();
			private FileInfo info;

			public FileWatcher(FileInfo info)
			{
				this.info=info;
			}

			public FileSystemWatcher Watcher
			{
				get
				{
					return this.watcher;
				}
			}

			public FileInfo Info
			{
				get
				{
					return this.info;
				}
			}
		}
	}
}