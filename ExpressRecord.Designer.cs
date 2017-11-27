namespace OrderManage
{
    partial class ExpressRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressRecord));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.timePickTo = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.timePickFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbExpressStatus = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.seqNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seqNum,
            this.Status,
            this.importDate,
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1060, 525);
            this.dataGridView1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "导入时间:";
            // 
            // timePickTo
            // 
            this.timePickTo.Location = new System.Drawing.Point(244, 15);
            this.timePickTo.Name = "timePickTo";
            this.timePickTo.Size = new System.Drawing.Size(137, 21);
            this.timePickTo.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(212, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 45;
            this.label11.Text = "至:";
            // 
            // timePickFrom
            // 
            this.timePickFrom.Location = new System.Drawing.Point(74, 14);
            this.timePickFrom.Name = "timePickFrom";
            this.timePickFrom.Size = new System.Drawing.Size(128, 21);
            this.timePickFrom.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(403, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "快递状态:";
            // 
            // cmbExpressStatus
            // 
            this.cmbExpressStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpressStatus.FormattingEnabled = true;
            this.cmbExpressStatus.Location = new System.Drawing.Point(468, 14);
            this.cmbExpressStatus.Name = "cmbExpressStatus";
            this.cmbExpressStatus.Size = new System.Drawing.Size(87, 20);
            this.cmbExpressStatus.TabIndex = 48;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(899, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 49;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(988, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 50;
            this.button2.Text = "导出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // seqNum
            // 
            this.seqNum.DataPropertyName = "seqNum";
            this.seqNum.HeaderText = "序号";
            this.seqNum.Name = "seqNum";
            this.seqNum.Width = 40;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "StatusDesc";
            this.Status.HeaderText = "状态";
            this.Status.Name = "Status";
            // 
            // importDate
            // 
            this.importDate.DataPropertyName = "importDate";
            this.importDate.HeaderText = "导入日期";
            this.importDate.Name = "importDate";
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
            // ExpressRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1084, 590);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbExpressStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timePickTo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.timePickFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ExpressRecord";
            this.Text = "快递记录查询";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker timePickTo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker timePickFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbExpressStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn seqNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn importDate;
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
    }
}