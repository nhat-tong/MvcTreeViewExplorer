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
                Id = "treeViewLeftCol"
            });
        }

        /// <summary>
        /// View item details (List)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult _ViewListItemDetails(string path)
        {
            return PartialView(new ExplorerViewModel {
                Root = _explorerBll.GetItemsByPath(path),
                IsRootActive = false,
                Id = "treeViewRightCol"
            });
        }

        /// <summary>
        /// View item details (Tree)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult _ViewTreeItemDetails(string path)
        {
            return PartialView(new ExplorerViewModel
            {
                Root = _explorerBll.GetItemsByPath(path),
                IsRootActive = false,
                Id = "treeViewRightCol"
            });
        }
    }
}