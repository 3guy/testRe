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
    public partial class UpdateInventory : Form
    {
        string appid = "";
        string money = "";
        string id = "";
        string state="";

        public UpdateInventory(string id, string appid, string money,string state)
        {
            InitializeComponent();
            this.comboBoxEx1.SelectedIndex = 0;
            this.appid = appid;
            this.money = money;
            this.id = id;
            this.state = state;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("appid不能为空！");
                return;
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("余额不能为空！");
                return;
            }

            string appid = textBoxX1.Text;
            //string money = textBoxX2.Text;
            //MessageBox.Show(float.Parse(money) + float.Parse(textBoxX2.Text));
            bool isok = InventoryStore.Update(id, appid, (float.Parse(money) + float.Parse(textBoxX2.Text)).ToString(), comboBoxEx1.Text);
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
            textBoxX1.Text = appid;
            textBoxX2.Text = money;
            comboBoxEx1.Text = state;
        }
    }
}
