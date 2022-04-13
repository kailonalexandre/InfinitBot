using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace InfinitBott
{
    public class Watcher
    {
        public string Directory { get; set; }
        public string Filter { get; set; }
        public CommandContext Commands { get; set; }
       

        private Delegate _changeMethod;

        public Delegate ChangeMethod
        {
            get { return _changeMethod; }
            set { _changeMethod = value; }
        }
        FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
       
        public Watcher(string directory, string filter, Delegate invokeMethod)
        {
            this._changeMethod = invokeMethod;
            this.Directory = directory;
            this.Filter = filter;
        }
        public void StartWatch()
        {
            fileSystemWatcher.Filter = this.Filter;
            fileSystemWatcher.Path = this.Directory;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Changed +=
                 new FileSystemEventHandler(fileSystemWatcher_Changed);

            fileSystemWatcher.Created +=
                 new FileSystemEventHandler(fileSystemWatcher_Changed);
    
        }
       
     

        void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            
            if (_changeMethod != null)
            {
                _changeMethod.DynamicInvoke(e);
            }
        }
    }
}
