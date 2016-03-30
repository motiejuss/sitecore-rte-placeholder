using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor
{
    public class EditorConfiguration : Sitecore.Shell.Controls.RichTextEditor.EditorConfiguration
    {
        public EditorConfiguration(Sitecore.Data.Items.Item profile) : base(profile)
        {
        }
        protected override void SetupStylesheets()
        {
            this.Editor.CssFiles.Add("/Editor/normalize.css");
            this.Editor.CssFiles.Add("/Editor/InsertAccordion/editor-control.css");
            base.SetupStylesheets();
        }
    }
}