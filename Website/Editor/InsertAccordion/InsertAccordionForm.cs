using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;

namespace Editor.InsertAccordion
{
    public class InsertAccordionForm : DialogForm
    {
        protected DataContext DialogFolderDataContext;
        protected TreeviewEx DialogContentItems;

        protected string Mode
        {
            get
            {
                string mode = StringUtil.GetString(base.ServerProperties["Mode"]);
                
                if (!string.IsNullOrEmpty(mode))
                {
                    return mode;
                }

                return "shell";
            }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                base.ServerProperties["Mode"] = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);

            if (!Context.ClientPage.IsEvent)
            {
                Inialize();
            }
        }

        private void Inialize()
        {
            SetMode();
            SetDialogFolderDataContextFromQueryString();

            string qs = WebUtil.GetQueryString();

            string selectedItemId = WebUtil.GetQueryString("selectedItemId");
            if (!string.IsNullOrEmpty(selectedItemId))
            {
                Item selectedItem = Client.ContentDatabase.GetItem(new ID(selectedItemId));
                if (selectedItem != null)
                {                    
                    DialogContentItems.SetSelectedItem(selectedItem);                    
                }
            }
        }

        private void SetMode()
        {
            Mode = WebUtil.GetQueryString("mo");
        }

        private void SetDialogFolderDataContextFromQueryString()
        {
            DialogFolderDataContext.GetFromQueryString();
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            string selectedItemID = GetSelectedItemIDAsString();

            if (string.IsNullOrEmpty(selectedItemID))
            {
                return;
            }

            if (GetSelectedItem().TemplateName != "AccordionBox")
            {
                selectedItemID = "";
            }

            string selectedItemPath = GetSelectedItemPath();
            string javascriptArguments = string.Format("{0}, {1}", EscapeJavascriptString(selectedItemID), EscapeJavascriptString(selectedItemPath));

            if (IsWebEditMode())
            {
                SheerResponse.SetDialogValue(javascriptArguments);
                base.OnOK(sender, args);
            }
            else
            {
                string closeJavascript = string.Format("scClose({0})", javascriptArguments);
                SheerResponse.Eval(closeJavascript);
            }
        }

        private string GetSelectedItemIDAsString()
        {
            ID selectedID = GetSelectedItemID();

            if (selectedID != ID.Null)
            {
                return selectedID.ToString();
            }

            return string.Empty;
        }

        private ID GetSelectedItemID()
        {
            Item selectedItem = GetSelectedItem();

            if (selectedItem != null)
            {
                return selectedItem.ID;
            }

            return ID.Null;
        }

        private string GetSelectedItemPath()
        {
            Item selectedItem = GetSelectedItem();

            if (selectedItem != null)
            {
                return selectedItem.Paths.FullPath;
            }

            return string.Empty;
        }

        private Item GetSelectedItem()
        {
            return DialogContentItems.GetSelectionItem();
        }

        private static string EscapeJavascriptString(string stringToEscape)
        {
            return StringUtil.EscapeJavascriptString(stringToEscape);
        }

        protected override void OnCancel(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            if (IsWebEditMode())
            {
                base.OnCancel(sender, args);
            }
            else
            {
                SheerResponse.Eval("scCancel()");
            }
        }

        private bool IsWebEditMode()
        {
            return string.Equals(Mode, "webedit", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}