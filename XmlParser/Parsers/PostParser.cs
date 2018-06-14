using System.Linq;
using System.Collections.ObjectModel;

namespace XmlParser
{
    public class PostParser
    {
        private IPostContentParser _parser;
        private string[] rawPosts;

        public PostParser(IPostContentParser contentParser)
        {
            _parser = contentParser;
        }

        public Post[] ToPosts() {
            Collection<Post> result = new Collection<Post>();

            foreach (var rawPost in rawPosts)
            {
                _parser.LoadRawContent(rawPost);
                result.Add(_parser.GetPost());
            }
            return result.ToArray();
        }

        public void SetPosts(string[] rawPosts) {
            this.rawPosts = rawPosts;
        }
    }
}

