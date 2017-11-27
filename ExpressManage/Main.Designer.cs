namespace ExpressManage
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.button1 = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.seqNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.side = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sendTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expressNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.province = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shopName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tip = new System.Windows.Forms.Label();
            this.btnSerive = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "省份设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(629, 10);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(57, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seqNum,
            this.side,
            this.shop,
            this.order,
            this.buyID,
            this.buyTime,
            this.sendTime,
            this.company,
            this.expressNo,
            this.province,
            this.shopName});
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(818, 511);
            this.dataGridView1.TabIndex = 2;
            // 
            // seqNum
            // 
            this.seqNum.DataPropertyName = "seqNum";
            this.seqNum.HeaderText = "序号";
            this.seqNum.Name = "seqNum";
            this.seqNum.Width = 40;
            // 
            // side
            // 
            this.side.DataPropertyName = "side";
            this.side.HeaderText = "站点";
            this.side.Name = "side";
            this.side.Width = 70;
            // 
            // shop
            // 
            this.shop.DataPropertyName = "shop";
            this.shop.HeaderText = "店铺";
            this.shop.Name = "shop";
            this.shop.Width = 70;
            // 
            // order
            // 
            this.order.DataPropertyName = "order";
            this.order.HeaderText = "线上订单号";
            this.order.Name = "order";
            // 
            // buyID
            // 
            this.buyID.DataPropertyName = "buyID";
            this.buyID.HeaderText = "买家帐号";
            this.buyID.Name = "buyID";
            this.buyID.Width = 80;
            // 
            // buyTime
            // 
            this.buyTime.DataPropertyName = "buyDate";
            this.buyTime.HeaderText = "付款日期";
            this.buyTime.Name = "buyTime";
            this.buyTime.Width = 80;
            // 
            // sendTime
            // 
            this.sendTime.DataPropertyName = "sendDate";
            this.sendTime.HeaderText = "发货日期";
            this.sendTime.Name = "sendTime";
            this.sendTime.Width = 80;
            // 
            // company
            // 
            this.company.DataPropertyName = "company";
            this.company.HeaderText = "快递公司";
            this.company.Name = "company";
            this.company.Width = 80;
            // 
            // expressNo
            // 
            this.expressNo.DataPropertyName = "expressNo";
            this.expressNo.HeaderText = "快递单号";
            this.expressNo.Name = "expressNo";
            // 
            // province
            // 
            this.province.DataPropertyName = "province";
            this.province.HeaderText = "省份";
            this.province.Name = "province";
            this.province.Width = 70;
            // 
            // shopName
            // 
            this.shopName.DataPropertyName = "shopName";
            this.shopName.HeaderText = "商品名称";
            this.shopName.Name = "shopName";
            this.shopName.Width = 140;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "拼多多专用";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(692, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(57, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "订单管理系统";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // tip
            // 
            this.tip.AutoSize = true;
            this.tip.Location = new System.Drawing.Point(395, 206);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(0, 12);
            this.tip.TabIndex = 0;
            // 
            // btnSerive
            // 
            this.btnSerive.Location = new System.Drawing.Point(546, 10);
            this.btnSerive.Name = "btnSerive";
            this.btnSerive.Size = new System.Drawing.Size(75, 23);
            this.btnSerive.TabIndex = 5;
            this.btnSerive.Text = "开启服务";
            this.btnSerive.UseVisualStyleBackColor = true;
            this.btnSerive.Click += new System.EventHandler(this.btnSerive_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(455, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ExpressManage.Properties.Resources.apollo_130417_01;
            this.ClientSize = new System.Drawing.Size(842, 562);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSerive);
            this.Controls.Add(this.tip);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "快递单处理";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label tip;
        private System.Windows.Forms.DataGridViewTextBoxColumn seqNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn side;
        private System.Windows.Forms.DataGridViewTextBoxColumn shop;
        private System.Windows.Forms.DataGridViewTextBoxColumn order;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn sendTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn company;
        private System.Windows.Forms.DataGridViewTextBoxColumn expressNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn province;
        private System.Windows.Forms.DataGridViewTextBoxColumn shopName;
        private System.Windows.Forms.Button btnSerive;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
    }
}

