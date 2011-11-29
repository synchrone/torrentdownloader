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

namespace TorrentDownloader
{
    public partial class frmUI : Form
    {
        protected Timer timer = new Timer();

        public frmUI() {
            InitializeComponent();

            if (Program.tm == null) { throw new Exception("TorrentManager is not instantiated"); }
            this.Text += Program.tm.Torrent.Name;

            Program.tm.TorrentStateChanged += new EventHandler<TorrentStateChangedEventArgs>(tm_TorrentStateChanged);
            Program.tm.PieceHashed += new EventHandler<PieceHashedEventArgs>(tm_PieceHashed);
            Program.tm.PeersFound += new EventHandler<PeersAddedEventArgs>(tm_PeersFound);
            Program.tm.PeerDisconnected += new EventHandler<PeerConnectionEventArgs>(tm_PeerDisconnected);
            Program.tm.PeerConnected += new EventHandler<PeerConnectionEventArgs>(tm_PeerConnected);

            timer.Interval = 500;
            timer.Tick+=new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.barProgress.Value = (int)Program.tm.Progress;
        } 

        void tm_PeerConnected(object sender, PeerConnectionEventArgs e)
        {
            
        }

        void tm_PeerDisconnected(object sender, PeerConnectionEventArgs e)
        {
            
        }
     
        void tm_PeersFound(object sender, PeersAddedEventArgs e)
        {
            
        }

        void tm_PieceHashed(object sender, PieceHashedEventArgs e)
        {
            
        }

        void tm_TorrentStateChanged(object sender, TorrentStateChangedEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
