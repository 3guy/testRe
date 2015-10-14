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
    public partial class AddInventory : Form
    {
        public AddInventory()
        {
            InitializeComponent();
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
            string money = textBoxX2.Text;

            bool isok = InventoryStore.Add(appid,money);
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
