using System.Windows.Forms;
namespace TorrentDownloader
{
    partial class frmUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUI));
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblSpeedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPeersLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPeersCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barProgress
            // 
            resources.ApplyResources(this.barProgress, "barProgress");
            this.barProgress.Name = "barProgress";
            // 
            // lstLog
            // 
            resources.ApplyResources(this.lstLog, "lstLog");
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Name = "lstLog";
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSpeedLabel,
            this.lblSpeed,
            this.lblPeersLabel,
            this.lblPeersCount,
            this.toolStripStatusLabel4,
            this.toolStripSplitButton1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.SizingGrip = false;
            // 
            // lblSpeedLabel
            // 
            this.lblSpeedLabel.Name = "lblSpeedLabel";
            resources.ApplyResources(this.lblSpeedLabel, "lblSpeedLabel");
            // 
            // lblSpeed
            // 
            resources.ApplyResources(this.lblSpeed, "lblSpeed");
            this.lblSpeed.Name = "lblSpeed";
            // 
            // lblPeersLabel
            // 
            this.lblPeersLabel.Name = "lblPeersLabel";
            resources.ApplyResources(this.lblPeersLabel, "lblPeersLabel");
            // 
            // lblPeersCount
            // 
            resources.ApplyResources(this.lblPeersCount, "lblPeersCount");
            this.lblPeersCount.Name = "lblPeersCount";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            resources.ApplyResources(this.toolStripStatusLabel4, "toolStripStatusLabel4");
            this.toolStripStatusLabel4.Spring = true;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownButtonWidth = 0;
            resources.ApplyResources(this.toolStripSplitButton1, "toolStripSplitButton1");
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // frmUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.lblProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUI_FormClosing);
            this.Load += new System.EventHandler(this.frmUI_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar barProgress;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblSpeed;
        private System.Windows.Forms.ToolStripStatusLabel lblPeersCount;
        private ToolStripStatusLabel lblSpeedLabel;
        private ToolStripStatusLabel lblPeersLabel;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripSplitButton toolStripSplitButton1;
    }
}

