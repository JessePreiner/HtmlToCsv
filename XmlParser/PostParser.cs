using HtmlAgilityPack;
using System;
using System.Linq;

namespace XmlParser
{
    public class PostParser
    {
        private IPostContentParser _parser;
        private Post[] posts;

        public PostParser(IPostContentParser contentParser, string rawPost)
        {
            _parser = contentParser;
            _parser.LoadRawContent(rawPost);
        }


        public Post ToPost()
        {
            return _parser.GetPost();
        }
    }

    public interface IPostContentParser
    {
        Post GetPost();
        void LoadRawContent(string rawContent);
    }

    public class HtmlPostContentParser : IPostContentParser
    {
        HtmlDocument _htmlDoc;

        public HtmlPostContentParser(HtmlDocument doc)
        {
            _htmlDoc = doc;
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

        string GetAuthor() {
            return GetTagValue(htmlTag:"//meta", searchField:"name", searchValue:"author", returnProp:"content");
        }

        string GetDescription()
        {
            return GetTagValue(htmlTag: "//meta", searchField: "name", searchValue: "description", returnProp: "content");
        }

        string GetTagValue(string htmlTag, string searchField, string searchValue, string returnProp) {
            return _htmlDoc.DocumentNode
                           .SelectNodes(htmlTag)
                           .SingleOrDefault(node => node
                                            .GetAttributeValue(searchField, "") == searchValue)
                           .GetAttributeValue(returnProp, "");
        }

        public void LoadRawContent(string rawContent)
        {
            _htmlDoc.LoadHtml(rawContent);
        }
    }
}

