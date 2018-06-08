using System;
using HtmlAgilityPack;
using System.IO;
using XmlParser;

namespace HtmlToCsv
{
    class Program
    {
        static void Main(string[] args)
        {
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
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                foreach (var item in doc.DocumentNode.ChildNodes)
                {
                    Console.WriteLine(item.InnerHtml);
                }
            } else {
                Console.WriteLine("No filename provided");
            }
        }
    }
}
