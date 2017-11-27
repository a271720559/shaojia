namespace OrderManage
{
    partial class ExpressReceive
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressReceive));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgShow = new System.Windows.Forms.DataGridView();
            this.IsSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SeqNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyersID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShopName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefundWay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefundAlipayNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlipayNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChineseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefundAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefundResoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExpressNo = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbResion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.timePickTo = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.timePickFrom = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDeal = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.txtCreateUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTime = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.cmbResult = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReSend = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnAu = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgShow)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1053, 41);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "原因设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(928, 41);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "快递公司设置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgShow
            // 
            this.dgShow.AllowUserToAddRows = false;
            this.dgShow.AllowUserToDeleteRows = false;
            this.dgShow.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgShow.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsSelect,
            this.SeqNum,
            this.Status,
            this.Result,
            this.BuyersID,
            this.ShopName,
            this.RefundWay,
            this.RefundAlipayNo,
            this.Remark1,
            this.Remark2,
            this.Remark3,
            this.Remark4,
            this.AlipayNo,
            this.ChineseName,
            this.RefundAmount,
            this.RefundResoin,
            this.Remark});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgShow.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgShow.Location = new System.Drawing.Point(16, 78);
            this.dgShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgShow.Name = "dgShow";
            this.dgShow.RowTemplate.Height = 40;
            this.dgShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgShow.Size = new System.Drawing.Size(1132, 516);
            this.dgShow.TabIndex = 30;
            this.dgShow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgShow_KeyDown);
            // 
            // IsSelect
            // 
            this.IsSelect.DataPropertyName = "IsSelect";
            this.IsSelect.HeaderText = "勾选";
            this.IsSelect.Name = "IsSelect";
            this.IsSelect.Width = 40;
            // 
            // SeqNum
            // 
            this.SeqNum.DataPropertyName = "SeqNum";
            this.SeqNum.HeaderText = "序号";
            this.SeqNum.Name = "SeqNum";
            this.SeqNum.ReadOnly = true;
            this.SeqNum.Width = 40;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 60;
            // 
            // Result
            // 
            this.Result.DataPropertyName = "Result";
            this.Result.HeaderText = "处理结果";
            this.Result.Name = "Result";
            this.Result.Width = 80;
            // 
            // BuyersID
            // 
            this.BuyersID.DataPropertyName = "ExpressCompany";
            this.BuyersID.HeaderText = "快递公司";
            this.BuyersID.Name = "BuyersID";
            // 
            // ShopName
            // 
            this.ShopName.DataPropertyName = "ExpressNo";
            this.ShopName.HeaderText = "快递单号";
            this.ShopName.Name = "ShopName";
            // 
            // RefundWay
            // 
            this.RefundWay.DataPropertyName = "Resion";
            this.RefundWay.HeaderText = "原因";
            this.RefundWay.Name = "RefundWay";
            this.RefundWay.Width = 60;
            // 
            // RefundAlipayNo
            // 
            this.RefundAlipayNo.DataPropertyName = "BuyAdress";
            this.RefundAlipayNo.HeaderText = "买家地址";
            this.RefundAlipayNo.Name = "RefundAlipayNo";
            this.RefundAlipayNo.Width = 120;
            // 
            // Remark1
            // 
            this.Remark1.DataPropertyName = "Remark";
            this.Remark1.HeaderText = "回复1";
            this.Remark1.Name = "Remark1";
            // 
            // Remark2
            // 
            this.Remark2.DataPropertyName = "Remark2";
            this.Remark2.HeaderText = "回复2";
            this.Remark2.Name = "Remark2";
            // 
            // Remark3
            // 
            this.Remark3.DataPropertyName = "Remark3";
            this.Remark3.HeaderText = "回复3";
            this.Remark3.Name = "Remark3";
            // 
            // Remark4
            // 
            this.Remark4.DataPropertyName = "Remark4";
            this.Remark4.HeaderText = "回复4";
            this.Remark4.Name = "Remark4";
            // 
            // AlipayNo
            // 
            this.AlipayNo.DataPropertyName = "CreateTime";
            this.AlipayNo.HeaderText = "录入时间";
            this.AlipayNo.Name = "AlipayNo";
            // 
            // ChineseName
            // 
            this.ChineseName.DataPropertyName = "CreateUser";
            this.ChineseName.HeaderText = "录入人";
            this.ChineseName.Name = "ChineseName";
            this.ChineseName.Width = 70;
            // 
            // RefundAmount
            // 
            this.RefundAmount.DataPropertyName = "DealTime";
            this.RefundAmount.HeaderText = "处理时间";
            this.RefundAmount.Name = "RefundAmount";
            this.RefundAmount.ReadOnly = true;
            // 
            // RefundResoin
            // 
            this.RefundResoin.DataPropertyName = "DealUser";
            this.RefundResoin.HeaderText = "处理人";
            this.RefundResoin.Name = "RefundResoin";
            this.RefundResoin.ReadOnly = true;
            this.RefundResoin.Width = 70;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "FinishTime";
            this.Remark.HeaderText = "完结时间";
            this.Remark.Name = "Remark";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1048, 612);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 29);
            this.button3.TabIndex = 31;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(776, 41);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(67, 29);
            this.button4.TabIndex = 32;
            this.button4.Text = "查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "快递单号:";
            // 
            // txtExpressNo
            // 
            this.txtExpressNo.Location = new System.Drawing.Point(96, 12);
            this.txtExpressNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtExpressNo.Name = "txtExpressNo";
            this.txtExpressNo.Size = new System.Drawing.Size(141, 25);
            this.txtExpressNo.TabIndex = 34;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(333, 11);
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(141, 25);
            this.txtCompanyName.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "快递公司:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "状态:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(277, 45);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(103, 23);
            this.cmbStatus.TabIndex = 38;
            // 
            // cmbResion
            // 
            this.cmbResion.FormattingEnabled = true;
            this.cmbResion.Location = new System.Drawing.Point(96, 45);
            this.cmbResion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbResion.Name = "cmbResion";
            this.cmbResion.Size = new System.Drawing.Size(108, 23);
            this.cmbResion.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 39;
            this.label4.Text = "原因:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(710, 612);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 29);
            this.button5.TabIndex = 41;
            this.button5.Text = "录入";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // timePickTo
            // 
            this.timePickTo.Location = new System.Drawing.Point(841, 8);
            this.timePickTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timePickTo.Name = "timePickTo";
            this.timePickTo.Size = new System.Drawing.Size(181, 25);
            this.timePickTo.TabIndex = 47;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(799, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 15);
            this.label11.TabIndex = 46;
            this.label11.Text = "至:";
            // 
            // timePickFrom
            // 
            this.timePickFrom.Location = new System.Drawing.Point(617, 8);
            this.timePickFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timePickFrom.Name = "timePickFrom";
            this.timePickFrom.Size = new System.Drawing.Size(169, 25);
            this.timePickFrom.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(496, 14);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 44;
            this.label10.Text = "录入时间:";
            // 
            // btnDeal
            // 
            this.btnDeal.Location = new System.Drawing.Point(115, 612);
            this.btnDeal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeal.Name = "btnDeal";
            this.btnDeal.Size = new System.Drawing.Size(87, 29);
            this.btnDeal.TabIndex = 48;
            this.btnDeal.Text = "处理";
            this.btnDeal.UseVisualStyleBackColor = true;
            this.btnDeal.Click += new System.EventHandler(this.btnDeal_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(216, 612);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(87, 29);
            this.btnFinish.TabIndex = 49;
            this.btnFinish.Text = "完结";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(319, 612);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 29);
            this.button6.TabIndex = 50;
            this.button6.Text = "紧急";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(935, 612);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 29);
            this.button7.TabIndex = 51;
            this.button7.Text = "录入回复";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Location = new System.Drawing.Point(471, 45);
            this.txtCreateUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.Size = new System.Drawing.Size(92, 25);
            this.txtCreateUser.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(400, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 52;
            this.label5.Text = "录入人:";
            // 
            // cmbTime
            // 
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime.FormattingEnabled = true;
            this.cmbTime.Location = new System.Drawing.Point(495, 8);
            this.cmbTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTime.Name = "cmbTime";
            this.cmbTime.Size = new System.Drawing.Size(97, 23);
            this.cmbTime.TabIndex = 54;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(16, 612);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(87, 29);
            this.btnSelect.TabIndex = 55;
            this.btnSelect.Text = "勾选";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(852, 41);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(67, 29);
            this.btnExport.TabIndex = 56;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cmbResult
            // 
            this.cmbResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResult.FormattingEnabled = true;
            this.cmbResult.Location = new System.Drawing.Point(659, 45);
            this.cmbResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.Size = new System.Drawing.Size(103, 23);
            this.cmbResult.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(579, 50);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 57;
            this.label6.Text = "处理结果:";
            // 
            // btnReSend
            // 
            this.btnReSend.Location = new System.Drawing.Point(531, 612);
            this.btnReSend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReSend.Name = "btnReSend";
            this.btnReSend.Size = new System.Drawing.Size(87, 29);
            this.btnReSend.TabIndex = 60;
            this.btnReSend.Text = "已补发";
            this.btnReSend.UseVisualStyleBackColor = true;
            this.btnReSend.Click += new System.EventHandler(this.btnReSend_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Location = new System.Drawing.Point(428, 612);
            this.btnRefund.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(87, 29);
            this.btnRefund.TabIndex = 59;
            this.btnRefund.Text = "已退款";
            this.btnRefund.UseVisualStyleBackColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnAu
            // 
            this.btnAu.Location = new System.Drawing.Point(1053, 9);
            this.btnAu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAu.Name = "btnAu";
            this.btnAu.Size = new System.Drawing.Size(100, 29);
            this.btnAu.TabIndex = 61;
            this.btnAu.Text = "权限设置";
            this.btnAu.UseVisualStyleBackColor = true;
            this.btnAu.Click += new System.EventHandler(this.btnAu_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(824, 612);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 29);
            this.btnDelete.TabIndex = 62;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ExpressReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1164, 656);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAu);
            this.Controls.Add(this.btnReSend);
            this.Controls.Add(this.btnRefund);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.cmbTime);
            this.Controls.Add(this.txtCreateUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnDeal);
            this.Controls.Add(this.timePickTo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.timePickFrom);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cmbResion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExpressNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dgShow);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ExpressReceive";
            this.Text = "快递签收管理";
            ((System.ComponentModel.ISupportInitialize)(this.dgShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgShow;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExpressNo;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbResion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DateTimePicker timePickTo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker timePickFrom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDeal;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtCreateUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTime;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cmbResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyersID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShopName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefundWay;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefundAlipayNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark4;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlipayNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChineseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefundAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefundResoin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.Button btnReSend;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.Button btnAu;
        private System.Windows.Forms.Button btnDelete;
    }
}