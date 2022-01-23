using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ObsCommentGenerator.Models;

namespace ObsCommentGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var comments = GetMyComments(args[0]);
            Console.WriteLine(string.Join("\r\n", comments.Select(cm => cm.Message)));
            Console.ReadLine();
        }

        /// <summary>
        /// Get all of my comments from TTS
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static IEnumerable<Comment> GetMyComments(string filename)
        {
            // load xml file
            var xml = XDocument.Load(filename);

            var log = xml.Root?.Descendants("comment");
            var comments = log?
                .Where(comment => comment.Attribute("service")!.Value == ""
                                  && comment.Attribute("handle")!.Value == "")
                .Select(comment => new Comment
                {
                    No = comment.Attribute("No")!.Value,
                    Message = comment.Value
                });
            return comments;
        }
    }
}
