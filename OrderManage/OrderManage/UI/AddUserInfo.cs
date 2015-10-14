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
    public partial class AddUserInfo : Form
    {
        public AddUserInfo()
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
            if (textBoxX1.Text == "" | textBoxX2.Text == "" | textBoxX3.Text == "" | textBoxX4.Text == "")
            {
                MessageBox.Show("有属性还未填写！");
                return;
            }
            string userid = textBoxX1.Text;
            string pwd = textBoxX2.Text;
            string name = textBoxX4.Text;
            string role = comboBoxEx1.Text;
            bool isok = UserStore.Add(userid, pwd, name, role);
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

        private void AddUserInfo_Load(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = 0;
        }
    }
}
