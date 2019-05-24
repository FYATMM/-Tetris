namespace 俄罗斯方块Tetris
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.reviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLine = new System.Windows.Forms.ToolStripSeparator();
            this.exiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.style1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.style2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.style3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarReview = new System.Windows.Forms.ProgressBar();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.trackBarReviewSpeed = new System.Windows.Forms.TrackBar();
            this.buttonReview = new System.Windows.Forms.Button();
            this.buttonReplay = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReviewSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(260, 25);
            this.menuStripMain.TabIndex = 7;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replayToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.reviewToolStripMenuItem,
            this.toolStripMenuItemLine,
            this.exiToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileToolStripMenuItem.Text = "&文件";
            // 
            // replayToolStripMenuItem
            // 
            this.replayToolStripMenuItem.Name = "replayToolStripMenuItem";
            this.replayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.replayToolStripMenuItem.Text = "重新&开始";
            this.replayToolStripMenuItem.Click += new System.EventHandler(this.replayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "Replay(Extended)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.replayExtendedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "&Save";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = "&Load";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // reviewToolStripMenuItem
            // 
            this.reviewToolStripMenuItem.Name = "reviewToolStripMenuItem";
            this.reviewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reviewToolStripMenuItem.Text = "Re&view";
            this.reviewToolStripMenuItem.Click += new System.EventHandler(this.reviewToolStripMenuItem_Click);
            // 
            // toolStripMenuItemLine
            // 
            this.toolStripMenuItemLine.Name = "toolStripMenuItemLine";
            this.toolStripMenuItemLine.Size = new System.Drawing.Size(174, 6);
            // 
            // exiToolStripMenuItem
            // 
            this.exiToolStripMenuItem.Name = "exiToolStripMenuItem";
            this.exiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exiToolStripMenuItem.Text = "E&xit";
            this.exiToolStripMenuItem.Click += new System.EventHandler(this.exiToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.styleToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.optionToolStripMenuItem.Text = "&设置选项";
            // 
            // styleToolStripMenuItem
            // 
            this.styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.style1ToolStripMenuItem,
            this.style2ToolStripMenuItem,
            this.style3ToolStripMenuItem});
            this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            this.styleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.styleToolStripMenuItem.Text = "&Style";
            // 
            // style1ToolStripMenuItem
            // 
            this.style1ToolStripMenuItem.Name = "style1ToolStripMenuItem";
            this.style1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.style1ToolStripMenuItem.Tag = "";
            this.style1ToolStripMenuItem.Text = "style1";
            this.style1ToolStripMenuItem.Click += new System.EventHandler(this.style1ToolStripMenuItem_Click);
            // 
            // style2ToolStripMenuItem
            // 
            this.style2ToolStripMenuItem.Name = "style2ToolStripMenuItem";
            this.style2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.style2ToolStripMenuItem.Text = "style2";
            this.style2ToolStripMenuItem.Click += new System.EventHandler(this.style1ToolStripMenuItem_Click);
            // 
            // style3ToolStripMenuItem
            // 
            this.style3ToolStripMenuItem.Name = "style3ToolStripMenuItem";
            this.style3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.style3ToolStripMenuItem.Text = "style3";
            this.style3ToolStripMenuItem.Click += new System.EventHandler(this.style1ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyboardToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.helpToolStripMenuItem.Text = "&游戏帮助";
            // 
            // keyboardToolStripMenuItem
            // 
            this.keyboardToolStripMenuItem.Name = "keyboardToolStripMenuItem";
            this.keyboardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.keyboardToolStripMenuItem.Text = "&操作键";
            this.keyboardToolStripMenuItem.Click += new System.EventHandler(this.keyBoardToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // progressBarReview
            // 
            this.progressBarReview.Location = new System.Drawing.Point(4, 380);
            this.progressBarReview.Name = "progressBarReview";
            this.progressBarReview.Size = new System.Drawing.Size(252, 17);
            this.progressBarReview.TabIndex = 13;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(181, 266);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 12;
            this.buttonLoad.Text = "载入";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(181, 237);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // trackBarReviewSpeed
            // 
            this.trackBarReviewSpeed.Location = new System.Drawing.Point(181, 291);
            this.trackBarReviewSpeed.Maximum = 15;
            this.trackBarReviewSpeed.Minimum = 1;
            this.trackBarReviewSpeed.Name = "trackBarReviewSpeed";
            this.trackBarReviewSpeed.Size = new System.Drawing.Size(75, 45);
            this.trackBarReviewSpeed.TabIndex = 10;
            this.trackBarReviewSpeed.Value = 1;
            this.trackBarReviewSpeed.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // buttonReview
            // 
            this.buttonReview.Location = new System.Drawing.Point(181, 337);
            this.buttonReview.Name = "buttonReview";
            this.buttonReview.Size = new System.Drawing.Size(75, 23);
            this.buttonReview.TabIndex = 9;
            this.buttonReview.Text = "Re&view";
            this.buttonReview.UseVisualStyleBackColor = true;
            this.buttonReview.Click += new System.EventHandler(this.buttonReview_Click);
            // 
            // buttonReplay
            // 
            this.buttonReplay.Location = new System.Drawing.Point(181, 196);
            this.buttonReplay.Name = "buttonReplay";
            this.buttonReplay.Size = new System.Drawing.Size(75, 23);
            this.buttonReplay.TabIndex = 8;
            this.buttonReplay.Text = "重新开始";
            this.buttonReplay.UseVisualStyleBackColor = true;
            this.buttonReplay.Click += new System.EventHandler(this.buttonReplay_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "youxi records files(*.trf)|*.trf|All Files(*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "youxi records files(*.trf)|*.trf";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "block.png");
            this.imageList1.Images.SetKeyName(1, "block.png");
            this.imageList1.Images.SetKeyName(2, "block.png");
            this.imageList1.Images.SetKeyName(3, "block.png");
            this.imageList1.Images.SetKeyName(4, "block.png");
            this.imageList1.Images.SetKeyName(5, "block.png");
            this.imageList1.Images.SetKeyName(6, "block.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "block.png");
            this.imageList2.Images.SetKeyName(1, "block.png");
            this.imageList2.Images.SetKeyName(2, "block.png");
            this.imageList2.Images.SetKeyName(3, "block.png");
            this.imageList2.Images.SetKeyName(4, "block.png");
            this.imageList2.Images.SetKeyName(5, "block.png");
            this.imageList2.Images.SetKeyName(6, "block.png");
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "block.png");
            this.imageList3.Images.SetKeyName(1, "block.png");
            this.imageList3.Images.SetKeyName(2, "block.png");
            this.imageList3.Images.SetKeyName(3, "block.png");
            this.imageList3.Images.SetKeyName(4, "block.png");
            this.imageList3.Images.SetKeyName(5, "block.png");
            this.imageList3.Images.SetKeyName(6, "block.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(260, 410);
            this.Controls.Add(this.progressBarReview);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.trackBarReviewSpeed);
            this.Controls.Add(this.buttonReview);
            this.Controls.Add(this.buttonReplay);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReviewSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem reviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemLine;
        private System.Windows.Forms.ToolStripMenuItem exiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem styleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem style1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem style2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem style3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBarReview;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TrackBar trackBarReviewSpeed;
        private System.Windows.Forms.Button buttonReview;
        private System.Windows.Forms.Button buttonReplay;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList3;
    }
}

