using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MonoTorrent.Common;
using MonoTorrent.Client;
using System.Resources;
using MonoTorrent.BEncoding;

namespace TorrentDownloader
{
    public partial class frmUI : Form
    {
        public string boundary = "=TD" + "=B";
        public string fastResumeFileName = "fastresume.dat";
        
        public TorrentManager tm;
        public ClientEngine te;
        public ComponentResourceManager Resources = new ComponentResourceManager(typeof(frmUI));

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
        private static int FindSubArray(byte[] array, byte[] needle, int startIndex, int sourceLength)
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
    
        public frmUI() {
            InitializeComponent();
        }
        protected string FormatSize(long bytes) { 
            
            if( bytes > 1024 && bytes < (1048576) ){ //1024^2
                return Math.Round(((decimal)bytes / 1024), 1) + " "+Resources.GetString("KB/s");
            }
            else if (bytes > (1048576) && bytes < (1073741824)){ //1024^3
                return Math.Round(((decimal)bytes / (1048576)), 1) + " " + Resources.GetString("MB/s");
            }
            else if (bytes > (1073741824) && bytes < (1099511627776)) {//1024^4
                return Math.Round(((decimal)bytes / (1073741824)), 1) + " " + Resources.GetString("GB/s");
            }
            else if( bytes > (1099511627776 ) ){
                return Math.Round(((decimal)bytes / (1099511627776)), 1) + " " + Resources.GetString("TB/s");
            }
            else{
                return bytes + " " + Resources.GetString("bytes/s"); ;
            }
        }
        void LoggerHandler(object sender, EventArgs e) {
            if (lstLog.InvokeRequired) {
                Action wrapper = delegate(){LoggerHandler(sender,e);};
                lstLog.Invoke(wrapper);
                return;
            }
            if (e is StatsUpdateEventArgs) {
                this.barProgress.Value = (int)this.tm.Progress;
                this.lblProgress.Text = String.Format("{0}%", this.tm.Progress);
                this.lblSpeed.Text = String.Format("{0}", FormatSize(this.tm.Monitor.DownloadSpeed));
                this.lblPeersCount.Text = String.Format("{0}/{1}", this.tm.Peers.Seeds, this.tm.Peers.Leechs);
            }
            else if (e is PeerConnectionEventArgs)
            {
                lstLog.Items.Add(String.Format("Peer {0} connected", ((PeerConnectionEventArgs)e).PeerID));
            }else if (e is PeerConnectionEventArgs)
            {
                lstLog.Items.Add(String.Format("Peer {0} disconnected", ((PeerConnectionEventArgs)e).PeerID));
            }else if (e is PeersAddedEventArgs)
            {
                lstLog.Items.Add(String.Format("{0} new peers found", ((PeersAddedEventArgs)e).NewPeers));
            }else if (e is PieceHashedEventArgs)
            {
                lstLog.Items.Add(String.Format("Piece #{0} hashed", ((PieceHashedEventArgs)e).PieceIndex));
            }else if (e is TorrentStateChangedEventArgs)
            {
                lstLog.Items.Add(String.Format("State changed to {0}", 
                    Enum.GetName(typeof(TorrentState), 
                        ((TorrentStateChangedEventArgs)e).NewState)
                    )
                );
            }
        }

        private void frmUI_Load(object sender, EventArgs e)
        {
            byte[] exeBytes = File.ReadAllBytes(Application.ExecutablePath);
            byte[] boundaryBytes = Encoding.ASCII.GetBytes(this.boundary);
            int torrentBeginsAt = FindSubArray(exeBytes, boundaryBytes, 0, exeBytes.Length);

            if (torrentBeginsAt == -1)
            {
                MessageBox.Show(Resources.GetString("cannot_find_boundary"));
                this.Close();
            }
            torrentBeginsAt += boundaryBytes.Length;
            byte[] torrentBytes = new byte[exeBytes.Length - torrentBeginsAt];
            Array.Copy(exeBytes, torrentBeginsAt, torrentBytes, 0, torrentBytes.Length);

            try
            {
                Torrent t = Torrent.Load(torrentBytes);
                string dirName = t.Name;
                foreach (char wrongChar in Path.GetInvalidFileNameChars())
                {
                    dirName.Replace(wrongChar, '_');
                }
                dirName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), dirName);

                this.te = new ClientEngine(new EngineSettings());
                this.tm = new TorrentManager(t, dirName, new TorrentSettings());

                string newDirName = dirName;
                int i = 0;
                while (File.Exists(newDirName))
                {
                    newDirName = dirName + "_" + i++;
                }
                dirName = newDirName;

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                else if (File.Exists(Path.Combine(dirName, fastResumeFileName)))
                {
                    tm.LoadFastResume(new FastResume(
                        BEncodedDictionary.Decode<BEncodedDictionary>(
                            File.ReadAllBytes(
                                Path.Combine(dirName, fastResumeFileName)
                            )
                        )
                    ));
                }

                this.te.Register(this.tm);
            }
            catch (BEncodingException)
            {
                MessageBox.Show(Resources.GetString("torrent_couldnt_be_read"));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.GetString("unknown_error") + Environment.NewLine + ex.Message);
                this.Close();
            }

            if (this.tm == null) { throw new Exception("TorrentManager is not instantiated"); }
            this.Text += this.tm.Torrent.Name;

            this.tm.TorrentStateChanged += new EventHandler<TorrentStateChangedEventArgs>(LoggerHandler);
            this.tm.PieceHashed += new EventHandler<PieceHashedEventArgs>(LoggerHandler);
            this.tm.PeersFound += new EventHandler<PeersAddedEventArgs>(LoggerHandler);
            this.tm.PeerDisconnected += new EventHandler<PeerConnectionEventArgs>(LoggerHandler);
            this.tm.PeerConnected += new EventHandler<PeerConnectionEventArgs>(LoggerHandler);

            this.te.StatsUpdate += new EventHandler<StatsUpdateEventArgs>(LoggerHandler);
            this.tm.Start();
        }

        private void frmUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.Write(true);
            try
            {
                FastResume fr = this.tm.SaveFastResume();
                File.WriteAllBytes(Path.Combine(this.tm.SavePath, fastResumeFileName), fr.Encode().Encode());
            }
            catch { }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            if (this.Height == 350)
            {
                this.Height = 95;
            }
            else {
                this.Height = 350;
            }
        }
    }
}
