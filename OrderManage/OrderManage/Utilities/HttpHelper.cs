namespace OrderManage.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;

    public class HttpHelper
    {
        private Encoding encoding = Encoding.Default;
        private HttpWebRequest request = null;
        private HttpWebResponse response = null;

        public CookieContainer _CookieContainer = new CookieContainer();

        public HttpHelper()
        { }

        public HttpHelper(CookieContainer CookieContainer)
        {
            this._CookieContainer = CookieContainer;
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public HttpResult GetHtml(HttpItem objhttpItem)
        {
            try
            {
                lock (this)
                {
                    this.SetRequest(objhttpItem);
                }
            }
            catch (Exception exception)
            {
                return new HttpResult { Cookie = "", Header = null, Html = exception.Message, StatusDescription = "配置参考时报错" };
                //return null;
            }
            return this.GetHttpRequestData(objhttpItem);
        }

        public HttpResult GetHtml(HttpItem objhttpItem, CookieContainer CookieContainer)
        {
            try
            {
                this._CookieContainer = CookieContainer;
                this.SetRequest(objhttpItem);
            }
            catch (Exception exception)
            {
                return new HttpResult { Cookie = "", Header = null, Html = exception.Message, StatusDescription = "配置参考时报错" };
            }
            return this.GetHttpRequestData(objhttpItem);
        }

        private HttpResult GetHttpRequestData(HttpItem objhttpitem)
        {
            HttpResult result = new HttpResult();
            try
            {
                using (this.response = (HttpWebResponse) this.request.GetResponse())
                {
                    result.Responseuri = response.ResponseUri.ToString();
                    result.StatusCode = this.response.StatusCode;
                    result.StatusDescription = this.response.StatusDescription;
                    result.Header = this.response.Headers;
                    
                    if (this.response.Cookies != null)
                    {
                        result.Cookie = GetCookiesStr(this._CookieContainer);
                        //result.CookieCollection = this.response.Cookies;
                    }
                    else if (this.response.Headers["set-cookie"] != null)
                    {
                        result.Cookie = this.response.Headers["set-cookie"];
                    }
                    MemoryStream memoryStream = new MemoryStream();
                    if ((this.response.ContentEncoding != null) && this.response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        memoryStream = GetMemoryStream(new GZipStream(this.response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        memoryStream = GetMemoryStream(this.response.GetResponseStream());
                    }
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    if (objhttpitem.ResultType == ResultType.Byte)
                    {
                        result.ResultByte = bytes;
                    }
                    if (this.encoding == null)
                    {
                        System.Text.RegularExpressions.Match match = Regex.Match(Encoding.Default.GetString(bytes), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
                        string str = (match.Groups.Count > 1) ? match.Groups[2].Value.ToLower() : string.Empty;
                        str = str.Replace("\"", "").Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk");
                        if (str.Length > 2)
                        {
                            this.encoding = Encoding.GetEncoding(str.Trim());
                        }
                        else if (string.IsNullOrEmpty(this.response.CharacterSet))
                        {
                            this.encoding = Encoding.UTF8;
                        }
                        else
                        {
                            this.encoding = Encoding.GetEncoding(this.response.CharacterSet);
                        }
                    }
                    result.Html = this.encoding.GetString(bytes);
                }
            }
            catch (WebException exception)
            {
                this.response = (HttpWebResponse) exception.Response;
                result.Html = exception.Message;
                if (this.response != null)
                {
                    result.StatusCode = this.response.StatusCode;
                    result.StatusDescription = this.response.StatusDescription;
                }
            }
            catch (Exception exception2)
            {
                result.Html = exception2.Message;
            }
            if (objhttpitem.IsToLower)
            {
                result.Html = result.Html.ToLower();
            }
            return result;
        }

        private static MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream stream = new MemoryStream();
            int count = 0x100;
            byte[] buffer = new byte[count];
            for (int i = streamResponse.Read(buffer, 0, count); i > 0; i = streamResponse.Read(buffer, 0, count))
            {
                stream.Write(buffer, 0, i);
            }
            return stream;
        }

        private void SetCer(HttpItem objhttpItem)
        {
            if (!string.IsNullOrEmpty(objhttpItem.CerPath))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                this.request = (HttpWebRequest) WebRequest.Create(objhttpItem.URL);
                this.request.ClientCertificates.Add(new X509Certificate(objhttpItem.CerPath));
            }
            else
            {
                this.request = (HttpWebRequest) WebRequest.Create(objhttpItem.URL);
            }
        }

        
        private void SetCookie(HttpItem objhttpItem)
        {
            if (!string.IsNullOrEmpty(objhttpItem.Cookie))
            {
                this.request.Headers[HttpRequestHeader.Cookie] = objhttpItem.Cookie;
            }
            else
                this.request.CookieContainer = _CookieContainer;
            if (objhttpItem.CookieCollection != null)
            {
                //this.request.CookieContainer = new CookieContainer();
                this.request.CookieContainer = _CookieContainer;
                this.request.CookieContainer.Add(objhttpItem.CookieCollection);
            }
            
        }

        private void SetPostData(HttpItem objhttpItem)
        {
            if (this.request.Method.Trim().ToLower().Contains("post"))
            {
                byte[] postdataByte = null;
                if (((objhttpItem.PostDataType == PostDataType.Byte) && (objhttpItem.PostdataByte != null)) && (objhttpItem.PostdataByte.Length > 0))
                {
                    postdataByte = objhttpItem.PostdataByte;
                }
                else if (!((objhttpItem.PostDataType != PostDataType.FilePath) || string.IsNullOrEmpty(objhttpItem.Postdata)))
                {
                    StreamReader reader = new StreamReader(objhttpItem.Postdata, this.encoding);
                    postdataByte = Encoding.Default.GetBytes(reader.ReadToEnd());
                    reader.Close();
                }
                else if (!string.IsNullOrEmpty(objhttpItem.Postdata))
                {
                    postdataByte = Encoding.Default.GetBytes(objhttpItem.Postdata);
                }
                if (postdataByte != null)
                {
                    this.request.ContentLength = postdataByte.Length;
                    this.request.GetRequestStream().Write(postdataByte, 0, postdataByte.Length);
                }
            }
        }

        private void SetProxy(HttpItem objhttpItem)
        {
            if (!string.IsNullOrEmpty(objhttpItem.ProxyIp))
            {
                WebProxy proxy;
                if (objhttpItem.ProxyIp.Contains(":"))
                {
                    string[] strArray = objhttpItem.ProxyIp.Split(new char[] { ':' });
                    proxy = new WebProxy(strArray[0].Trim(), Convert.ToInt32(strArray[1].Trim())) {
                        Credentials = new NetworkCredential(objhttpItem.ProxyUserName, objhttpItem.ProxyPwd)
                    };
                    this.request.Proxy = proxy;
                }
                else
                {
                    proxy = new WebProxy(objhttpItem.ProxyIp, false) {
                        Credentials = new NetworkCredential(objhttpItem.ProxyUserName, objhttpItem.ProxyPwd)
                    };
                    this.request.Proxy = proxy;
                }
                this.request.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
        }

        private void SetRequest(HttpItem objhttpItem)
        {
            this.SetCer(objhttpItem);
            if ((objhttpItem.Header != null) && (objhttpItem.Header.Count > 0))
            {
                foreach (string str in objhttpItem.Header.AllKeys)
                {
                    this.request.Headers.Add(str, objhttpItem.Header[str]);
                }
            }
            this.SetProxy(objhttpItem);

            this.request.Headers.Add("Cache-control", "no-cache");
            this.request.Headers.Add("Accept-Language", "zh-cn");

            this.request.Method = objhttpItem.Method;
            this.request.Timeout = objhttpItem.Timeout;
            this.request.ReadWriteTimeout = objhttpItem.ReadWriteTimeout;
            this.request.Accept = objhttpItem.Accept;
            this.request.ContentType = objhttpItem.ContentType;
            this.request.KeepAlive = objhttpItem.KeepAlive;
            this.request.UserAgent = objhttpItem.UserAgent;

            this.encoding = objhttpItem.Encoding;
            this.SetCookie(objhttpItem);
            this.request.Referer = objhttpItem.Referer;
            this.request.AllowAutoRedirect = objhttpItem.Allowautoredirect;
            this.SetPostData(objhttpItem);
            if (objhttpItem.Connectionlimit > 0)
            {
                this.request.ServicePoint.ConnectionLimit = objhttpItem.Connectionlimit;
            }
        }

        private List<Cookie> GetAllCookies(CookieContainer cc)
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

        private string GetCookiesStr(CookieContainer cc)
        {
            Hashtable htable = new Hashtable();
            List<Cookie> CookieList = GetAllCookies(cc);
            string cookie = "";
            foreach (var item in CookieList)
            {
                //item.Domain = ".china.alibaba.com";
                //text_Result.AppendText("======" + item.Value);
                htable.Add(item.Name, item.Value);
                cookie += item.Name + "=" + item.Value + "; ";
            }
            return cookie;
        }
    }
}

