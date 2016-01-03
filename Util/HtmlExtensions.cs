using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MvcTreeViewExplorer.Util
{
    public static class HtmlExtensions
    {
        private const string JsViewDataName = "RenderJavaScript";

        #region Script
        /// <summary>
        /// Permet d'ajouter des scripts qui seront rendus via la méthode RenderScripts depuis une partial
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="templates"></param>
        /// <returns></returns>
        public static MvcHtmlString Script(this HtmlHelper htmlHelper, params Func<object, HelperResult>[] templates)
        {
            List<string> scriptList = htmlHelper.ViewContext.HttpContext.Items[JsViewDataName] as List<string>;
            if (scriptList != null)
            {
                scriptList.Add(templates[0](null).ToHtmlString());
            }
            else
            {
                scriptList = new List<string>();
                scriptList.Add(templates[0](null).ToHtmlString());
                htmlHelper.ViewContext.HttpContext.Items.Add(JsViewDataName, scriptList);
            }

            return MvcHtmlString.Empty;
        }
        #endregion

        #region RenderScripts
        /// <summary>
        /// Rendre les scripts ayant été ajoutés via la méthode Script ci dessus
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            StringBuilder result = new StringBuilder();

            List<string> scriptList = htmlHelper.ViewContext.HttpContext.Items[JsViewDataName] as List<string>;
            if (scriptList != null)
            {
                foreach (string script in scriptList)
                {
                    result.AppendLine(script);
                }
            }

            return MvcHtmlString.Create(result.ToString());
        }
        #endregion
    }
}