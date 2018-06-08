using System;
namespace XmlParser
{
    public class Post
    {
        public string title;
        public string author;
        public string description;
        public string original_id;
        public string[] images;
        public DateTime posted_date;

        public Post(){}

        public Post(string title, string description, string original_id)
        {
            this.title = title;
            this.description = description;
            this.original_id = original_id;
        }
    }
}
