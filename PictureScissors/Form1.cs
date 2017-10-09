using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PictureScissors
{
    public partial class Form1 : Form
    {
        List<string> imgList = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            lbStatus.Text = "";
            btnChooseFile.Enabled = rb2.Checked;
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1.Checked)
            {
                imgList.Clear();
                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
                FileInfo[] files = di.GetFiles();
                foreach (FileInfo fi in files)
                {
                    string ext = fi.Extension.ToLower();
                    if(ext.Contains("bmp") || ext.Contains("jpg") || ext.Contains("png") || ext.Contains("gif"))
                        imgList.Add(fi.FullName);
                }
                lbStatus.Text = string.Format("当前文件夹中有{0}个图片！", imgList.Count);
            }
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imgList.Clear();
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    imgList.Add(openFileDialog.FileNames[i]);
                }
                lbStatus.Text = string.Format("已选择{0}个图片！", openFileDialog.FileNames.Length);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!Validation())
                return;
        }

        bool Validation()
        {
            if (imgList.Count == 0)
            {
                MessageBox.Show("请先选择图片！", "图片剪刀", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            int w, h;
            int.TryParse(tbWidth.Text, out w);
            int.TryParse(tbHeight.Text, out h);

            if (w == 0 || h == 0)
            {
                MessageBox.Show("长度或宽度不能为零！", "图片剪刀", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (rbPercent.Checked && (w>100 || h>100))
            {
                MessageBox.Show("百分数必须在1-100之间！", "图片剪刀", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
    }
}
