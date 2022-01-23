using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ObsCommentGenerator.Models;

namespace ObsCommentGenerator
{
    class Program
    {
        static string _outputFolder = "";
        static string _lastCommentNo = "";

        static void Main(string[] args)
        {
            _outputFolder = args[1];
            File.WriteAllText(_outputFolder, "");

            var folder = Path.GetDirectoryName(args[0]);
            var filename = Path.GetFileName(args[0]);
            using var watcher = new FileSystemWatcher(folder, filename);
            watcher.EnableRaisingEvents = true;
            watcher.Changed += OnChanged;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// Get the comments when file change is detected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                Task.Delay(100); // Add slight delay to allow system to finish writing the file
                var comments = GetMyComments(e.FullPath).ToArray();
                if (!comments.Any())
                {
                    // No elements
                    return;
                }
                WriteMyComment(comments.Last());
            }
            catch (IOException)
            {
                // File not finish writing or windows bug
            }
        }

        /// <summary>
        /// Get all of my comments from TTS
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static IEnumerable<Comment> GetMyComments(string filename)
        {
            var xmlFile = new MemoryStream(File.ReadAllBytes(filename));
            var xml = XDocument.Load(xmlFile);

            var log = xml.Root?.Descendants("comment");
            var comments = log?
                .Where(comment => comment.Attribute("service")!.Value == ""
                                  && comment.Attribute("handle")!.Value == "")
                .Select(comment => new Comment
                {
                    No = comment.Attribute("no")!.Value,
                    Message = comment.Value
                });
            return comments;
        }

        /// <summary>
        /// Writes the latest comment to the output file
        /// </summary>
        /// <param name="comment"></param>
        static void WriteMyComment(Comment comment)
        {
            if (comment.No == _lastCommentNo)
            {
                // Same comment do nothing
                return;
            }

            _lastCommentNo = comment.No;
            File.WriteAllText(_outputFolder, comment.Message);
        }
    }
}
