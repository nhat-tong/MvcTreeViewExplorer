#region using
using MvcTreeViewExplorer.Areas.Explorer.Bll;
using MvcTreeViewExplorer.Areas.Explorer.ViewModels;
using System.Web.Mvc;
#endregion

namespace MvcTreeViewExplorer.Areas.Explorer.Controllers
{
    public class TreeViewController : Controller
    {
        private ExplorerBll _explorerBll;

        public TreeViewController()
        {
            _explorerBll = new ExplorerBll();
        }

        // GET: Explorer/TreeView
        public ActionResult Index()
        {
            return View(new ExplorerViewModel {
                Root = _explorerBll.GetItemsByPath(@"D:\NHAT\ELMAH"),
                IsRootActive = true,
                Id = "#treeViewLeftCol"
            });
        }

        public ActionResult _ViewItemDetails(string path)
        {
            return PartialView(new ExplorerViewModel {
                Root = _explorerBll.GetItemsByPath(path),
                IsRootActive = false,
                Id = "#treeViewRightCol"
            });
        }
    }
}