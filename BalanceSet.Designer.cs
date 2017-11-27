namespace OrderManage
{
    partial class BalanceSet
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBalance = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAilpayNo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 21);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "余额:";
            // 
            // btnBalance
            // 
            this.btnBalance.Location = new System.Drawing.Point(144, 109);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(61, 23);
            this.btnBalance.TabIndex = 84;
            this.btnBalance.Text = "关闭";
            this.btnBalance.UseVisualStyleBackColor = true;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(67, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 85;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 87;
            this.label2.Text = "支付宝帐号:";
            // 
            // cmbAilpayNo
            // 
            this.cmbAilpayNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAilpayNo.FormattingEnabled = true;
            this.cmbAilpayNo.Location = new System.Drawing.Point(87, 27);
            this.cmbAilpayNo.Name = "cmbAilpayNo";
            this.cmbAilpayNo.Size = new System.Drawing.Size(118, 20);
            this.cmbAilpayNo.TabIndex = 88;
            this.cmbAilpayNo.SelectedValueChanged += new System.EventHandler(this.cmbAilpayNo_SelectedValueChanged);
            // 
            // BalanceSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 169);
            this.Controls.Add(this.cmbAilpayNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBalance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "BalanceSet";
            this.Text = "余额设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBalance;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAilpayNo;
    }
}