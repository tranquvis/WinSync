using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using WinSync.Service;

namespace WinSync.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeType">
    /// 0: Link added
    /// 1: Link data changed
    /// 2: Link removed
    /// </param>
    /// <param name="l">Link</param>
    public delegate void LinkDataChangedEventHandler(int changeType, SyncLink l);

    public class DataManager
    {
        private const string LinksDataFilePath = @"links.dat";

        static List<SyncLink> _links;

        /// <summary>
        /// list of Links
        /// if they are null, they will be loaded from file
        /// </summary>
        public static List<SyncLink> Links
        {
            get
            {
                if (_links == null)
                    LoadLinks();
                return _links;
            }
        }

        /// <summary>
        /// occurs, when a Link was added, removed or changed
        /// </summary>
        public static event LinkDataChangedEventHandler LinkChanged;

        /// <summary>
        /// load list of links from file
        /// </summary>
        /// <returns></returns>
        public static void LoadLinks()
        {
            if(AnySyncRunning)
                throw new ApplicationException("Cannot load Links while any sync is running!");

            _links = new List<SyncLink>();

            if (CreateDataFileIfNotExist())
                return;

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
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Link l = Link.CreateFromLine(line);
                    if (l != null)
                    { 
                        SyncLink sl = new SyncLink(l);
                        _links.Add(sl);
                        LinkChanged(0, sl);
                    }
                    else
                    {
                        errorLines.Add(i);
                    }
                }

                i++;
            }
            if (errorLines.Count > 0)
                MessageBox.Show($"Please check links.dat. \nBad format detected in lines: {string.Join(", ", errorLines)}",
                    @"Bad Format in Data-File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// update file to match the links in DataManager
        /// </summary>
        public static void SaveLinksToFile()
        {
            List<string> lines = new List<string>(File.ReadAllLines(LinksDataFilePath));
            bool linkSec = false;
            int linkSecPos = -1;
            int oldLinksCount = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Trim().Equals("<links>"))
                    linkSec = true;
                else if (line.Trim().Equals("</links>"))
                    break;
                else if (linkSec)
                {
                    if (linkSecPos == -1)
                        linkSecPos = i;
                    oldLinksCount++;
                }
            }

            if (linkSecPos == -1)
                throw new FormatException("The data file must contain a links section.");

            //remove old links
            lines.RemoveRange(linkSecPos, oldLinksCount);
            //insert new links
            lines.InsertRange(linkSecPos, Links.ConvertAll(x => x.ToString() + "\n"));

            File.WriteAllLines(LinksDataFilePath, lines);
        }

        /// <summary>
        /// create data file if it doesn't exist
        /// </summary>
        /// <returns>false if the file existed already</returns>
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
        /// add Link and update file
        /// </summary>
        /// <param name="link"></param>
        public static void AddLink(Link link)
        {
            if (LinkExists(link.Title))
                throw BadInputException.LinkAlreadyExists;

            SyncLink sl = new SyncLink(link);
            Links.Add(sl);
            LinkChanged(0, sl);

            SaveLinksToFile();
        }

        /// <summary>
        /// change Link and update file
        /// </summary>
        /// <param name="link">new link data</param>
        /// <param name="oldTitle">title of old link</param>
        public static void ChangeLink(Link link, string oldTitle)
        {
            if (link.Title != oldTitle && LinkExists(link.Title))
                throw BadInputException.LinkAlreadyExists;

            int pos = GetLinkPosByTitle(oldTitle);
            if (Links[pos].IsRunning)
                throw new ApplicationException("Cannot change Link while its sync is running!");

            SyncLink sl = new SyncLink(link);
            Links[pos] = sl;
            LinkChanged(1, sl);

            SaveLinksToFile();
        }

        /// <summary>
        /// remove Link and update file
        /// </summary>
        /// <param name="title">the title of the Link</param>
        public static void RemoveLink(string title)
        {
            int pos = GetLinkPosByTitle(title);
            SyncLink sl = Links[pos];
            if (Links[pos].IsRunning)
                throw new ApplicationException("Cannot remove Link while its sync is running!");
            
            Links.RemoveAt(pos);
            LinkChanged(2, sl);

            SaveLinksToFile();
        }

        /// <summary>
        /// if a Link with title exists
        /// </summary>
        /// <param name="title"></param>
        private static bool LinkExists(string title)
        {
            return GetLinkPosByTitle(title) != -1;
        }

        /// <summary>
        /// get Position of Link in List by title
        /// </summary>
        /// <param name="title">Link Position or -1 if no matching link found</param>
        private static int GetLinkPosByTitle(string title)
        {
            int pos = -1;
            for(int i = 0; i < Links.Count; i++)
            {
                if(Links[0].Title == title)
                {
                    pos = i;
                    break;
                }
            }

            return pos;
        }

        #region sync data
        /// <summary>
        /// check if any synchronisation is running
        /// </summary>
        /// <returns></returns>
        public static bool AnySyncRunning
        {
            get { return _links != null && _links.Any(l => l.IsRunning); }
        }
        
        /// <summary>
        /// get total progress of all syncs
        /// </summary>
        /// <returns></returns>
        public static float GetTotalProgress()
        {
            float total = 0;
            float p = 0;

            foreach (SyncLink l in Links)
            {
                if (!l.IsRunning) continue;
                total += l.SyncInfo.TotalFileSize;
                p += l.SyncInfo.SizeApplied;
            }

            return total > 0 ? 100f / total * p : 0f;
        }
        #endregion
    }
}

