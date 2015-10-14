using PlaceOrder.Common;
using PlaceOrder.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PlaceOrder.UI
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

        internal static void 查询订单详情(QuickForm mf, DataTable dt)
        {
            Dispatch.Instance.Add<QuickForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                mf.dataGridView1.DataSource = dt;
            });
        }
    }
}
