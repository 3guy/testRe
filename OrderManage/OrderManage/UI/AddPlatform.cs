using OrderManage.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    public partial class AddPlatform : Form
    {
        public AddPlatform()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX2.Text != textBoxX3.Text)
            {
                MessageBox.Show("两次输入的密码不一样！");
                return;
            }
            if (textBoxX1.Text == "" | textBoxX2.Text == "" | textBoxX3.Text == "" )
            {
                MessageBox.Show("有属性还未填写！");
                return;
            }
            string userid = textBoxX1.Text;
            string pwd = textBoxX2.Text;

            bool isok = PlatformStore.Add(userid, pwd);
            if (isok)
            {
                MessageBox.Show("添加成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
}
