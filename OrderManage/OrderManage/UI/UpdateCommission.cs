using OrderManage.Common;
using OrderManage.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    public partial class UpdateCommission : Form
    {
        string gameName;
        string type;
        string price;
        string id;
        public UpdateCommission(string gameName,string type,string price,string id)
        {
            this.gameName = gameName;
            this.type = type;
            this.price = price;
            this.id = id;
            InitializeComponent();
        }

        private void UpdateCommission_Load(object sender, EventArgs e)
        {
            DataTable yxdt = GameStore.GetAll();
            DataRow yxdr = yxdt.NewRow();
            yxdr["id"] = "0";
            yxdr["gamename"] = "---请选择---";
            yxdt.Rows.InsertAt(yxdr, 0);
            comboBoxEx1.DataSource = yxdt;
            comboBoxEx1.DisplayMember = "gamename";
            comboBoxEx1.ValueMember = "id";

            comboBoxEx1.Text = gameName;
            comboBoxEx2.Text = type;
            textBoxX1.Text = price;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedIndex == 0)
            {
                MessageBox.Show("请选择游戏!");
                return;
            }
            if (comboBoxEx2.SelectedIndex == 0)
            {
                MessageBox.Show("请选择账号类型!");
                return;
            }
            if (!Validation.CheckPrice(textBoxX1.Text))
            {
                MessageBox.Show("提成输入错误!");
                return;
            }
            bool isok = CommissionStore.Update(id,comboBoxEx1.SelectedValue.ToString(), comboBoxEx2.Text,textBoxX1.Text);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                this.Close();
            }
        }
    }
}
