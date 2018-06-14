namespace XmlParser
{
    public interface IPostContentParser
    {
        Post GetPost();
        void LoadRawContent(string rawContent);
    }
}

