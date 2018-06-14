namespace XmlParser
{
    public class PostParser
    {
        private IPostContentParser _parser;

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
}

