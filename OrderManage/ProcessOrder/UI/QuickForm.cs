using NetEntity;
using ProcessOrder.BLL;
using ProcessOrder.Common;
using ProcessOrder.Net;
using ProcessOrder.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProcessOrder.UI
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


            
        }
        
        private void QuickForm_Load(object sender, EventArgs e)
        {
            Global.notifyIcon = this.notifyIcon1;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;
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
                    labelX2.Text = Global.user[1];
                    
                    Reflash();
                }
                else
                {
                    System.Environment.Exit(System.Environment.ExitCode);
                }



                初始化采集();
                ThreadPool.QueueUserWorkItem(new WaitCallback(登录交易猫));
                
            }
            catch (Exception ex)
            {
                ImportDataLog.WriteLog("ReceiveData:" + ex.Message + "\r\n" +
"触发异常方法：" + ex.TargetSite + "\r\n" +
"异常详细信息" + ex.StackTrace + "\r\n");
            }

        }

        void 初始化采集()
        {
            DataTable dt = (DataTable)nc.SendObj(new NetCommand("获取交易猫账号", ""));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JiaoYiMaoHelper jym = new JiaoYiMaoHelper(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                Global.jymhlist.Add(jym);
            }
        }

        void 登录交易猫(object obj)
        {
            for (int i = 0; i < Global.jymhlist.Count; i++)
            {
                if (!Global.jymhlist[i].IsLogin)
                    Global.jymhlist[i].登录();
            }
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
                case Keys.F5:
                    if (kbh.flags == 128)
                    {
                        Reflash();
                    }
                    break;
            }
        }


        internal void Reflash()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(刷新待处理));
            ThreadPool.QueueUserWorkItem(new WaitCallback(刷新正在处理));
            ThreadPool.QueueUserWorkItem(new WaitCallback(刷新已处理));
            ThreadPool.QueueUserWorkItem(new WaitCallback(刷新提成));

        }

        internal void 刷新提成(object obj)
        {
            string jttc = nc.SendObj(new NetCommand("今天提成", "")).ToString();
            if (jttc == "")
                UIHelper.刷新今天提成(this, "0");
            else
                UIHelper.刷新今天提成(this, jttc);
            string zttc = nc.SendObj(new NetCommand("昨天提成", "")).ToString();
            if (zttc == "")
                UIHelper.刷新昨天提成(this, "0");
            else
                UIHelper.刷新昨天提成(this, zttc);
        }

        internal void 刷新待处理(object obj)
        {
            DataTable dt = (DataTable)nc.SendObj(new NetCommand("待处理订单", ""));
            UIHelper.绑定待处理(this, dt);
        }

        internal void 刷新正在处理(object obj)
        {
            DataTable dt2 = (DataTable)nc.SendObj(new NetCommand("正在处理的订单", ""));
            UIHelper.绑定正在处理(this, dt2);
        }

        internal void 刷新已处理(object obj)
        {
            DataTable dt3 = (DataTable)nc.SendObj(new NetCommand("已处理订单", ""));
            UIHelper.绑定已处理(this, dt3);
        }

        private void QuickForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Global.notifyIcon.Dispose();
            //System.Environment.Exit(System.Environment.ExitCode);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //
            DataGridViewCellCollection dgvcc = this.dataGridView1.CurrentRow.Cells;

            if (CIndex == 0)
            {
                if (dataGridView2.Rows.Count < 5)
                {
                    //修改
                    bool isok = (bool)nc.SendObj(new NetCommand("接受订单", dgvcc[6].Value.ToString()));
                    if (!isok)
                    {
                        MessageBox.Show("接受失败！可能已分配给其他用户！");
                    }
                    ThreadPool.QueueUserWorkItem(new WaitCallback(刷新待处理));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(刷新正在处理));
                }
                else
                {
                    MessageBox.Show("接受的订单已超过5单，请先完成5单再接新单！！！",
                                  "错误",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Stop
                                  );
                }
            }
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            UI.Dispatch.Instance.Execite();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //
            DataGridViewCellCollection dgvcc = this.dataGridView2.CurrentRow.Cells;

            if (CIndex == 0)
            {
                CompleteOrder co = new CompleteOrder(dgvcc[6].Value.ToString(), dgvcc[2].Value.ToString(), dgvcc[4].Value.ToString(),dgvcc[7].Value.ToString(),nc);
                co.ShowDialog();
                ThreadPool.QueueUserWorkItem(new WaitCallback(刷新正在处理));
                ThreadPool.QueueUserWorkItem(new WaitCallback(刷新已处理));
                ThreadPool.QueueUserWorkItem(new WaitCallback(刷新提成));
            }
        }

        private void QuickForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认关闭充值台？此操作不可恢复", "退出充值台", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
