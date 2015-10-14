using ProcessOrder.Common;
using ProcessOrder.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ProcessOrder.UI
{
    internal class UIHelper
    {
        internal static void Message(QuickForm mf, string msg)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                MessageBox.Show(msg);
            });
        }

        internal static void 刷新今天提成(QuickForm mf, string msg)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                mf.labelX5.Text = msg;
            });
        }

        internal static void 刷新昨天提成(QuickForm mf, string msg)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                mf.labelX3.Text = msg;
            });
        }


        internal static void 绑定待处理(QuickForm mf, DataTable dt)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                mf.dataGridView1.DataSource = dt;
            });
        }

        internal static void 绑定正在处理(QuickForm mf, DataTable dt)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                mf.dataGridView2.DataSource = dt;
            });
        }

        internal static void 绑定已处理(QuickForm mf, DataTable dt)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                mf.dataGridView3.DataSource = dt;
            });
        }
    }
}
