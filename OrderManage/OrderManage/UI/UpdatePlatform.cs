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
    public partial class UpdatePlatform : Form
    {
        string 账号 = "";
        string id = "";

        public UpdatePlatform(string id, string 账号)
        {
            InitializeComponent();
            this.账号 = 账号;
            this.id = id;
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
            bool isok = PlatformStore.Update(id, userid, pwd);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败！");
            }
        }

        private void UpdateUserInfo_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = 账号;
        }
    }
}
