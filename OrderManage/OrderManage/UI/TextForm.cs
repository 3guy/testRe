using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;

using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    public partial class TextForm : Form
    {
        public TextForm()
        {
            InitializeComponent();
        }

        public TextForm(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        public TextForm(string text,string title)
        {
            InitializeComponent();
            textBoxX1.Text = text;
            this.Text = title;
        }

        /// <summary>
        /// 自定义属于用于返回值
        /// </summary>
        public string ReturnValue
        {
            get
            {
                return textBoxX1.Text.Trim();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("信息还是空的！");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tsbRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*"; //过滤文件类型
            fd.InitialDirectory = Application.StartupPath;//设定初始目录
            fd.ShowReadOnly = true; //设定文件是否只读
            DialogResult r = fd.ShowDialog();

            if (r == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(fd.FileName, System.Text.Encoding.Default);
                this.textBoxX1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip.SourceControl.Select();//先获取焦点，防止点两下才运行
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip.SourceControl;
            rtb.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip.SourceControl.Select();
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip.SourceControl;
            rtb.Paste();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip.SourceControl.Select();
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip.SourceControl;
            rtb.Cut();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip.SourceControl.Select();
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip.SourceControl;
            rtb.SelectedText = "";
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip.SourceControl.Select();
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip.SourceControl;
            rtb.SelectAll();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip.SourceControl.Select();
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip.SourceControl;
            rtb.Undo();
        }
    }
}
