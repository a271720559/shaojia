using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace OrderManage
{
    public partial class ShowPicture : Form
    {
        SqlConn sqlConn = new SqlConn();
        int locationX = 0;
        int locationY = 0;
        public ShowPicture(int? no)
        {
            InitializeComponent();
            foreach (var control in this.Controls)
            {
                if (control is Label)
                {
                    var lb = control as Label;
                    lb.BackColor = Color.Transparent;
                }
            }
            if (no.HasValue)
            {
                this.timeRefoundFrom.Value = DateTime.Parse("2017-01-01");
                this.tbNo.Text = no.Value.ToString();
                Search();
            }
        }
        private string picDirPath = null;                        //图片路径
        private List<string> imagePathList = new List<string>(); //获取列表图片路径
        private int index;
        private void btnSearch_Click(object sender, EventArgs e)
        {
             //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
             //DialogResult result = folderBrowserDialog.ShowDialog();
             //string folderDirPath = string.Empty;
             //if (result == DialogResult.OK)
             //{
             //    //获取用户选择的文件夹路径
             //    folderDirPath = folderBrowserDialog.SelectedPath;

             //    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
             //    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
             //    FileInfo[] fileInfo = dir.GetFiles("*.JPG");
             //    this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
             //    for (int i = 0; i < fileInfo.Length; i++)
             //    {
             //        //获取文件完整目录
             //        picDirPath = fileInfo[i].FullName;
             //        //记录图片源路径 双击显示图片时使用
             //        imagePathList.Add(picDirPath);
             //        //图片加载到ImageList控件和imageList图片列表
             //        this.imageList1.Images.Add(Image.FromFile(picDirPath));
             //    }

             //    //显示文件列表
             //    this.listView1.Items.Clear();
             //    this.listView1.LargeImageList = this.imageList1;
             //    this.listView1.View = View.LargeIcon;        //大图标显示
             //    //imageList1.ImageSize = new Size(40, 40);   //不能设置ImageList的图像大小 属性处更改

             //    //开始绑定
             //    this.listView1.BeginUpdate();
             //    //增加图片至ListView控件中
             //    for (int i = 0; i < imageList1.Images.Count; i++)
             //    {
             //        ListViewItem lvi = new ListViewItem();
             //        lvi.ImageIndex = i;
             //        lvi.Text = "pic" + i;
             //        this.listView1.Items.Add(lvi);
             //    }
             //    this.listView1.EndUpdate();
             //}
            Search();
        }

        private void Search()
        {
            this.tip.Text = "图片查询中,请稍等";
            this.tip.Visible = true;
            this.tip.TabIndex = 1;
            FontFamily ff = new FontFamily(this.tip.Font.Name);
            var fontStyle = this.tip.Font.Style;
            this.tip.Font = new Font(ff, 30, fontStyle, GraphicsUnit.World);
            this.tip.BackColor = Color.Transparent;
            System.Windows.Forms.Application.DoEvents();
            string command = string.Empty;
            command = "select * from PicPath where 1=1";
            if (!string.IsNullOrWhiteSpace(this.tbNo.Text))
                command += " and no='"+this.tbNo.Text+"'";
            if (this.timeRefoundFrom.Value != null)
            {
                var timeFrom = this.timeRefoundFrom.Value;
                timeFrom = timeFrom.AddHours(-this.timeRefoundFrom.Value.Hour).AddMinutes(-this.timeRefoundFrom.Value.Minute).AddSeconds(-this.timeRefoundFrom.Value.Second);

                command += " and convert(datetime,uploadTime) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (this.timeRefoundTo.Value != null)
            {
                var timeTo = this.timeRefoundTo.Value;
                timeTo = timeTo.AddHours(-this.timeRefoundTo.Value.Hour).AddMinutes(-this.timeRefoundTo.Value.Minute).AddSeconds(-this.timeRefoundTo.Value.Second);
                timeTo = timeTo.AddHours(23).AddMinutes(59).AddSeconds(59);
                command += " and convert(datetime,uploadTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            DataTable dt = sqlConn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                List<PicShowEntry> picList = new List<PicShowEntry>();
                List<Image> imageList = new List<Image>();
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    PicShowEntry entry = new PicShowEntry();
                    entry.no = int.Parse(dt.Rows[i]["no"].ToString());
                    entry.picPath = dt.Rows[i]["path"].ToString();
                    entry.picName = dt.Rows[i]["picName"].ToString();
                    picList.Add(entry);
                }
                //var seachList = 
                int seq = 1;
                locationX = 0;
                locationY = 0;
                this.panel1.Controls.Clear();
                foreach(var picEntry in picList)
                {
                    picDirPath = picEntry.picPath;
                    //记录图片源路径 双击显示图片时使用
                    imagePathList.Add(picDirPath);
                    //Image image = Image.FromFile(picDirPath);

                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(picDirPath);
                    //WebResponse response = request.GetResponse();//获得响应
                    //Image image = Image.FromStream(response.GetResponseStream());

                    System.Net.WebClient web = new System.Net.WebClient();
                    byte[] buffer = web.DownloadData(picDirPath);
                    web.Dispose();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
                    //Image image = Image.FromStream(ms);


                    //image.Tag = picEntry.picName;
                    ////图片加载到ImageList控件和imageList图片列表
                    //this.imageList1.Images.Add(image);
                    //imageList.Add(image);




                    //Panel pl = new Panel();
                    //PictureBox picBox = new PictureBox();
                    //picBox.Image = Image.FromStream(ms);
                    //picBox.Width = 580;
                    //picBox.Height = 520;
                    //picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pl.Controls.Add(picBox);
                    //Label lb = new Label();
                    //lb.Text = picEntry.picName;
                    //lb.Width = 200;
                    //lb.Location = new Point(200, 521);
                    //pl.Controls.Add(lb);
                    //pl.Width = 600;
                    //pl.Height = 540;
                    //pl.Location = new Point(locationX+5,locationY+5);
                    //locationX += 620;
                    //if (seq % 2 == 0)
                    //{
                    //    locationY += 560;
                    //    locationX = 0;
                    //}
                    //pl.BorderStyle = BorderStyle.FixedSingle;
                    //this.panel1.Controls.Add(pl);
                    //seq++;


                    //Panel pl = new Panel();
                    PictureBox picBox = new PictureBox();
                    picBox.Image = Image.FromStream(ms);
                    picBox.ImageLocation = picDirPath;
                    picBox.Width = 580;
                    picBox.Height = 520;
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    
                    Label lb = new Label();
                    lb.Text = picEntry.picName;
                    lb.Width = 200;
                    lb.Location = new Point(200, 521);
                    
                    //pl.Width = 600;
                    //pl.Height = 540;
                    //pl.Location = new Point(locationX + 5, locationY + 5);
                    picBox.Location = new Point(locationX + 5, locationY + 5);
                    lb.Location = new Point(locationX + 230, locationY + 530);
                    locationX += 620;
                    if (seq % 2 == 0)
                    {
                        locationY += 560;
                        locationX = 0;
                    }
                    picBox.MouseDoubleClick += new MouseEventHandler(pictureBox1_MouseDoubleClick);
                    this.panel1.Controls.Add(picBox);
                    this.panel1.Controls.Add(lb);
                    //pl.BorderStyle = BorderStyle.FixedSingle;
                    //this.panel1.Controls.Add(pl);
                    seq++;
                }
                //this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
                

                ////显示文件列表
                //this.listView1.Items.Clear();
                //this.listView1.LargeImageList = this.imageList1;
                //this.listView1.View = View.LargeIcon;        //大图标显示
               

                ////开始绑定
                //this.listView1.BeginUpdate();
                ////增加图片至ListView控件中
                //for (int i = 0; i < imageList1.Images.Count; i++)
                //{
                //    ListViewItem lvi = new ListViewItem();
                //    lvi.ImageIndex = i;
                //    lvi.Text = picList[i].picName;
                //    this.listView1.Items.Add(lvi);
                //}
                //this.listView1.EndUpdate();
            }
            this.tip.Visible = false;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;
            //采用索引方式 imagePathList记录图片真实路径
            index = this.listView1.SelectedItems[0].Index;
            //显示图片
            //this.pictureBox1.Image = Image.FromFile(imagePathList[index]);
            Process.Start(imagePathList[index]);
            //图片被拉伸或收缩适合pictureBox大小
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var picBox = sender as PictureBox;
            if (picBox != null)
                Process.Start(picBox.ImageLocation);
        }

        private void ShowPicture_Load(object sender, EventArgs e)
        {
            this.MouseWheel += FormSample_MouseWheel;
        }
        void FormSample_MouseWheel(object sender, MouseEventArgs e)
        {
            //获取光标位置
            Point mousePoint = new Point(e.X, e.Y);
            //换算成相对本窗体的位置
            mousePoint.Offset(this.Location.X, this.Location.Y);
            //判断是否在panel内
            if (panel1.RectangleToScreen(panel1.DisplayRectangle).Contains(mousePoint))
            {
                //滚动
                panel1.AutoScrollPosition = new Point(0, panel1.VerticalScroll.Value - e.Delta);
            }
        }
    }
}
