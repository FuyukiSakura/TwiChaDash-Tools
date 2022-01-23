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
        static void Main(string[] args)
        {
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
                var comments = GetMyComments(e.FullPath);
                Console.WriteLine(string.Join("\r\n", comments.Select(cm => cm.Message)));
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

    }
}
