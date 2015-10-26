using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WinSync.Data
{
    public class DataManager
    {
        private const string LinksDataFilePath = @"links.dat";

        /// <summary>
        /// create data file if it doesn't exist
        /// </summary>
        /// <returns>true if file was created</returns>
        public static bool CreateDataFileIfNotExist()
        {
            if (File.Exists(LinksDataFilePath)) return false;

            StreamWriter sw = File.CreateText(LinksDataFilePath);

            sw.WriteLine("<links>");
            sw.WriteLine("</links>");

            sw.Close();
            return true;
        }

        /// <summary>
        /// Add Link to file
        /// </summary>
        /// <param name="link"></param>
        public static void AddLink(Link link)
        {
            if (LinkExists(link.Title))
                throw MyException.LinkAlreadyExists;
            
            List<string> lines = new List<string>(File.ReadAllLines(LinksDataFilePath));

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Trim().Equals("<links>"))
                {
                    lines.Insert(i+1, link.ToString());
                }
            }

            File.WriteAllLines(LinksDataFilePath, lines);
        }

        /// <summary>
        /// Update Link in file
        /// </summary>
        /// <param name="link"></param>
        /// <param name="oldTitle"></param>
        public static void UpdateLink(Link link, string oldTitle)
        {
            if (link.Title != oldTitle && LinkExists(link.Title))
                throw MyException.LinkAlreadyExists;

            List<string> lines = new List<string>(File.ReadAllLines(LinksDataFilePath));
            bool linkSec = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Trim().Equals("<links>"))
                    linkSec = true;
                else if (line.Trim().Equals("</links>"))
                    break;
                else if(linkSec)
                {
                    //get title
                    string[] parts = line.Split(new[]{'\"'}, 3);
                    if(parts.Length < 3) continue;
                    string title = parts[1];

                    if(title.Equals(oldTitle))
                    {
                        //set line
                        lines[i] = link + "\n";
                    }
                }
            }

            File.WriteAllLines(LinksDataFilePath, lines);
        }

        /// <summary>
        /// Delete Link from file
        /// </summary>
        /// <param name="title">the title of the Link</param>
        public static void DeleteLink(string title)
        {
            List<string> lines = new List<string>(File.ReadAllLines(LinksDataFilePath));
            bool linkSec = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Trim().Equals("<links>"))
                    linkSec = true;
                else if (line.Trim().Equals("</links>"))
                    break;
                else if (linkSec)
                {
                    //get title
                    string[] parts = line.Split(new[] { '\"' }, 3);
                    if (parts.Length < 3) continue;
                    string lineTitle = parts[1];

                    if (lineTitle.Equals(title))
                    {
                        //delete line
                        lines.RemoveAt(i);
                    }
                }
            }

            File.WriteAllLines(LinksDataFilePath, lines);
        }

        /// <summary>
        /// get whole list of links, saved in the file
        /// </summary>
        /// <returns></returns>
        public static List<Link> GetLinkList()
        {
            List<Link> links = new List<Link>();
            List<string> lines = new List<string>(File.ReadAllLines(LinksDataFilePath));
            List<int> errorLines = new List<int>();
            bool linkSec = false;

            int i = 0;
            foreach (string line in lines)
            {
                if (line.Trim().Equals("<links>"))
                    linkSec = true;
                else if (line.Trim().Equals("</links>"))
                    break;
                else if (linkSec)
                {
                    if(string.IsNullOrWhiteSpace(line))
                        continue;

                    Link l = Link.CreateFromLine(line);
                    if (l != null)
                        links.Add(l);
                    else
                    {
                        errorLines.Add(i);
                    }
                }

                i++;
            }
            if(errorLines.Count > 0)
                MessageBox.Show($"Please check links.dat. \nBad format detected in lines: {string.Join(", ", errorLines)}",
                    @"Bad Format in Data-File", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return links;
        }

        /// <summary>
        /// Check if a Link with title exists in the file
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool LinkExists(string title)
        {
            List<string> lines = new List<string>(File.ReadAllLines(LinksDataFilePath));
            bool linkSec = false;

            foreach (string line in lines)
            {
                if (line.Trim().Equals("<links>"))
                    linkSec = true;
                else if (line.Trim().Equals("</links>"))
                    break;
                else if (linkSec)
                {
                    //get title
                    string[] parts = line.Split(new[] { '\"' }, 3);
                    if (parts.Length < 3) continue;
                    string lineTitle = parts[1];

                    if (lineTitle.Equals(title))
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}

