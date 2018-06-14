using System.Collections.Generic;
using CommandLine;

namespace HtmlToCsv
{
    partial class Program
    {
        class ArgumentOptions
        {
            [Option('i', "input", Required = false, HelpText = "Input files to be processed.")]
            public IEnumerable<string> InputFiles { get; set; }

            [Option('s', "sitemap", Required = false, HelpText = "Sitemap to process.")]
            public string SitemapUrl { get; set; }

            [Option('o', "output", Required = false, HelpText = "Name of CSV to write out.")]
            public string CsvName { get; set; }
        }
    }
}
