using NetEntity;
using ProcessOrder.Common;
using ProcessOrder.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProcessOrder.UI
{
    public partial class CompleteOrder : Form
    {
        string 订单详情;
        string 备注;
        string poid;
        string oid;
        NetClient nc;

        string note;
        public CompleteOrder(string poid, string 订单详情, string 备注,string oid, NetClient nc)
        {
            this.订单详情 = 订单详情;
            this.备注 = 备注;
            this.poid = poid;
            this.oid = oid;
            this.nc = nc;

            InitializeComponent();
        }

        private void SellForm_Load(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)nc.SendObj(new NetCommand("获取库存", ""));
            DataRow dr = dt.NewRow();
            dr["id"] = "0";
            dr["appid"] = "---请选择appid---";
            dt.Rows.InsertAt(dr, 0);
            comboBoxEx1.DataSource = dt;
            comboBoxEx1.DisplayMember = "appid";
            comboBoxEx1.ValueMember = "id";

            textBoxX3.Text = 订单详情;
            textBoxX4.Text = 备注;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            //if (textBoxX1.Text == "")
            //{
            //    MessageBox.Show("价格不能为空！");
            //    return;
            //}
            if (comboBoxEx1.SelectedIndex == 0)
            {
                MessageBox.Show("请选择appid！");
                return;
            }
            if (MessageBox.Show("确认取消充值此订单吗？此操作不可恢复", "取消充值", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MessageBox.Show("再次确认取消充值此订单吗？此操作不可恢复", "取消充值", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string[] data = new string[6];
                    data[0] = comboBoxEx1.SelectedValue.ToString();
                    data[1] = "0";
                    data[2] = textBoxX2.Text;
                    data[3] = poid;
                    data[4] = oid;
                    data[5] = "取消充值";
                    bool isok = (bool)nc.SendObj(new NetCommand("完成订单", data));
                    if (isok)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("完成订单失败！请联系管理员！");
                    }
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("价格不能为空！");
                return;
            }
            if (comboBoxEx1.SelectedIndex == 0)
            {
                MessageBox.Show("请选择appid！");
                return;
            }
            
            string []data=new string [6];
            data[0] = comboBoxEx1.SelectedValue.ToString();
            data[1] = textBoxX1.Text;
            //data[2] = textBoxX2.Text;
            data[3] = poid;
            data[4] = oid;
            data[5] = "充值成功";


            textBoxX2.Text = textBoxX2.Text.Replace("自动获取面值", "");
            note += "appid：" + comboBoxEx1.Text + "\r\n";
            note += textBoxX2.Text + "\r\n";
            textBoxX2.Text = note;
            
            data[2] = textBoxX2.Text;
            bool isok = (bool)nc.SendObj(new NetCommand("完成订单", data));
            if (isok)
            {
                //自动发货();
                this.Close();
            }
            else
            {
                MessageBox.Show("完成订单失败！请联系管理员！");
            }
        }

        private void 自动发货()
        { 
            string 详情=textBoxX3.Text;
            if (详情.IndexOf("[交易猫账号：")>=0)
            {
                string 账号 = Global.jymhlist[0].截取文本(详情, "[交易猫账号：", "]", 0);
                string sid = Global.jymhlist[0].截取文本(详情, "[ID：", "]", 0);
                //MessageBox.Show("账号:"+账号);
                //MessageBox.Show("sid:" + sid);
                for (int i = 0; i < Global.jymhlist.Count; i++)
                {
                    if (Global.jymhlist[i].账号 == 账号)
                    {
                        //MessageBox.Show("账号为:" + Global.jymhlist[i].账号);
                        Global.jymhlist[i].发货(sid);
                        ArrayList al = Global.jymhlist[i].获取订单列表();
                        for (int j = 0; j < al.Count; j++)
                        {
                            string ssid = ((string[])al[j])[0];
                            if (sid == ssid)
                            {
                                MessageBox.Show("自动发货异常！请手动点击！");
                                return;
                            }
                        }
                        return;
                    }
                }
            }
        }

        internal int ConvertToInt32(object objValue, int defaultValue)
        {
            int result;
            try
            {
                result = Convert.ToInt32(objValue);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        internal string 截取文本(string AText, string ATag1, string ATag2, int AOffset)
        {
            string sValue = "";

            int i1;
            int i2;

            try
            {
                i1 = AText.IndexOf(ATag1);
                if (i1 != -1)
                {
                    i2 = AText.IndexOf(ATag2, i1 + ATag1.Length);
                    if (i2 != -1)
                    {
                        i1 += AOffset;
                        sValue = AText.Substring(i1 + ATag1.Length, i2 - i1 - ATag1.Length);
                    }
                }
            }
            catch (Exception)
            {

            }

            return sValue.Trim();
        }

        internal string[] 截取文本(string AText, string ATag1, string ATag2, string ATag3, int AOffset)
        {
            List<string> strList = new List<string>();

            string sStr = "";
            int iPos = -1;
            int iEnd = 0;
            do
            {
                iPos = AText.IndexOf(ATag1, iEnd);
                if (iPos != -1)  //找到了
                {
                    iEnd = AText.IndexOf(ATag2, iPos + ATag1.Length);
                    if (iEnd == -1)
                    {
                        iEnd = AText.IndexOf(ATag3, iPos + ATag1.Length);
                        if (iEnd == 1) { break; }
                    }
                    sStr = AText.Substring(iPos + ATag1.Length + AOffset, iEnd - iPos - ATag1.Length - AOffset);
                    strList.Add(sStr);
                }
            }
            while (iPos > -1);

            return strList.ToArray();
        }

        private void comboBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedIndex == 0)
            {
                return;
            }
            checkBoxX1.Checked = false;
            //if (textBoxX4.Text == "系统自动采集")
            //{
                string price = "";
                price = 截取文本(textBoxX3.Text, "商品原价：", "\r\n", 0);
                string value = nc.SendObj(new NetCommand("获取面值", new string[] { comboBoxEx1.SelectedValue.ToString(), price })).ToString();
                //MessageBox.Show(price + "|" + value + "|" + comboBoxEx1.SelectedValue.ToString());
                if (value == "null" | value == "")
                {
                    textBoxX1.Text = "";
                    note = "";
                    textBoxX1.Enabled = true;
                }
                else
                {
                    textBoxX1.Enabled = false;

                    int 件数 = int.Parse(截取文本(textBoxX3.Text, "购买数量：", "件",0).Trim());
                    textBoxX1.Text = (float.Parse(value)*件数).ToString();
                    note = "自动获取面值\r\n";
                }
            //}
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
                textBoxX1.Text = "";
                note = "";
                textBoxX1.Enabled = true;
                note += "手动填写价格\r\n";
            }
            else
            {
                string price = "";
                price = 截取文本(textBoxX3.Text, "商品原价：", "\r\n", 0);
                string value = nc.SendObj(new NetCommand("获取面值", new string[] { comboBoxEx1.SelectedValue.ToString(), price })).ToString();
                //MessageBox.Show(price + "|" + value + "|" + comboBoxEx1.SelectedValue.ToString());
                if (value != "null" && value != "")
                {
                    textBoxX1.Enabled = false;

                    int 件数 = int.Parse(截取文本(textBoxX3.Text, "购买数量：", "件", 0).Trim());
                    textBoxX1.Text = (float.Parse(value) * 件数).ToString();
                    note = "自动获取面值\r\n";
                }
            }
        }
    }
}
