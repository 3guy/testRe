using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ProcessOrder.Common
{
    internal class JiaoYiMaoHelper
    {
        CookieContainer _CookieCAli = new CookieContainer();
        string _CookieStr = string.Empty;

        internal string 账号;
        internal string 密码;
        internal bool IsLogin = false;
        internal JiaoYiMaoHelper(string 账号,string 密码)
        {
            this.账号 = 账号;
            this.密码 = 密码;
        }

        internal List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

        /// <summary>
        /// 转化为32位Int，数值范围：-2147483648～2147483647，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
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


        internal bool 登录()
        {
            //string url = "https://api.open.uc.cn/cas/login/commit?uc_param_str=einisivelafrpfmibiup";
            string sUrl = "https://api.open.uc.cn/cas/login/commit";
            HttpWebRequest httpRequest = WebRequest.Create(sUrl) as HttpWebRequest;
            //httpRequest.ServicePoint.Expect100Continue = false;
            httpRequest.AllowAutoRedirect = true;
            httpRequest.KeepAlive = true;
            //httpRequest.CookieContainer = GlobalUnit.CookieCAli;
            httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon)";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, */*";
            //httpRequest.Referer = "https://login.taobao.com/member/login.jhtml?style=b2b&from=b2b&full_redirect=true&redirect_url=http%3A%2F%2Floginchina.alibaba.com%2Fmember%2Fjump.htm%3Ftarget%3Dhttp%253A%252F%252Floginchina.alibaba.com%252Fmember%252FmarketSigninJump.htm%253FDone%253Dhttp%25253A%25252F%25252Fchina.alibaba.com%25252F&reg=http%3A%2F%2Fchina.alibaba.com%2Fmember%2Fjoin.htm%3Flead%3Dhttp%253A%252F%252Fchina.alibaba.com%252F%26leadUrl%3Dhttp%253A%252F%252Fchina.alibaba.com%252F%26tracelog%3Dnotracelog_s_reg";
            httpRequest.Method = "POST";
            //httpRequest.Headers.Add("Accept-Encoding", "");
            httpRequest.Headers.Add("Cache-Control", "no-cache");
            //httpRequest.Headers.Add("Cookie", GlobalUnit.CookiesAli);

            //Encoding myEncoding = Encoding.GetEncoding("gb2312");
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            httpRequest.CookieContainer = _CookieCAli;

            //Parameter Name	Value
            //client_id	94
            //redirect_uri	https://www.jiaoyimao.com/login?redirectUrl=https%3A%2F%2Fwww.jiaoyimao.com%2Fmerchant%2Fadmin%2Findex
            //target_client_id	
            //target_redirect_uri	
            //display	pc
            //change_uid	1
            //loginName	wushehe004
            //password	wu456123




            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            //ServicePointManager.ServerCertificateValidationCallback =
            //            new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

            string param =

                "client_id=94&" +
                "redirect_uri=" + HttpUtility.UrlEncode("https://www.jiaoyimao.com/login?redirectUrl=https%3A%2F%2Fwww.jiaoyimao.com%2Fmerchant%2Fadmin%2Findex", myEncoding) + "&" +
                "target_client_id=&" +
                "target_redirect_uri=&" +
                "display=pc&" +
                "change_uid=1&" +
                "loginName=" + 账号 + "&" +
                "password=" + 密码
                ;

            byte[] postBytes = myEncoding.GetBytes(param);

            httpRequest.ContentLength = postBytes.Length;

            using (Stream reqStream = httpRequest.GetRequestStream())  //填参数
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
            }

            string sResult = string.Empty;


            using (WebResponse wr = httpRequest.GetResponse())  //Post
            {
                HttpWebResponse rs = (HttpWebResponse)wr;
                //_CookieCAli.Add(httpRequest.RequestUri, rs.Cookies);
                StreamReader reader = new StreamReader(wr.GetResponseStream(), myEncoding);
                sResult = reader.ReadToEnd();
            }




            Hashtable htable = new Hashtable();
            List<Cookie> CookieList = GetAllCookies(_CookieCAli);
            _CookieStr = "";
            foreach (var item in CookieList)
            {
                //item.Domain = ".china.alibaba.com";
                //text_Result.AppendText("======" + item.Value);
                htable.Add(item.Name, item.Value);
                _CookieStr += item.Name + "=" + item.Value + "; ";
            }



            //获取csrf_token
            HeaderObject HObject = new HeaderObject();
            HObject.CookieStr = _CookieStr;
            HObject.Url = "https://www.jiaoyimao.com/merchant/staff/index";
            sResult = HttpHelper.Get(HObject);

            if (sResult.IndexOf("商家管理后台") > 0)
            {
                IsLogin = true;
                return true;
            }
            else
            {
                IsLogin = false;
                return false;
            }

            //MessageBox.Show(sResult);

        }

        internal ArrayList 获取订单列表()
        {
            HeaderObject HObject = new HeaderObject();
            HObject.CookieStr = _CookieStr;
            HObject.Url = "https://www.jiaoyimao.com/merchant/deliver/manageIosPrechargeOrder?page=1&listType=1";
            string sResult = HttpHelper.Get(HObject);

            //textBox1.Text = sResult;

            //Get total pagecount 共1页
            //string sTem = GetPosValue(sResult, "mod-page", "</div>", 0);
            string sTem = 截取文本(sResult, "共", "页 ，到第", 0);
            int iTotalPage = ConvertToInt32(sTem, 1);
            ArrayList dd = new ArrayList();
            dd = 解析订单列表(sResult, dd);

            for (int i = 2; i <= iTotalPage; i++)
            {
                HObject.CookieStr = _CookieStr;
                HObject.Url = string.Format("https://www.jiaoyimao.com/merchant/deliver/manageIosPrechargeOrder?page={0}&listType=1", i);
                sResult = HttpHelper.Get(HObject);
                dd = 解析订单列表(sResult, dd);
            }
            return dd;
        }

        internal ArrayList 解析订单列表(string AHtml, ArrayList dd)
        {
            string[] items = 截取文本(AHtml, "<li class=\"item-info\">", "</li>", "", 0);

            for (int i = 0; i < items.Length; i++)
            {
                string sID = 截取文本(items[i], "prepareIosPrechargeOrder/", "\"", 0);
                string sOrder = 截取文本(items[i], "订单号：", "</span>", 0);

                //string sOrderTime = "订单时间：" + GetPosValue(item, "订单时间：", "</span>", 0);

                //string sName = GetPosValue(item, "class=\"name\"", "</span>", 0);
                //sName = "单价：" + GetPosValue(item, "unit\">", "<", 0);

                //string sCount = GetPosValue(item, "count\">", "</span>", 0);

                dd.Add(new string[]{sID,sOrder});
                //dd.Add("ID:" + sID + " 订单号：" + sOrder);
            }
            return dd;
        }

        internal string 获取订单详情(string sID)
        {
            HeaderObject hobDetail = new HeaderObject();
            hobDetail.CookieStr = _CookieStr;
            hobDetail.Url = "https://www.jiaoyimao.com/merchant/deliver/actViewIosPrechargeOrder/" + sID;
            string sResult = HttpHelper.Get(hobDetail);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sResult);

            HtmlNode rootnode = doc.DocumentNode;    //XPath路径表达式，这里表示选取所有span节点中的font最后一个子节点，其中span节点的class属性值为num
            //根据网页的内容设置XPath路径表达式
            //string xpathstring = "//div[@class='MsgContentSelf'or@class='MsgContent']";
            string xpathstring = "//div[@class='row']";
            HtmlNodeCollection msghcon = rootnode.SelectNodes(xpathstring);    //所有找到的节点都是一个集合
            if (msghcon == null)
                return "";
            //MessageBox.Show(msghcon[0].InnerText);


            string sTem = string.Empty;
            string ret="";

            ret+="[ID：" + sID+"]\r\n";
            sTem = 截取文本(sResult, "订单号：", "</div>", 0);
            ret += "订单号：" + sTem + "\r\n";
            foreach (HtmlNode hnitem in msghcon)
            {
                ret += hnitem.InnerText.Replace("&yen;", "") + "\r\n";
            }
            return ret;
        }

        internal void 准备发货(string sID)
        {
            HeaderObject hobDetail = new HeaderObject();
            hobDetail.CookieStr = _CookieStr;
            hobDetail.Url = "https://www.jiaoyimao.com/merchant/deliver/prepareIosPrechargeOrder/" + sID;
            HttpHelper.Get(hobDetail);

            hobDetail = new HeaderObject();
            hobDetail.CookieStr = _CookieStr;
            hobDetail.Url = "https://www.jiaoyimao.com/merchant/deliver/actViewIosPrechargeOrder/" + sID;
            string r = HttpHelper.Get(hobDetail);
        }

        internal void 发货(string sID)
        {
            准备发货(sID);

            string sParam = "orderId=" + sID + "&chargeRemark=";

            HeaderObject hobDetail = new HeaderObject();
            hobDetail.CookieStr = _CookieStr;
            hobDetail.Url = "https://www.jiaoyimao.com/merchant/deliver/submitactiosprechargeorder";
            string sResult = HttpHelper.Post(hobDetail, sParam);
        }



    }
}
