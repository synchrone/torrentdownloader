using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;
using MonoTorrent.Client;
using MonoTorrent.Common;

namespace TorrentDownloader
{
    static class Program
    {
        public static string boundary = "=TD"+"=B";
        public static TorrentManager tm;
        public static ClientEngine te;
        [STAThread]
        static void Main()
        {
            byte[] exeBytes = File.ReadAllBytes(Application.ExecutablePath);
            byte[] boundaryBytes = Encoding.ASCII.GetBytes(Program.boundary);
            int torrentBeginsAt = FindSubArray(exeBytes, boundaryBytes, 0, exeBytes.Length);

            if (torrentBeginsAt == -1) {
                MessageBox.Show("Couldn't find Boundary: "+Program.boundary);
                return;
            }
            torrentBeginsAt += boundaryBytes.Length;
            byte[] torrentBytes = new byte[exeBytes.Length - torrentBeginsAt] ;
            Array.Copy(exeBytes, torrentBeginsAt, torrentBytes, 0,torrentBytes.Length);

            Torrent t = Torrent.Load(torrentBytes);    
     
            string dirName = t.Name;
            foreach (char wrongChar in Path.GetInvalidFileNameChars())
            {
                dirName.Replace(wrongChar, '_');
            }
            Program.te = new ClientEngine(new EngineSettings());
            Program.tm = new TorrentManager(t, Path.Combine(Application.ExecutablePath, dirName), new TorrentSettings());
            Program.te.Register(Program.tm);

            Program.tm.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmUI());
        }

        /// <summary>
        /// Find subarray in the source array.
        /// </summary>
        /// 
        /// <param name="array">Source array to search for needle.</param>
        /// <param name="needle">Needle we are searching for.</param>
        /// <param name="startIndex">Start index in source array.</param>
        /// <param name="sourceLength">Number of bytes in source array, where the needle is searched for.</param>
        /// 
        /// <returns>Returns starting position of the needle if it was found or <b>-1</b> otherwise.</returns>
        /// 
        public static int FindSubArray(byte[] array, byte[] needle, int startIndex, int sourceLength)
        {
            int needleLen = needle.Length;
            int index;

            while (sourceLength >= needleLen)
            {
                // find needle's starting element
                index = Array.IndexOf(array, needle[0], startIndex, sourceLength - needleLen + 1);

                // if we did not find even the first element of the needls, then the search is failed
                if (index == -1)
                    return -1;

                int i, p;
                // check for needle
                for (i = 0, p = index; i < needleLen; i++, p++)
                {
                    if (array[p] != needle[i])
                    {
                        break;
                    }
                }

                if (i == needleLen)
                {
                    // needle was found
                    return index;
                }

                // continue to search for needle
                sourceLength -= (index - startIndex + 1);
                startIndex = index + 1;
            }
            return -1;
        }
    }
}
