using System;
using CsvHelper.Configuration;

namespace XmlParser
{
    public class Post
    {
        public string original_id { get; set; }
        public DateTime posted_date { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string[] images { get; set; }
        public string content { get; set; }

        public Post(){}

        public Post(string title, string description, string original_id)
        {
            this.title = title;
            this.description = description;
            this.original_id = original_id;
        }

        public sealed class MyClassMap : ClassMap<Post>
        {
            public MyClassMap()
            {
                AutoMap();
            }
        }
    }
}
