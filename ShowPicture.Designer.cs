namespace OrderManage
{
    partial class ShowPicture
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPicture));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.timeRefoundTo = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.timeRefoundFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNo = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbTip = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tip = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(256, 256);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(34, 585);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1007, 250);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Visible = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // timeRefoundTo
            // 
            this.timeRefoundTo.Location = new System.Drawing.Point(279, 22);
            this.timeRefoundTo.Name = "timeRefoundTo";
            this.timeRefoundTo.Size = new System.Drawing.Size(144, 21);
            this.timeRefoundTo.TabIndex = 57;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(247, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 12);
            this.label15.TabIndex = 56;
            this.label15.Text = "至:";
            // 
            // timeRefoundFrom
            // 
            this.timeRefoundFrom.Location = new System.Drawing.Point(94, 21);
            this.timeRefoundFrom.Name = "timeRefoundFrom";
            this.timeRefoundFrom.Size = new System.Drawing.Size(144, 21);
            this.timeRefoundFrom.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 58;
            this.label1.Text = "上传时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(438, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 59;
            this.label2.Text = "流水号:";
            // 
            // tbNo
            // 
            this.tbNo.Location = new System.Drawing.Point(492, 22);
            this.tbNo.Name = "tbNo";
            this.tbNo.Size = new System.Drawing.Size(100, 21);
            this.tbNo.TabIndex = 60;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1132, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 61;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lbTip
            // 
            this.lbTip.AutoSize = true;
            this.lbTip.Location = new System.Drawing.Point(439, 298);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(0, 12);
            this.lbTip.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tip);
            this.panel1.Location = new System.Drawing.Point(12, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1253, 600);
            this.panel1.TabIndex = 63;
            // 
            // tip
            // 
            this.tip.AutoSize = true;
            this.tip.Location = new System.Drawing.Point(539, 250);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(0, 12);
            this.tip.TabIndex = 0;
            // 
            // ShowPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1277, 657);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbTip);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeRefoundTo);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.timeRefoundFrom);
            this.Name = "ShowPicture";
            this.Text = "图片查询";
            this.Load += new System.EventHandler(this.ShowPicture_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DateTimePicker timeRefoundTo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker timeRefoundFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lbTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label tip;
    }
}