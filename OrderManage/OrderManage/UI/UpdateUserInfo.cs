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
    public partial class UpdateUserInfo : Form
    {
        string 账号 = "";
        string 姓名 = "";
        string 角色 = "";
        string id = "";

        public UpdateUserInfo(string id,string 账号,string 姓名,string 角色)
        {
            InitializeComponent();
            this.账号 = 账号;
            this.姓名 = 姓名;
            this.角色 = 角色;
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
            if (textBoxX1.Text == "" | textBoxX2.Text == "" | textBoxX3.Text == "" | textBoxX4.Text == "")
            {
                MessageBox.Show("有属性还未填写！");
                return;
            }
            string userid = textBoxX1.Text;
            string pwd = textBoxX2.Text;
            string name = textBoxX4.Text;
            string role = comboBoxEx1.Text;
            bool isok = UserStore.Update(id,userid, pwd, name, role);
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
            //for (int i = 0; i < comboBoxEx1.Items.Count; i++)
            //{
            //    if (comboBoxEx1.Items[i].ToString() == 角色)
            //        comboBoxEx1.SelectedIndex = i;
            //}
            comboBoxEx1.Text = 角色;
            textBoxX1.Text = 账号;
            textBoxX4.Text = 姓名;
        }
    }
}
