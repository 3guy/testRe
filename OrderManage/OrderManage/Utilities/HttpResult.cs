namespace OrderManage.Utilities
{
    using System;
    using System.Net;

    public class HttpResult
    {
        private string _Cookie = string.Empty;
        private System.Net.CookieCollection cookiecollection = new System.Net.CookieCollection();
        private WebHeaderCollection header = new WebHeaderCollection();
        private string html = string.Empty;
        private byte[] resultbyte = null;
        private HttpStatusCode statusCode = HttpStatusCode.OK;
        private string statusDescription = "";
        private string _responseuri;

        public string Responseuri
        {
            get { return _responseuri; }
            set { _responseuri = value; }
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

        public string Html
        {
            get
            {
                return this.html;
            }
            set
            {
                this.html = value;
            }
        }

        public byte[] ResultByte
        {
            get
            {
                return this.resultbyte;
            }
            set
            {
                this.resultbyte = value;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
            set
            {
                this.statusCode = value;
            }
        }

        public string StatusDescription
        {
            get
            {
                return this.statusDescription;
            }
            set
            {
                this.statusDescription = value;
            }
        }
    }
}

