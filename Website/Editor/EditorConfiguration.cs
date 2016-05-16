using Sitecore.Configuration;
using Sitecore.Xml;
using System.Xml;

namespace Editor
{
    public class EditorConfiguration : Sitecore.Shell.Controls.RichTextEditor.EditorConfiguration
    {
        public EditorConfiguration(Sitecore.Data.Items.Item profile) : base(profile)
        {
        }
        protected override void SetupStylesheets()
        {
            XmlNodeList configNodes = Factory.GetConfigNodes("clientstylesheets/htmleditor/style");
            foreach (XmlNode node in configNodes)
            {
                this.Editor.CssFiles.Add(XmlUtil.GetAttribute("src", node));
            }
            base.SetupStylesheets();
        }
    }
}