
using HtmlAgilityPack;
using ProcessOrder.Entity;
using ProcessOrder.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProcessOrder.BLL
{
    /// <summary>
    /// 窗口操作类
    /// </summary>
    internal class WindowHelper
    {
        internal IntPtr hwndwork;//工作台窗口句柄
        internal IntPtr hwndworkmsg;//工作台聊天记录窗口句柄
        internal IntPtr hwndworkedit;//工作台输入框句柄

        CDmSoft dm = new CDmSoft();

        internal WindowHelper(string username)
        {
            FindWindow formfw = new FindWindow(new IntPtr(0), "StandardFrame", username + " - 客服工作台", 100);
            hwndwork = formfw.FoundHandle;
            dm.SetWindowState(hwndwork.ToInt32(), 8);
            dm.SetWindowState(hwndwork.ToInt32(), 1);
            FindWindow fw = new FindWindow(formfw.FoundHandle, "Internet Explorer_Server", "", 100);
            hwndworkmsg = fw.FoundHandle;
            FindWindow workeditfw = new FindWindow(formfw.FoundHandle, "RichEditComponent", "", 100);
            hwndworkedit = workeditfw.FoundHandle;
        }

        /// <summary>
        /// 获取当前窗口原始消息
        /// </summary>
        /// <returns></returns>
        internal string GetMsg()
        {
            mshtml.IHTMLDocument2 id = GetHtmlDocument(hwndworkmsg.ToInt32());
            //MessageBox.Show(id.body.innerHTML.ToString());
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(id.body.innerHTML);

            HtmlNode rootnode = doc.DocumentNode;    //XPath路径表达式，这里表示选取所有span节点中的font最后一个子节点，其中span节点的class属性值为num
            //根据网页的内容设置XPath路径表达式
            //string xpathstring = "//div[@class='MsgContentSelf'or@class='MsgContent']";
            string xpathstring = "//div";
            HtmlNodeCollection msghcon = rootnode.SelectNodes(xpathstring);    //所有找到的节点都是一个集合
            if (msghcon == null)
                return "";
            string innertext = msghcon[2].InnerText;
            string msg = innertext;
            if (msg.Length<20)
                return "";
            msg = msg.Substring(20);
            return msg;
        }

        internal List<MsgEntity> AllMsg()
        {
            List<MsgEntity> msglist=new List<MsgEntity>();
            string msg=this.GetMsg();
            while (msg.LastIndexOf("):") >= 0)
            {
                MsgEntity msn = new MsgEntity();
                msn.user = this.LastMsgUser(msg);
                msn.usermsg = this.LastMsg(msg);
                msglist.Insert(0, msn);
                msg = this.DeleteLastMsg(msg);
            }

            return msglist;
        }

        /// <summary>
        /// 删除最后一次消息人与消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string DeleteLastMsg(string msg)
        {
            int timeindex = msg.LastIndexOf("&nbsp;(");
            int nameindex = msg.LastIndexOf("\r\n", timeindex);
            return msg.Substring(0, nameindex+1);
        }


        /// <summary>
        /// 获取当前窗口最后一次消息发送人
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string LastMsgUser(string msg)
        {
            string xname = "";
            try
            {
                int timeindex = msg.LastIndexOf("&nbsp;(");
                int nameindex = msg.LastIndexOf("\r\n", timeindex);
                xname = msg.Substring(nameindex, timeindex - nameindex);
                xname = xname.Replace("&nbsp;", "");
                xname = xname.Replace("\r\n", "");
                xname = xname.Trim();
            }
            catch { }
            return xname;
        }

        /// <summary>
        /// 获取当前窗口最后一次消息内容
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string LastMsg(string msg)
        {
            string xmsg = "";
            try
            {
                int timeindex = msg.LastIndexOf("):");
                xmsg = msg.Substring(timeindex + 2);
                xmsg = xmsg.Replace("&nbsp;", "");
                xmsg = xmsg.Replace("\r\n", "");
                xmsg = xmsg.Trim();
            }
            catch { }
            return xmsg;
        }

        /// <summary>
        /// 获取当前窗口最后一次消息内容及消息发送人
        /// </summary>
        /// <returns></returns>
        internal MsgEntity LastMsg()
        {
            string msg = this.GetMsg();
            MsgEntity msgentity = new MsgEntity();
            msgentity.user = this.LastMsgUser(msg);
            msgentity.usermsg = this.LastMsg(msg);
            return msgentity;
        }

        /// <summary>
        /// 设置发送内容
        /// </summary>
        /// <param name="content"></param>
        internal void SetMsg(string content)
        {
            SetWindowState();
            dm.SendString(hwndworkedit.ToInt32(), content);
        }

        /// <summary>
        /// 设置窗口置顶并激活
        /// </summary>
        internal void SetWindowState()
        {
            int ret = dm.GetWindowState(hwndwork.ToInt32(), 3);
            if (ret == 1)
                dm.SetWindowState(hwndwork.ToInt32(), 12);
            dm.SetWindowState(hwndwork.ToInt32(), 1);
        }

        /// <summary>
        /// 按发送回车
        /// </summary>
        internal void PressEnter()
        {
            SetWindowState();
            //System.Windows.Forms.SendKeys.SendWait("{Enter}"); 
            dm.KeyPress(System.Windows.Forms.Keys.Enter.GetHashCode());
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        internal void SendMsg(string msg)
        {
            this.SetMsg(msg);
            Thread.Sleep(100);
            //System.Windows.Forms.Application.DoEvents();
            this.PressEnter();
            Thread.Sleep(100);
            //System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// 获取当前窗口最原始消息框的mshtml.IHTMLDocument2
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public mshtml.IHTMLDocument2 GetHtmlDocument(int hwnd)
        {
            System.Object domObject = new System.Object();
            int tempInt = 0;
            System.Guid guidIEDocument2 = new Guid();
            int WM_Html_GETOBJECT = Win32API.RegisterWindowMessage("WM_Html_GETOBJECT");//定义一个新的窗口消息
            int W = Win32API.SendMessage(hwnd, WM_Html_GETOBJECT, 0, ref tempInt);//注:第二个参数是RegisterWindowMessage函数的返回值
            int lreturn = Win32API.ObjectFromLresult(W, ref guidIEDocument2, 0, ref domObject);
            mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)domObject;
            return doc;
        }
    }
}
