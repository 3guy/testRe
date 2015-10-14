namespace OrderManage.Utilities
{
    using System;
    using System.Net;
    using System.Text;

    public class HttpItem
    {
        private string _Accept = "text/html, application/xhtml+xml, */*";
        private bool _KeepAlive = false;


        private string _CerPath = string.Empty;
        private string _ContentType = "text/html";
        private string _Cookie = string.Empty;
        private System.Text.Encoding _Encoding = null;
        private string _Method = "GET";
        private string _Postdata = string.Empty;
        private byte[] _PostdataByte = null;
        private Utilities.PostDataType _PostDataType = Utilities.PostDataType.String;
        private int _ReadWriteTimeout = 0x7530;
        private string _Referer = string.Empty;
        private int _Timeout = 0x186a0;
        private string _URL = string.Empty;
        private string _UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
        private bool allowautoredirect = false;
        private int connectionlimit = 0x400;
        private System.Net.CookieCollection cookiecollection = null;
        private WebHeaderCollection header = new WebHeaderCollection();
        private bool isToLower = false;
        private string proxyip = string.Empty;
        private string proxypwd = string.Empty;
        private string proxyusername = string.Empty;
        private Utilities.ResultType resulttype = Utilities.ResultType.String;


        public bool KeepAlive
        {
            get { return _KeepAlive; }
            set { _KeepAlive = value; }
        }
        public string Accept
        {
            get
            {
                return this._Accept;
            }
            set
            {
                this._Accept = value;
            }
        }

        public bool Allowautoredirect
        {
            get
            {
                return this.allowautoredirect;
            }
            set
            {
                this.allowautoredirect = value;
            }
        }

        public string CerPath
        {
            get
            {
                return this._CerPath;
            }
            set
            {
                this._CerPath = value;
            }
        }

        public int Connectionlimit
        {
            get
            {
                return this.connectionlimit;
            }
            set
            {
                this.connectionlimit = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this._ContentType;
            }
            set
            {
                this._ContentType = value;
            }
        }

        public string Cookie
        {
            get
            {
                return this._Cookie;
            }
            set
            {
                this._Cookie = value;
            }
        }

        public System.Net.CookieCollection CookieCollection
        {
            get
            {
                return this.cookiecollection;
            }
            set
            {
                this.cookiecollection = value;
            }
        }

        public System.Text.Encoding Encoding
        {
            get
            {
                return this._Encoding;
            }
            set
            {
                this._Encoding = value;
            }
        }

        public WebHeaderCollection Header
        {
            get
            {
                return this.header;
            }
            set
            {
                this.header = value;
            }
        }

        public bool IsToLower
        {
            get
            {
                return this.isToLower;
            }
            set
            {
                this.isToLower = value;
            }
        }

        public string Method
        {
            get
            {
                return this._Method;
            }
            set
            {
                this._Method = value;
            }
        }

        public string Postdata
        {
            get
            {
                return this._Postdata;
            }
            set
            {
                this._Postdata = value;
            }
        }

        public byte[] PostdataByte
        {
            get
            {
                return this._PostdataByte;
            }
            set
            {
                this._PostdataByte = value;
            }
        }

        public Utilities.PostDataType PostDataType
        {
            get
            {
                return this._PostDataType;
            }
            set
            {
                this._PostDataType = value;
            }
        }

        public string ProxyIp
        {
            get
            {
                return this.proxyip;
            }
            set
            {
                this.proxyip = value;
            }
        }

        public string ProxyPwd
        {
            get
            {
                return this.proxypwd;
            }
            set
            {
                this.proxypwd = value;
            }
        }

        public string ProxyUserName
        {
            get
            {
                return this.proxyusername;
            }
            set
            {
                this.proxyusername = value;
            }
        }

        public int ReadWriteTimeout
        {
            get
            {
                return this._ReadWriteTimeout;
            }
            set
            {
                this._ReadWriteTimeout = value;
            }
        }

        public string Referer
        {
            get
            {
                return this._Referer;
            }
            set
            {
                this._Referer = value;
            }
        }

        public Utilities.ResultType ResultType
        {
            get
            {
                return this.resulttype;
            }
            set
            {
                this.resulttype = value;
            }
        }

        public int Timeout
        {
            get
            {
                return this._Timeout;
            }
            set
            {
                this._Timeout = value;
            }
        }

        public string URL
        {
            get
            {
                return this._URL;
            }
            set
            {
                this._URL = value;
            }
        }

        public string UserAgent
        {
            get
            {
                return this._UserAgent;
            }
            set
            {
                this._UserAgent = value;
            }
        }
    }
}

