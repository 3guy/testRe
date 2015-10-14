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
    public partial class UpdateGame : Form
    {
        string id;
        string gamename;
        public UpdateGame(string id,string gamename)
        {
            this.id = id;
            this.gamename = gamename;
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
                MessageBox.Show("必须填写游戏名！");
                return;
            }
            bool isok = GameStore.Update(id,textBoxX1.Text);
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

        private void UpdateGame_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = gamename;
        }
    }
}
