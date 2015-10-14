using DotNet.Utilities;
using OrderManage.BLL;
using OrderManage.Common;
using OrderManage.Net;
using OrderManage.UI;
using OrderManage.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OrderManage
{
    public partial class MainForm : Form
    {
        internal NetServer ns;
        ActionFactory actionf;
        WaitCallback duihuacall;

        List<JiaoYiMaoHelper> jymhlist = new List<JiaoYiMaoHelper>();
        string 系统用户id = "";
        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            ns = new NetServer();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;
            dataGridView5.AutoGenerateColumns = false;
            dataGridView6.AutoGenerateColumns = false;
            dataGridView7.AutoGenerateColumns = false;
            dataGridView8.AutoGenerateColumns = false;
            dataGridView9.AutoGenerateColumns = false;

            actionf = new ActionFactory(ns);
            duihuacall = new WaitCallback(actionf.DoAction);
            ThreadPool.SetMinThreads(500, 500);
            ns.duiHuaEvent += ns_duiHuaEvent;
            ns.xinYongHuDengLuEvent += ns_xinYongHuDengLuEvent;
            ns.yongHuTuiChuEvent += ns_yongHuTuiChuEvent;
            ns.Start();

            LoginForm lf = new LoginForm();
            DialogResult dr=lf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Reflsh();
            }
            else
            {
                System.Environment.Exit(System.Environment.ExitCode);
            }

            //初始化采集订单的系统用户
            创建系统用户();

            //初始化采集订单
            初始化采集();

            //ThreadPool.QueueUserWorkItem(new WaitCallback(登录交易猫));

            
            
            //登录交易猫("");
        }

        void 登录交易猫(object obj)
        {
            //Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            for (int i = 0; i < jymhlist.Count; )
            {
                if (!jymhlist[i].IsLogin)
                {
                    //UIHelper.弹出验证码输入框(new WebLogin(jymhlist[i]));
                    jymhlist[i].验证登录();
                    //new WebLogin2(jymhlist[i]).ShowDialog();
                    if (jymhlist[i].IsLogin)
                    {
                        i++;
                    }
                    //UIHelper.弹出登录框(new WebLogin2(jymhlist[i]));
                }
                //while (true)
                //{
                //    if (jymhlist[i].IsLogin)
                //    {
                //        i++;
                //        break;
                //    }
                //    Thread.Sleep(50);
                //}
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(自动采集));
        }


        //void 登录交易猫(object obj)
        //{
        //    for (int i = 0; i < jymhlist.Count; )
        //    {
        //        if (!jymhlist[i].IsLogin)
        //        {
        //            UIHelper.弹出验证码输入框(new WebLogin(jymhlist[i]));
        //        }
        //        while (true)
        //        {
        //            if (jymhlist[i].IsLogin)
        //            {
        //                i++;
        //                break;
        //            }
        //            Thread.Sleep(50);
        //        }
        //    }
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(自动采集));
        //}

        void 创建系统用户()
        {
            string usid = UserStore.GetUserID("系统");
            if (usid == "")
            {
                UserStore.Add("jakljklasd.dfjaos", "h.jzklhj.klahsdf", "系统", "下单员");
                usid = UserStore.GetUserID("系统");
                系统用户id = usid;
            }
            else
            {
                系统用户id = usid;
            }
        }

        void 初始化采集()
        {
            DataTable dt = PlatformStore.GetAll();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JiaoYiMaoHelper jym = new JiaoYiMaoHelper(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                jymhlist.Add(jym);

            }
        }

        void ns_yongHuTuiChuEvent(User user)
        {
            //MessageBox.Show(xu.qq);
            //MessageBox.Show((ns.userList.Count-1).ToString());
        }

        void ns_xinYongHuDengLuEvent(User user)
        {
            //MessageBox.Show(ns.userList.Count.ToString());
        }

        
        void ns_duiHuaEvent(User user, object obj)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(duihuacall), new ServerObj(user, obj));
        }

        private void UiTimer_Tick(object sender, EventArgs e)
        {
            Dispatch.Instance.Execite();
        }

        /// <summary>
        /// 去除重复值
        /// </summary>
        /// <param name="myData"></param>
        /// <returns></returns>
        public static String[] RemoveDup(String[] myData,ref string [] chongfu)
        {
            if (myData.Length > 0)
            {
                Array.Sort(myData);

                int size = 1; //at least 1   
                for (int i = 1; i < myData.Length; i++)
                    if (myData[i] != myData[i - 1])
                        size++;

                String[] myTempData = new String[size];

                int j = 0;
                int chongfunum=0;
                myTempData[j++] = myData[0];
                chongfu = new string[myData.Length-myTempData.Length];
                for (int i = 1; i < myData.Length; i++)
                {
                    if (myData[i] != myData[i - 1])
                        myTempData[j++] = myData[i];
                    else
                    {
                        chongfu[chongfunum] = myData[i];
                        chongfunum++;
                    }
                }

                return myTempData;
            }

            return myData;
        }

        private void 添加用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserInfo auif = new AddUserInfo();
            auif.ShowDialog();
            Reflsh();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            Reflsh();
        }

        private void Reflsh()
        {
            //dataGridView5.DataSource = ProcessOrdersStore.GetAll();
            dataGridView1.DataSource = OrderStore.GetAll();
            dataGridView2.DataSource = UserStore.GetAll();
            dataGridView3.DataSource = InventoryStore.GetAll();
            dataGridView4.DataSource = PlatformStore.GetAll();
            dataGridView6.DataSource = FacevalueStore.GetAll();
            //dataGridView7.DataSource = CommissionStore.GetCommission("", DateTime.Now.AddMonths(-1).ToString(), DateTime.Now.AddDays(1).ToString());
            dataGridView8.DataSource = CommissionStore.GetAll();
            dataGridView9.DataSource = GameStore.GetAll();

            //comboBoxEx1.Items.Clear();
            DataTable dt = InventoryStore.GetAllByState();
            DataRow drr = dt.NewRow();
            drr["id"] = "0";
            drr["appid"] = "---请选择appid---";
            dt.Rows.InsertAt(drr, 0);
            comboBoxEx1.DataSource = dt;
            comboBoxEx1.DisplayMember = "appid";
            comboBoxEx1.ValueMember = "id";


            DataTable czydt = UserStore.GetAllCZY();
            DataRow czydr = czydt.NewRow();
            czydr["id"] = "0";
            czydr["name"] = "全部";
            czydt.Rows.InsertAt(czydr, 0);
            comboBoxEx3.DataSource = czydt;
            comboBoxEx3.DisplayMember = "name";
            comboBoxEx3.ValueMember = "id";
            //float totalamount = 0;
            //for (int i = 0; i < czydt.Rows.Count; i++)
            //{
            //    totalamount += float.Parse(czydt.Rows[i][2].ToString());
            //}
            //labelX17.Text = totalamount.ToString();

            DataTable yxdt = GameStore.GetAll();
            DataRow yxdr = yxdt.NewRow();
            yxdr["id"] = "0";
            yxdr["gamename"] = "全部";
            yxdt.Rows.InsertAt(yxdr, 0);
            comboBoxEx2.DataSource = yxdt;
            comboBoxEx2.DisplayMember = "gamename";
            comboBoxEx2.ValueMember = "id";
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            DialogResult dr = lf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.Show();
            }
            
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            DialogResult dr = lf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.Show();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            notifyIcon1.Dispose();
            this.Dispose();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.Hide();
            //}
        }

        DataSet xlsds = new DataSet();
        private void buttonX5_Click_1(object sender, EventArgs e)
        {            
            string stime = "";
            string etime = "";
            if (checkBoxX1.Checked)
            {
                stime=dateTimePicker1.Value.ToString("yyyy-MM-dd");
                etime=dateTimePicker3.Value.AddDays(1).ToString("yyyy-MM-dd");

                stime += " "+dateTimePicker7.Value.ToString("HH:mm")+":00";
                etime += " " + dateTimePicker8.Value.ToString("HH:mm") + ":00";
                               
            }
            string game = "";
            if (comboBoxEx2.SelectedIndex != 0)
            {
                game = comboBoxEx2.Text;
            }
            xlsds=ProcessOrdersStore.Serach(stime, etime, textBoxX3.Text, textBoxX2.Text, textBoxX4.Text, textBoxX8.Text, textBoxX1.Text, game);
            dataGridView5.DataSource = xlsds.Tables[0];
            if (xlsds!=null)
                this.labelItem2.Text = ((DataTable)dataGridView5.DataSource).Rows.Count.ToString();
        }

        private void checkBoxX1_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBoxX1.Checked;
            dateTimePicker3.Enabled = checkBoxX1.Checked;
            dateTimePicker7.Enabled = checkBoxX1.Checked;
            dateTimePicker8.Enabled = checkBoxX1.Checked;
        }

        private void checkBoxX2_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePicker4.Enabled = checkBoxX2.Checked;
            dateTimePicker2.Enabled = checkBoxX2.Checked;
        }

        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            string stime = "";
            string etime = "";
            if (checkBoxX2.Checked)
            {
                stime = dateTimePicker4.Value.ToString("yyyy-MM-dd");
                etime = dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd");
            }

            dataGridView1.DataSource = OrderStore.Serach(stime, etime, textBoxX9.Text, textBoxX7.Text);
            this.labelItem2.Text = ((DataTable)dataGridView1.DataSource).Rows.Count.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //0:修改 1：删除
            DataGridViewCellCollection dgvcc = this.dataGridView2.CurrentRow.Cells;

            if (CIndex == 0)
            {
                //修改
                UpdateUserInfo uui = new UpdateUserInfo(dgvcc[6].Value.ToString(), dgvcc[1].Value.ToString(), dgvcc[3].Value.ToString(), dgvcc[4].Value.ToString());
                uui.ShowDialog();
                Reflsh();
            }
            //else if (CIndex == 1)
            //{
            //    //删除
            //    if (MessageBox.Show("确认删除此用户？此操作不可恢复，导出备份目录：" + Application.StartupPath + @"\被封号" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", "导出被封号", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    { }
            //}

        }

        private void 添加库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddInventory auif = new AddInventory();
            auif.ShowDialog();
            Reflsh();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //0:修改 1：删除
            DataGridViewCellCollection dgvcc = this.dataGridView3.CurrentRow.Cells;

            if (CIndex == 0)
            {
                //修改
                UpdateInventory ui = new UpdateInventory(dgvcc[4].Value.ToString(), dgvcc[1].Value.ToString(), dgvcc[2].Value.ToString(), dgvcc[5].Value.ToString());
                ui.ShowDialog();
                Reflsh();
            }
        }

        private void 添加交易猫账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPlatform apf = new AddPlatform();
            apf.ShowDialog();
            Reflsh();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //0:修改 1：删除
            DataGridViewCellCollection dgvcc = this.dataGridView4.CurrentRow.Cells;

            if (CIndex == 0)
            {
                //修改
                UpdatePlatform uui = new UpdatePlatform(dgvcc[3].Value.ToString(), dgvcc[1].Value.ToString());
                uui.ShowDialog();
                Reflsh();
            }
            //else if (CIndex == 1)
            //{
            //    //删除
            //    if (MessageBox.Show("确认删除此用户？此操作不可恢复，导出备份目录：" + Application.StartupPath + @"\被封号" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", "导出被封号", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    { }
            //}
        }

        


        private void 自动采集(object obj)
        {
            while (true)
            {
                for (int i = 0; i < jymhlist.Count; i++)
                {
                    if (jymhlist[i].IsLogin)
                    {
                        ImportDataLog.WriteLog("获取订单列表");
                        ArrayList al = jymhlist[i].获取订单列表();
                        ImportDataLog.WriteLog("获取到的订单列表总数："+al.Count);
                        for (int j = 0; j < al.Count; j++)
                        {
                            string sid = ((string[])al[j])[0];
                            string 订单号 = ((string[])al[j])[1];
                            if (!OrderStore.IsExist(订单号))
                            {
                                string ss = "[交易猫账号：" + jymhlist[i].账号 + "]\r\n";
                                ImportDataLog.WriteLog("自动采集-----订单号:" + 订单号+" " + ss + jymhlist[i].获取订单详情(sid) + " 系统用户id:" + 系统用户id);
                                OrderStore.Add(订单号, ss + jymhlist[i].获取订单详情(sid), "系统自动采集", 系统用户id);
                                ImportDataLog.WriteLog("添加完成");
                                jymhlist[i].准备发货(sid);
                            }
                            else
                            {
                                if (OrderStore.IsSuccess(订单号))
                                {
                                    jymhlist[i].发货(sid);
                                }
                            }
                        }
                    }
                    else
                    {
                        //jymhlist[i].登录();
                    }
                }
                Thread.Sleep(20000);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string stime = "";
            string etime = "";
            if (checkBoxX1.Checked)
            {
                stime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                etime = dateTimePicker3.Value.AddDays(1).ToString("yyyy-MM-dd");
            }

            string p = ProcessOrdersStore.订单总金额(stime, etime, textBoxX3.Text, textBoxX2.Text, textBoxX4.Text, textBoxX8.Text, textBoxX1.Text);
            if (p == "")
                p = "0";
            MessageBox.Show("订单总金额：" + p);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            string stime = "";
            string etime = "";
            if (checkBoxX1.Checked)
            {
                stime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                etime = dateTimePicker3.Value.AddDays(1).ToString("yyyy-MM-dd");
            }

            string p=ProcessOrdersStore.sum(stime, etime, textBoxX3.Text, textBoxX2.Text, textBoxX4.Text, textBoxX8.Text, textBoxX1.Text);
            if (p == "")
                p = "0";
            MessageBox.Show("内购总金额："+p);
            
        }

        private void 添加面值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFacevalue af = new AddFacevalue();
            af.ShowDialog();
            Reflsh();
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //0:修改 1：删除
            DataGridViewCellCollection dgvcc = this.dataGridView6.CurrentRow.Cells;

            if (CIndex == 0)
            {
                //修改
                UpdateFacevalue uui = new UpdateFacevalue(dgvcc[6].Value.ToString(), dgvcc[5].Value.ToString(), dgvcc[2].Value.ToString(), dgvcc[3].Value.ToString(), dgvcc[4].Value.ToString());
                uui.ShowDialog();
                buttonX4_Click(sender, e);
            }
            else if (CIndex == 1)
            {
                //删除
                if (MessageBox.Show("确认删除此面值？此操作不可恢复", "删除面值", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool isok = FacevalueStore.Delete(dgvcc[6].Value.ToString());
                    if (isok)
                    {                        
                        MessageBox.Show("删除成功！");
                        buttonX4_Click(sender, e);
                    }
                    else
                        MessageBox.Show("删除失败！");
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            string appid = "";
            string price = "";
            if (comboBoxEx1.SelectedIndex != 0)
                appid = comboBoxEx1.SelectedValue.ToString();
            price = textBoxX5.Text;
            dataGridView6.DataSource=FacevalueStore.GetAll(appid,price);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            string uid = "";
            if (comboBoxEx3.SelectedIndex != 0)
            {
                uid = comboBoxEx3.SelectedValue.ToString();
            }
                //            stime=dateTimePicker1.Value.ToString("yyyy-MM-dd");
                //etime=dateTimePicker3.Value.AddDays(1).ToString("yyyy-MM-dd");
            string stime = "",etime="";
            stime=dateTimePicker5.Value.ToString("yyyy-MM-dd");
            etime = dateTimePicker3.Value.AddDays(1).ToString("yyyy-MM-dd");
            DataTable dt= CommissionStore.GetCommission(uid,stime,etime );
            dataGridView7.DataSource =dt;

            float totalamount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totalamount += float.Parse(dt.Rows[i][2].ToString());
            }
            labelX17.Text = decimal.Parse(totalamount.ToString()).ToString("#0.00");
            
        }

        private void 添加提成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCommission af = new AddCommission();
            af.ShowDialog();
            Reflsh();
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //0:修改 1：删除
            DataGridViewCellCollection dgvcc = this.dataGridView8.CurrentRow.Cells;

            if (CIndex == 0)
            {
                //修改

                UpdateCommission uc = new UpdateCommission(dgvcc[2].Value.ToString(), dgvcc[3].Value.ToString(), dgvcc[4].Value.ToString(), dgvcc[5].Value.ToString());
                uc.ShowDialog();
                Reflsh();
            }
            else if (CIndex == 1)
            {
                //删除
                if (MessageBox.Show("确认删除此提成？此操作不可恢复", "删除提成", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool isok = CommissionStore.Delete(dgvcc[5].Value.ToString());
                    if (isok)
                    {
                        MessageBox.Show("删除成功！");
                        Reflsh();
                    }
                    else
                        MessageBox.Show("删除失败！");
                }
            }
        }

        private void 添加游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGame af = new AddGame();
            af.ShowDialog();
            Reflsh();
        }

        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;                       //按钮所在列为第五列，列下标从0开始的   
            //0:修改 1：删除
            DataGridViewCellCollection dgvcc = this.dataGridView9.CurrentRow.Cells;

            if (CIndex == 0)
            {
                //修改
                UpdateGame uui = new UpdateGame(dgvcc[2].Value.ToString(), dgvcc[1].Value.ToString());
                uui.ShowDialog();
                Reflsh();
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            //DataChangeExcel.DataSetToExcel(xlsds, "D:\test.xls");


            ExportExcel(xlsds);
        }

        private void ExportExcel(DataSet ds)
        {
            if (ds == null)
            {
                MessageBox.Show("没有数据不可以导出");
            }
            DataSetToExcel dste = new DataSetToExcel();
            try
            {
                if (dste.ToExcel(ds, "D:\\data.xls"))
                {
                    
                    MessageBox.Show("数据导出Excel成功！");
                }
            }
            catch
            {
                MessageBox.Show("数据导出Excel失败！请联系管理员！");
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
