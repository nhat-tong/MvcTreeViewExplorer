using System;
using System.Collections.Generic;

namespace MvcTreeViewExplorer.Areas.Explorer.Models
{
    public class FileItem : IExplorerItem
    {
        public List<IExplorerItem> ChildItems { get; set; }

        public bool IsFolder { get { return false; } set { IsFolder = value; } }

        public DateTime? ModifiedDate { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }
    }
}