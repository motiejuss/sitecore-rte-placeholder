using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sitecore.Data.Items;
using System.Web;
using Antlr4.StringTemplate;

namespace Editor
{
    public class EditorComponents
    {
        public static string RenderArticleBody(string articleBody)
        {
            string _content = articleBody;            
            string pattern = @"\[([^>]+)\](.*?)\[/\1\]";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(articleBody);
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
                    switch (groups[1].Value)
                    {
                        case "accordion":
                            _content = _RenderAccordion(_content, groups[0].Value, groups[2].Value);
                            break;
                        default:
                            break;
                    }
                }
            }
            return _content;
        }

        private static string _RenderAccordion(string content, string stringToReplace, string itemID)
        {
            Item accordionItem = Sitecore.Context.Database.GetItem(itemID);
            if (accordionItem != null && accordionItem.HasChildren)
            {
                var path = HttpContext.Current.Server.MapPath(@"\Editor\InsertAccordion\template.st");
                var file = new FileInfo(path);
                using (StreamReader reader = file.OpenText())
                {
                    Template template = new Template(reader.ReadToEnd(), '$', '$');
                    List<object> sectors = new List<object>();
                    foreach (Item item in accordionItem.Children)
                    {
                        if (item != null)
                        {
                            sectors.Add(new
                            {
                                IsOpen = item.Fields["IsOpen"].Value.ToString() == "1" ? true : false,
                                Title = item.Fields["Title"].Value.ToString(),
                                Text = item.Fields["Text"].Value.ToString(),
                                Id = item.ID.ToShortID().ToString()
                            });
                        }
                    }
                    template.Add("Headline", accordionItem.Fields["Headline"].Value.ToString());
                    template.Add("Sectors", sectors);
                    content = content.Replace(stringToReplace, template.Render());
                }
            }
            return content;
        }
    }
}