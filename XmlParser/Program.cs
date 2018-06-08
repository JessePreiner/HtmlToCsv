using System;
using HtmlAgilityPack;
using System.IO;
using XmlParser;
using CsvHelper;
using System.Text;

namespace HtmlToCsv
{
    class Program
    {

        static void Main(string[] args)
        {
            PostParser _postParser;
            Post[] posts = new Post[] { };

            if (args != null && args.Length > 0) {
                String html = null;
                try
                {
                    html = File.ReadAllText(args[0]);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"Can't read the file {args[0]} " +
                                      $"because {ex.GetBaseException().Message}");
                    return;
                }

                _postParser = new PostParser(new HtmlPostContentParser(new HtmlDocument()), html);

                Post result = _postParser.ToPost();
                using (TextWriter writer = new StreamWriter(@"output.csv")) {
                    CsvWriter csv = new CsvWriter(writer);
                    csv.WriteHeader<Post>();
                    csv.WriteRecord(result);
                    csv.NextRecord();
                }


            } else {
                Console.WriteLine("No filename provided");
            }
        }
    }
}
