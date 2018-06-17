using HtmlAgilityPack;
using System;
using System.Linq;

namespace XmlParser
{
    public class HtmlPostContentParser : IPostContentParser
    {
        HtmlDocument _htmlDoc;

        public HtmlPostContentParser(HtmlDocument doc)
        {
            _htmlDoc = doc;
        }

        public void LoadRawContent(string rawContent)
        {
            _htmlDoc.LoadHtml(rawContent);
        }

        public Post GetPost()
        {
            string guid = Guid.NewGuid().ToString();
            return new Post
            {
                author = GetAuthor(),
                content = GetContent(),
                title = GetTitle(),
                description = GetDescription(),
                original_id = guid,
                posted_date = GetDate()
            };
        }

        string GetTitle()
        {
            var titleNode = _htmlDoc.DocumentNode.Descendants().SingleOrDefault(node => node.Name == "title");
            return titleNode!= null ? titleNode.InnerText : "";
        }

        DateTime GetDate()
        {
            var dateNode = _htmlDoc.DocumentNode.Descendants().
                                     SingleOrDefault(node => node.HasClass("post-date") && node.Name == "div");
            return dateNode != null ? DateTime.Parse(dateNode.InnerText) : new DateTime();
        }

        string GetContent() {
            var contentNode = _htmlDoc.DocumentNode.Descendants().SingleOrDefault(node => node.HasClass("post-body") && node.HasClass("section"));
            return contentNode != null ? contentNode.InnerHtml : "";
        }

        string GetAuthor()
        {
            return GetTagValue(htmlTag: "//meta", searchField: "name", searchValue: "author", returnProp: "content");
        }

        string GetDescription()
        {
            return GetTagValue(htmlTag: "//meta", searchField: "name", searchValue: "description", returnProp: "content");
        }

        string GetTagValue(string htmlTag, string searchField, string searchValue, string returnProp)
        {
            return _htmlDoc.DocumentNode
                           .SelectNodes(htmlTag)
                           .SingleOrDefault(node => node
                                            .GetAttributeValue(searchField, "") == searchValue)
                           .GetAttributeValue(returnProp, "");
        }
    }
}

