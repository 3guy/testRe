using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    internal partial class WebLogin : Form
    {
        internal JiaoYiMaoHelper jymh;
        internal WebLogin(JiaoYiMaoHelper jymh)
        {
            this.jymh = jymh;
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text != "")
            {
                string[] res = jymh.校验验证码(textBoxX1.Text);
                bool isok = bool.Parse(res[0]);
                if (isok)
                {
                    jymh.code = textBoxX1.Text;
                    bool islogin=jymh.登录();
                    if (islogin)
                    {
                        this.Close();
                        //MessageBox.Show("登录成功！");
                    }
                    else
                    {
                        this.pictureBox1.Image = jymh.重新获取验证码();
                        MessageBox.Show("登录失败！");
                    }
                }
                else
                {
                    MessageBox.Show(res[1]);
                    //this.pictureBox1.Image = jymh.获取验证码();
                    this.pictureBox1.Image = jymh.重新获取验证码();
                }
            }
        }

        private void WebLogin_Load(object sender, EventArgs e)
        {
            Bitmap bmp = jymh.获取验证码();
            this.pictureBox1.Image = bmp;
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pictureBox1.Image = jymh.重新获取验证码();
        }

    }
}
