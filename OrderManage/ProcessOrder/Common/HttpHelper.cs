using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ProcessOrder.Common
{
    public class HeaderObject
    {
        private string _Url = "";
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private string _PostData = "";
        public string PostData
        {
            get { return _PostData; }
            set { _PostData = value; }
        }

        private string _ContentType = "application/x-www-form-urlencoded";
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        private string _UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon)";
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }

        private string _Referer = "";
        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }

        private string _Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, */*";
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }

        private string _CookieStr = "";
        public string CookieStr
        {
            get { return _CookieStr; }
            set { _CookieStr = value; }
        }

        private Encoding _Encoding = Encoding.GetEncoding("utf-8");
        public Encoding Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }

        private CookieContainer _CookieContainer = null;
        public CookieContainer CookieContainer
        {
            get { return _CookieContainer; }
            set { _CookieContainer = value; }
        }

    }

    public class HttpHelper
    {
        /// <SUMMARY></SUMMARY> 
        /// 获取指定页面的HTML代码 
        /// 
        /// <PARAM name="url" />指定页面的路径 
        /// <PARAM name="postData" />回发的数据 
        /// <PARAM name="isPost" />是否以post方式发送请求 
        /// <PARAM name="cookieCollection" />Cookie集合 
        /// <RETURNS></RETURNS> 
        public static string Get(HeaderObject HObject)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(HObject.Url);

                httpWebRequest.Headers.Add("Cookie", HObject.CookieStr);

                httpWebRequest.Headers.Add("Cache-control", "no-cache");
                httpWebRequest.Headers.Add("Accept-Language", "zh-cn");

                httpWebRequest.CookieContainer = HObject.CookieContainer;
                httpWebRequest.TransferEncoding = "";
                httpWebRequest.ContentType = HObject.ContentType;
                httpWebRequest.AllowAutoRedirect = true;
                //httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = HObject.Referer;
                httpWebRequest.Accept = HObject.Accept;
                httpWebRequest.UserAgent = HObject.UserAgent;
                httpWebRequest.Method = "GET";
                //httpWebRequest.ContentLength = byteRequest.Length;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, HObject.Encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                httpWebRequest.Abort();
                httpWebResponse.Close();

                return html;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return string.Empty;
            }
        }

        public static string Post(HeaderObject HObject, string AParam)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(HObject.Url);

                httpWebRequest.Headers.Add("Cookie", HObject.CookieStr);

                httpWebRequest.Headers.Add("Cache-control", "no-cache");
                httpWebRequest.Headers.Add("Accept-Language", "zh-cn");
                //httpWebRequest.Headers.Add("x-requested-with", "XMLHttpRequest");

                httpWebRequest.CookieContainer = HObject.CookieContainer;
                httpWebRequest.TransferEncoding = "";
                httpWebRequest.ContentType = HObject.ContentType;
                httpWebRequest.AllowAutoRedirect = true;
                //httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = HObject.Referer;
                httpWebRequest.Accept = HObject.Accept;
                httpWebRequest.UserAgent = HObject.UserAgent;
                httpWebRequest.Method = "POST";
                //httpWebRequest.ContentLength = byteRequest.Length;

                byte[] postBytes = Encoding.ASCII.GetBytes(AParam);

                httpWebRequest.ContentLength = postBytes.Length;

                using (Stream reqStream = httpWebRequest.GetRequestStream())  //填参数
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }

                string html = "";
                using (WebResponse wr = httpWebRequest.GetResponse())  //Post
                {
                    HttpWebResponse rs = (HttpWebResponse)wr;
                    StreamReader reader = new StreamReader(wr.GetResponseStream(), HObject.Encoding);
                    html = reader.ReadToEnd();
                }

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }

                return html;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                } if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return string.Empty;
            }
        }


    }
}
