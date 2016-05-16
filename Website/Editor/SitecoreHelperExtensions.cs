using Sitecore.Mvc.Helpers;
using Sitecore;
using System.Web;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data.Items;
using System.Xml;
using Sitecore.Configuration;
using System.Text;
using Sitecore.Xml;

namespace Editor
{
    public static class SitecoreHelperExtensions
    {
        public static HtmlString RichTextEditorField(this SitecoreHelper sitecoreHelper, string fieldName, string viewPath, Item item = null)
        {
            HtmlString output;
            item = item == null ? sitecoreHelper.CurrentItem : item;            
            StringBuilder fieldValue = new StringBuilder(FieldRenderer.Render(item, fieldName));
            if (Context.PageMode.IsExperienceEditorEditing)
            {
                XmlNodeList configNodes = Factory.GetConfigNodes("clientstylesheets/experienceeditor/style");                             
                foreach (XmlNode node in configNodes)
                {
                    fieldValue.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" />", XmlUtil.GetAttribute("src", node));
                }
                output = new HtmlString(fieldValue.ToString());
            }
            else
            {
                output = new HtmlString(EditorComponents.ProcessRichTextEditorField(fieldValue.ToString(), viewPath));
            }
            return output;
        }
    }
}