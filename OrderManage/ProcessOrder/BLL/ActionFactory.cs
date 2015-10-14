using NetEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ProcessOrder.UI;
using ProcessOrder.Net;
using ProcessOrder.Common;
using System.Threading;

namespace ProcessOrder.BLL
{
    internal class ActionFactory
    {
        QuickForm mf;
        NetClient nc;
        internal ActionFactory(QuickForm mf, NetClient nc)
        {
            this.mf = mf;
            this.nc = nc;
        }

        internal void DoAction(object obj)
        {
            try
            {
                NetCommand ncmd = (NetCommand)obj;
                string cmd = ncmd.cmd;
                object data = ncmd.data;

                switch (cmd)
                {
                    case "Refalsh":
                        ThreadPool.QueueUserWorkItem(new WaitCallback(mf.刷新待处理));
                        break;
                    default:
                        Global.netData.Add(ncmd.gid, data);
                        break;
                }
            }
            catch (Exception ex)
            {
                ImportDataLog.WriteLog("SendObj:" + ex.Message + "\r\n" +
   "触发异常方法：" + ex.TargetSite + "\r\n" +
   "异常详细信息" + ex.StackTrace + "\r\n");
            }
        }
    }
}
