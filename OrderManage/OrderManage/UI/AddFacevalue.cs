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
    public partial class AddFacevalue : Form
    {
        public AddFacevalue()
        {
            InitializeComponent();
        }

        private void AddFacevalue_Load(object sender, EventArgs e)
        {
            DataTable dt = InventoryStore.GetAllByState();
            DataRow dr = dt.NewRow();
            dr["id"] = "0";
            dr["appid"] = "---请选择appid---";
            dt.Rows.InsertAt(dr, 0);
            comboBoxEx1.DataSource = dt;
            comboBoxEx1.DisplayMember = "appid";
            comboBoxEx1.ValueMember = "id";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedIndex == 0)
            {
                MessageBox.Show("请选择appid！");
                return;
            }
            if (textBoxX3.Text=="")
            {
                MessageBox.Show("请填写价格！");
                return;
            }
            if (textBoxX4.Text == "")
            {
                MessageBox.Show("请填写面值！");
                return;
            }

            bool isok=FacevalueStore.Add(comboBoxEx1.SelectedValue.ToString(), textBoxX2.Text, textBoxX3.Text, textBoxX4.Text);
            if (isok)
            {
                MessageBox.Show("添加成功！");
                this.Close();
            }

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
