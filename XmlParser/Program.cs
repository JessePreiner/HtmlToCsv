using System;
using HtmlAgilityPack;
using System.IO;
using XmlParser;
using CsvHelper;
using System.Collections.Generic;
using CommandLine;
using System.Linq;

namespace HtmlToCsv
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ArgumentOptions>(args)
                .WithParsed(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed((errs) => HandleParseError(errs));
        }

        static void RunOptionsAndReturnExitCode(ArgumentOptions opts)
        {
            LogOptions(opts);

            if (ImpossibleArgumentConfiguration(opts))
            {
                Console.WriteLine("Can't process both sitemap and input files");
                return;
            }

            if (ProcessingSitemap(opts)) { ProcessSitemap(opts.SitemapUrl, opts.CsvName); }
            if (ProcessingFileList(opts)) { ProcessFileList(opts.InputFiles, opts.CsvName); }
        }

        private static void ProcessFileList(IEnumerable<string> inputFiles, string outputFilePath)
        {
            /*
             * 
             * htmlfiles <- getHtmlFilesFromInputFiles(inputFiles)
             * posts <- getPostsFromHtmlFiles(htmlFiles)
             * csv = getCsvFromPosts(posts)
             * 
             * 
             */

            PostParser _postParser;
            Post[] posts = new Post[] { };


            String html = null;
            try
            {
                html = File.ReadAllText(inputFiles.First());
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Can't read the file {inputFiles.First()} " +
                                  $"because {ex.GetBaseException().Message}");
                return;
            }

            _postParser = new PostParser(new HtmlPostContentParser(new HtmlDocument()), html);

            Post result = _postParser.ToPost();
            using (TextWriter writer = new StreamWriter(outputFilePath))
            {
                CsvWriter csv = new CsvWriter(writer);
                csv.WriteHeader<Post>();
                csv.WriteRecord(result);
                csv.NextRecord();
            }
        }

        private static void ProcessSitemap(string sitemapUrl, string outputFileName)
        {
            throw new NotImplementedException("Currently only works for input files (the first one)");
        }

        private static bool ProcessingFileList(ArgumentOptions opts)
        {
            return opts.InputFiles.Count() > 0;
        }

        private static bool ProcessingSitemap(ArgumentOptions opts)
        {
            return !String.IsNullOrEmpty(opts.SitemapUrl);
        }

        private static void LogOptions(ArgumentOptions opts)
        {
            Console.WriteLine($"csv:\t\t{opts.CsvName}\n" +
                              $"input files:\t{String.Join(",", opts.InputFiles)}\n" +
                              $"sitemap:\t{opts.SitemapUrl}\n\n");
        }

        private static bool ImpossibleArgumentConfiguration(ArgumentOptions opts)
        {
            return ProcessingSitemap(opts) && ProcessingFileList(opts);
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("We have a problem");
        }
    }
}
