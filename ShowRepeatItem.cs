using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderManage
{
    public partial class ShowRepeatItem : Form
    {
        public ShowRepeatItem(string text)
        {
            InitializeComponent();
            this.textBox1.Text = text;
        }
    }
}
