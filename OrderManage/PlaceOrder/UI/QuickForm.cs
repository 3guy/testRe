using NetEntity;
using PlaceOrder.BLL;
using PlaceOrder.Common;
using PlaceOrder.Net;
using PlaceOrder.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PlaceOrder.UI
{

    public partial class QuickForm : Form
    {
        NetClient nc;
        WaitCallback duihuacall;
        ActionFactory actionf;
        internal static string currentdaqu = "";

        public QuickForm()
        {
            InitializeComponent();
            nc = new NetClient();
            ThreadPool.SetMaxThreads(50, 50);
            ThreadPool.SetMinThreads(50, 50);
            Control.CheckForIllegalCrossThreadCalls = false;
            dataGridView1.AutoGenerateColumns = false;
        }

        private void QuickForm_Load(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = 0;
            Global.notifyIcon = this.notifyIcon1;
            HookKeyPress.keyPressEvent += HookKeyPress_keyPressEvent;
            HookKeyPress.Hook_Start();
            nc.duiHuaEvent += nc_duiHuaEvent;
            nc.shiQuLianJieEvent += nc_shiQuLianJieEvent;
            nc.Start();
            try
            {
                actionf = new ActionFactory(this, nc);
                duihuacall = new WaitCallback(actionf.DoAction);

                LoginForm lf = new LoginForm(nc);
                DialogResult dr=lf.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    刷新所属游戏();
                    comboBoxEx3.SelectedIndex = 0;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(BindData));
                }
                else
                {
                    System.Environment.Exit(System.Environment.ExitCode);
                }

            }
            catch (Exception ex)
            {
                ImportDataLog.WriteLog("ReceiveData:" + ex.Message + "\r\n" +
"触发异常方法：" + ex.TargetSite + "\r\n" +
"异常详细信息" + ex.StackTrace + "\r\n");
            }

        }

        void BindData(object obj)
        {
            DataTable dt = (DataTable)nc.SendObj(new NetCommand("查询订单详情", ""));
            UIHelper.查询订单详情(this, dt);
        }

        void 刷新所属游戏()
        {
            DataTable yxdt  = (DataTable)nc.SendObj(new NetCommand("所属游戏", ""));
            DataRow yxdr = yxdt.NewRow();
            yxdr["id"] = "0";
            yxdr["gamename"] = "---选择游戏---";
            yxdt.Rows.InsertAt(yxdr, 0);
            comboBoxEx2.DataSource = yxdt;
            comboBoxEx2.DisplayMember = "gamename";
            comboBoxEx2.ValueMember = "id";
        }

        void nc_shiQuLianJieEvent()
        {
            //throw new NotImplementedException();
        }

        void nc_duiHuaEvent(object obj)
        {
            ThreadPool.QueueUserWorkItem(duihuacall, obj);
        }

        void HookKeyPress_keyPressEvent(System.Windows.Forms.Keys keys, HookKeyPress.KeyBoardHookStruct kbh)
        {
            switch (keys)
            {
                case Keys.F1:
                    if (kbh.flags == 128)
                    {
                        // 这里写按下后做什么事

                        Empty();
                    }
                    break;
                case Keys.F2:
                    if (kbh.flags == 128)
                    {
                        // 这里写按下后做什么事
                        Add();
                        
                    }
                    break;
                case Keys.F5:
                    if (kbh.flags == 128)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(BindData));
                    }
                    break;
            }
        }



        private void QuickForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Global.notifyIcon.Dispose();
            //System.Environment.Exit(System.Environment.ExitCode);
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            Format();
        }

        private void Format()
        {
            try
            {
                string content = textBoxX1.Text;

                string[] rows = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string 订单号 = rows[1].Split(':')[1].Trim();

                textBoxX2.Text = 订单号;
            }
            catch
            { }

            //手游客服-小威  14:54:08
            //订单号				:SY1406170000000528
            //订单创建时间			:2014-06-17 14:51:56
            //游戏区服信息			:天天酷跑/腾讯/QQ(苹果)/其他
            //面额				:320
            //到货时间			:5分钟到货
            //充值游戏账号			:452152561
            //充值游戏密码			:298woaini
        }


        private void buttonX2_Click(object sender, EventArgs e)
        {
            Empty();
        }

        private void Empty()
        {
            textBoxX2.Text = "";
            textBoxX1.Text = "";
            textBoxX9.Text = "";
            textBoxX10.Text = "";
            textBoxX11.Text = "";
            textBoxX7.Text = "";
            comboBoxEx2.SelectedIndex = 0;
            comboBoxEx3.SelectedIndex = 0;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!Validation.CheckNumber(textBoxX10.Text))
            {
                MessageBox.Show("购买数量填写错误！");
                return;
            }
            if (!Validation.CheckPrice(textBoxX11.Text))
            {
                MessageBox.Show("商品原价填写错误！");
                return;
            }
            if (comboBoxEx2.SelectedIndex == 0)
            {
                MessageBox.Show("所属游戏必须选择！");
                return;
            }
            if (comboBoxEx3.SelectedIndex == 0)
            {
                MessageBox.Show("账号类型必须选择！");
                return;
            }
            if (!Validation.CheckPrice(textBoxX7.Text))
            {
                UIHelper.Message(this, "订单总价填写错误！");
                return;
            }
            Add();
        }

        private void Add()
        {
            if (textBoxX2.Text == "")
            {
                UIHelper.Message(this,"订单不能为空！");
                return;
            }
            textBoxX1.Text = textBoxX1.Text.Replace(" ","");
            string[] data = new string[9];
            data[0] = textBoxX2.Text;
            data[1] = textBoxX9.Text;
            data[2] = textBoxX1.Text + "\r\n购买数量：" + textBoxX10.Text + "件\r\n商品原价：" + decimal.Parse(textBoxX11.Text).ToString("#0.00") + "\r\n订单总价：" + decimal.Parse(textBoxX7.Text).ToString("#0.00") + "\r\n所属游戏：" + comboBoxEx2.Text + "\r\n账号类型：" + comboBoxEx3.Text+"\r\n";
            
            if ((bool)nc.SendObj(new NetCommand("下单", data)))
            {
                UIHelper.Message(this, "添加成功！");
                Empty();
                ThreadPool.QueueUserWorkItem(new WaitCallback(BindData));
            }
            else
            {
                //MessageBox.Show("添加失败！");
                UIHelper.Message(this,"添加失败！");
            }
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            UI.Dispatch.Instance.Execite();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            string 状态 = "";
            if (comboBoxEx1.Text != "选择状态")
                状态 = comboBoxEx1.Text;
            if (textBoxX3.Text == "" && textBoxX4.Text == "" && textBoxX5.Text == "" && textBoxX6.Text == "")
            {
                MessageBox.Show("必须有一个不为空！");
                return;
            }
            object obj = nc.SendObj(new NetCommand("条件查询订单详情", new string[] { textBoxX3.Text, textBoxX4.Text, 状态, textBoxX5.Text, textBoxX6.Text }));
            DataTable dt = (DataTable)obj;
            UIHelper.查询订单详情(this, dt);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(BindData));
        }

        private void QuickForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认关闭工作台？此操作不可恢复", "退出工作台", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
                System.Environment.Exit(System.Environment.ExitCode);
            }
            else
            {
                e.Cancel = true;
            }
        }


    }
}
