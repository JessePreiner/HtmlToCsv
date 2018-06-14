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
                description = GetDescription(),
                original_id = guid,
                posted_date = DateTime.UtcNow
            };
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

