using System.IO;
using System.Text.RegularExpressions;
using Sitecore.Data.Items;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;

namespace Editor
{
    public class EditorComponents
    {        
        public static string ProcessRichTextEditorField(string content, string viewPath)
        {
            string _content = content;
            string pattern = @"\[([^>]+)\](.*?)\[/\1\]";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(_content);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    /*
                     * groups[0].Value [accordion]{9613DF72-B222-4BD4-9F2E-A1844A49A80D}[/accordion]
                     * groups[1].Value accordion
                     * groups[2].Value {9613DF72-B222-4BD4-9F2E-A1844A49A80D}
                     * */
                    GroupCollection groups = match.Groups;
                    _content = _RenderComponent(_content, groups[0].Value, groups[2].Value, groups[1].Value, viewPath);
                }
            }
            return _content;
        }

        private static string _RenderComponent(string content, string stringToReplace, string itemID, string componentName, string viewPath)
        {
            string componentView = _RenderViewToString(viewPath, componentName, itemID);
            content = content.Replace(stringToReplace, componentView);
            return content;
        }

        private static string _RenderViewToString(string viewName, string componentName, string itemID)
        {
            ControllerContext controllerContext = new ControllerContext(PageContext.Current.RequestContext, new EditorComponentsController());            

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controllerContext.RouteData.GetRequiredString("action");
            }

            Rendering r = new Rendering();
            RenderingModel vm = new RenderingModel();            
            RenderingParameters rp = new RenderingParameters(string.Format("ComponentName={0}", componentName));

            r.DataSource = itemID;
            r.Parameters = rp;            
            vm.Rendering = r;

            controllerContext.Controller.ViewData.Model = vm;

            using (var stringWriter = new StringWriter())
            {
                var viewEngineResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);                
                
                var viewContext = new ViewContext(
                    controllerContext,
                    viewEngineResult.View,
                    controllerContext.Controller.ViewData,
                    controllerContext.Controller.TempData,
                    stringWriter);

                viewEngineResult.View.Render(viewContext, stringWriter);

                return stringWriter.GetStringBuilder().ToString();
            }
        }
        
    }

    public class EditorComponentsController : Controller
    {

    }
}