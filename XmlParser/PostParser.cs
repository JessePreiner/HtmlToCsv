using HtmlAgilityPack;
using System.Collections.Generic;
using System;
using System.Linq;

namespace XmlParser
{
    public class PostParser
    {
        private IPostContentParser _parser;
        private Post[] posts;

        public PostParser(IPostContentParser contentParser, string rawContent)
        {
            _parser = contentParser;
            _parser.LoadRawContent(rawContent);
        }

        public List<Post> ToPosts(string[] rawPosts)
        {
            List<Post> result = new List<Post> { };

            foreach (var rawPost in rawPosts)
            {
                result.Add(ToPost(rawPost));
            }
            return result;
        }

        public Post ToPost(string rawContent)
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
                description = $"Description {guid}",
                original_id = guid,
                posted_date = DateTime.UtcNow
            };
        }

        string GetAuthor() {
            return _htmlDoc.DocumentNode
                           .SelectNodes("meta")
                           .SingleOrDefault(node => node
                                            .GetAttributeValue("name", "") == "author")
                           .GetAttributeValue("content", "");
        }

        public void LoadRawContent(string rawContent)
        {
            _htmlDoc.LoadHtml(rawContent);
        }
    }
}

