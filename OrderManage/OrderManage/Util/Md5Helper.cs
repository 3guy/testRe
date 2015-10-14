using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManage.Util
{
    public class Md5Helper
    {
        public static string GetMd5(string str)
        { 
            System.Security.Cryptography.MD5CryptoServiceProvider Md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] src = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] md5out=Md5.ComputeHash(src);
            return Convert.ToBase64String(md5out);
        }
    }
}
