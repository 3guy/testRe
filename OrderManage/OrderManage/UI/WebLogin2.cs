using OrderManage.Common;
using OrderManage.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    internal partial class WebLogin2 : Form
    {
        internal JiaoYiMaoHelper jymh;
        internal WebLogin2(JiaoYiMaoHelper jymh)
        {
            this.jymh = jymh;
            //webBrowser1.ScriptErrorsSuppressed = true; //禁用错误脚本提示
            InitializeComponent();
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //MessageBox.Show(webBrowser1.Document.Body.InnerHtml);
            //int index = webBrowser1.Document.Body.InnerHtml.IndexOf("<DIV class=user-link>您好，");
            //int index = webBrowser1.Document.Body.InnerHtml.IndexOf("正在为你跳转中，请稍候...");
            //ImportDataLog.WriteLog(webBrowser1.Document.Body.InnerHtml);
            int index = webBrowser1.Document.Body.InnerHtml.IndexOf("欢迎您，");
            //int index = webBrowser1.Document.Body.InnerHtml.IndexOf("class=user-link>您好，");
            if (index > 0)
            {

                //jymh._CookieStr = webBrowser1.Document.Cookie + ";" + HttpHelper.GetCookieString("http://www.jiaoyimao.com");
                //FullWebBrowserCookie.InternetSetCookie("https://www.jiaoyimao.com", "JSESSIONID", );
                jymh._CookieStr = webBrowser1.Document.Cookie + ";" + FullWebBrowserCookie.GetCookieInternal(new Uri("https://www.jiaoyimao.com"), false) ;
                //jymh._CookieStr = webBrowser1.Document.Cookie;
                //jymh._CookieStr = FullWebBrowserCookie.GetCookieInternal(new Uri("http://www.jiaoyimao.com"), true);
                //jymh._CookieStr = webBrowser1.Document.Cookie;
                
                jymh.验证登录();
                this.Close();
            }
        }

        private void WebLogin2_Load(object sender, EventArgs e)
        {
            //webBrowser1.Document.Cookie = "external_login_type=0;actk=205aff1bf7afcf2770fbdfbfaa043721; Hm_lvt_63bfdb121fda0a8a7846d5aac78f6f37=1437658860,1437659602,1437659679; Hm_lpvt_63bfdb121fda0a8a7846d5aac78f6f37=1437659679";
        }
    }
}
