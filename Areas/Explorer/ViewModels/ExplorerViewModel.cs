using MvcTreeViewExplorer.Areas.Explorer.Models;

namespace MvcTreeViewExplorer.Areas.Explorer.ViewModels
{
    public class ExplorerViewModel
    {
        public IExplorerItem Root { get; set; }

        public bool IsRootActive { get; set; }

        public string Id { get; set; }
    }
}