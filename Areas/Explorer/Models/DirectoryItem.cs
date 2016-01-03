using System;
using System.Collections.Generic;

namespace MvcTreeViewExplorer.Areas.Explorer.Models
{
    public class DirectoryItem : IExplorerItem
    {
        public bool IsFolder { get { return true; } set { IsFolder = value; } }

        public DateTime? ModifiedDate { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public List<IExplorerItem> ChildItems { get; set; }

        public string Path { get; set; }
    }
}