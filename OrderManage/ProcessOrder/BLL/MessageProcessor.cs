using ProcessOrder.Common;
using ProcessOrder.Entity;
using ProcessOrder.Util;
using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;

using System.Windows.Forms;

namespace ProcessOrder.BLL
{
    /// <summary>
    /// 消息处理类
    /// </summary>
    internal class MessageProcessor
    {
        internal WindowHelper wh;
        internal string myid;
        internal string myname;
        internal MessageProcessor(string myid, string myname)
        {
            wh = new WindowHelper(myid);
            this.myid = myid;
            this.myname = myname;
        }

        /// <summary>
        /// 添加问题处理
        /// </summary>
        /// <returns></returns>
        internal bool AddWenTi()
        {
            try
            {
                MsgEntity lsendmsg = wh.LastMsg();
                if (lsendmsg.user == myname | lsendmsg.usermsg == "")
                    return false;
                string msg = DSymbol(lsendmsg.usermsg);
                if(msg!="")
                    return AccessStore.AddWenti(msg);
                return false;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 添加答案
        /// </summary>
        /// <returns></returns>
        internal bool AddDaAn()
        {
            try
            {
                List<MsgEntity> mse = wh.AllMsg();
                //如果最后一条聊天记录发送人不是我自己，或者回答为空的话则不添加答案
                if (mse[mse.Count - 1].user != myname | mse[mse.Count - 1].usermsg == "")
                    return false;
                string wenti = "";
                string daan = "";
                //答案为最后一个我发送的内容
                daan = mse[mse.Count - 1].usermsg;
                //从最后一条记录往上查找，如果有不是我发送的记录，则内容为问题
                for (int i = mse.Count - 1; i >= 0; i--)
                {
                    if (mse[i].user != myname && mse[i].usermsg != "")
                    {
                        wenti = mse[i].usermsg;
                        i = -1;
                    }
                }
                return AccessStore.AddDaAn(DSymbol(wenti), daan);
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 添加问题答案
        /// </summary>
        /// <param name="wenti"></param>
        /// <param name="daan"></param>
        /// <param name="ishuifu"></param>
        /// <returns></returns>
        internal static bool AddWentiDaan(string wenti,string daan,string ishuifu)
        {
            try
            {
                return AccessStore.Add(DSymbol(wenti),daan,ishuifu);
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        internal void SendMsg()
        {
            try
            {
                MsgEntity lsendmsg = wh.LastMsg();
                if (lsendmsg.user == myname | lsendmsg.usermsg == "")
                    return;
                string answer = AccessStore.GetAnwser(DSymbol(wh.LastMsg().usermsg));
                if (answer != "")
                    wh.SendMsg(DaAnFilter(answer));
            }
            catch
            {

            }
        }

        /// <summary>
        /// 答案过滤器
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        internal string DaAnFilter(string answer)
        {
            switch (answer)
            {
                default:
                    return answer;
            }
        }

        /// <summary>
        /// 去除符号
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static string DSymbol(string msg)
        {
            //Regex reg = new Regex(@"[^\u4e00-\u9fa5]+");
            //Regex reg = new Regex(@"[\-,\/,\|,\$,\+,\%,\&,\',\(,\),\*,\x20-\x2f,\x3a-\x40,\x5b-\x60,\x7b-\x7e,\x80-\xff,\u3000-\u3002,\u300a,\u300b,\u300e-\u3011,\u2014,\u2018,\u2019,\u201c,\u201d,\u2026,\u203b,\u25ce,\uff01-\uff5e,\uffe5]");
            string result = msg;
            result = result.Replace("?","");
            result = result.Replace("？", "");
            result = result.Replace("!", "");
            result = result.Replace("！", "");
            result = result.Replace("，", "");
            result = result.Replace(",", "");
            result = result.Replace("_", "");
            result = result.Replace("-", "");
            result = result.Replace("/", "");
            result = result.Replace("*", "");
            result = result.Replace("+", "");
            result = result.Replace("丶", "");
            result = result.Replace("丿", "");
            result = result.Replace("灬", "");
            result = result.Replace("丨", "");
            result = result.Replace("╭", "");
            result = result.Replace("╮", "");
            result = result.Replace("╯", "");
            result = result.Replace("^", "");
            result = result.Replace("^", "");
            result = result.Replace("$", "");
            result = result.Replace("#", "");
            result = result.Replace("@", "");
            result = result.Replace("~", "");
            result = result.Replace(":", "");
            result = result.Replace("：", "");
            result = result.Replace("“", "");
            result = result.Replace("”", "");
            result = result.Replace("＂", "");
            result = result.Replace("；", "");
            result = result.Replace(";", "");
            result = result.Replace(",", "");
            result = result.Replace("，", "");
            result = result.Replace("。", "");
            result = result.Replace("．", "");
            result = result.Replace("（", "");
            result = result.Replace("）", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("、", "");
            result = result.Replace("对方向您发送了一个振屏", "");
            result = result.Replace("【该消息来自手机淘宝网httpwww.taobao.comm】", "");
            result = result.Replace("对方正在使用淘宝客户端收发消息", "");
            result = result.Replace("该会员正在浏览的商品英雄联盟lol账号5w金币5万50000金币全区30级雄起工作室httpitem.taobao.comitem.htmid=20300442758", "");
            result = result.Replace("正在使用淘宝客户端收发消息", "");
            result = result.Replace("对方正在使用旺信收发消息", "");
            result = result.Replace("【该消息来自手机淘宝网httpwww.taobao.comm】", "");
            return result;
        }

    }
}
