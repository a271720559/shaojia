namespace OrderManage
{
    partial class ExpressInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpressInput));
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExpressCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExpressNo = new System.Windows.Forms.TextBox();
            this.cmbResion = new System.Windows.Forms.ComboBox();
            this.txtCreateUser = new System.Windows.Forms.TextBox();
            this.txtBuyAdress = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtBuyID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(33, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 72;
            this.label14.Text = "买家地址:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(249, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 70;
            this.label7.Text = "录入人:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 66;
            this.label3.Text = "原因:";
            // 
            // txtExpressCompany
            // 
            this.txtExpressCompany.Location = new System.Drawing.Point(300, 21);
            this.txtExpressCompany.Name = "txtExpressCompany";
            this.txtExpressCompany.Size = new System.Drawing.Size(100, 21);
            this.txtExpressCompany.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 65;
            this.label2.Text = "快递公司:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 64;
            this.label1.Text = "快递单号:";
            // 
            // txtExpressNo
            // 
            this.txtExpressNo.Location = new System.Drawing.Point(101, 21);
            this.txtExpressNo.Name = "txtExpressNo";
            this.txtExpressNo.Size = new System.Drawing.Size(116, 21);
            this.txtExpressNo.TabIndex = 1;
            this.txtExpressNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpressNo_KeyDown);
            this.txtExpressNo.Leave += new System.EventHandler(this.txtExpressNo_Leave);
            // 
            // cmbResion
            // 
            this.cmbResion.FormattingEnabled = true;
            this.cmbResion.Location = new System.Drawing.Point(101, 55);
            this.cmbResion.Name = "cmbResion";
            this.cmbResion.Size = new System.Drawing.Size(116, 20);
            this.cmbResion.TabIndex = 75;
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Location = new System.Drawing.Point(300, 56);
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.ReadOnly = true;
            this.txtCreateUser.Size = new System.Drawing.Size(100, 21);
            this.txtCreateUser.TabIndex = 76;
            // 
            // txtBuyAdress
            // 
            this.txtBuyAdress.Location = new System.Drawing.Point(101, 116);
            this.txtBuyAdress.Multiline = true;
            this.txtBuyAdress.Name = "txtBuyAdress";
            this.txtBuyAdress.Size = new System.Drawing.Size(319, 205);
            this.txtBuyAdress.TabIndex = 77;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(345, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 78;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(264, 343);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 79;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(183, 343);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 80;
            this.button3.Text = "识别地址";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtBuyID
            // 
            this.txtBuyID.Location = new System.Drawing.Point(101, 85);
            this.txtBuyID.Name = "txtBuyID";
            this.txtBuyID.Size = new System.Drawing.Size(116, 21);
            this.txtBuyID.TabIndex = 81;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 82;
            this.label4.Text = "买家ID:";
            // 
            // ExpressInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(471, 388);
            this.Controls.Add(this.txtBuyID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBuyAdress);
            this.Controls.Add(this.txtCreateUser);
            this.Controls.Add(this.cmbResion);
            this.Controls.Add(this.txtExpressNo);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtExpressCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ExpressInput";
            this.Text = "快递信息录入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExpressCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExpressNo;
        private System.Windows.Forms.ComboBox cmbResion;
        private System.Windows.Forms.TextBox txtCreateUser;
        private System.Windows.Forms.TextBox txtBuyAdress;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtBuyID;
        private System.Windows.Forms.Label label4;
    }
}