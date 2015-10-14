using NetEntity;
using OrderManage.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderManage
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == ""|textBoxX2.Text=="")
            {
                MessageBox.Show("用户名或密码不能为空！");
                return;
            }
            //string id = nc.SendObj(new NetCommand("login", textBoxX1.Text + ":" + textBoxX2.Text + ":下单员")).ToString();
            string[] info = UserStore.Login(textBoxX1.Text, textBoxX2.Text, "管理员");
            if (info == null)
            {
                MessageBox.Show("登陆失败！");
                return;
            }
            else
            {
                MessageBox.Show("登陆成功！");
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            
        }

        private void textBoxX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonX1_Click(sender, e);
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Global.notifyIcon.Dispose();
            //System.Environment.Exit(System.Environment.ExitCode);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
