using System;
using System.Collections.Generic;

namespace MvcTreeViewExplorer.Areas.Explorer.Models
{
    public interface IExplorerItem
    {
        string Path { get; set; }

        string Name { get; set; }

        DateTime? ModifiedDate { get; set; }

        string Type { get; set; }

        List<IExplorerItem> ChildItems { get; set; }

        bool IsFolder { get; set; }
    }
}