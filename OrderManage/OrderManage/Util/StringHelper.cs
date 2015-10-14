using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManage.Util
{
    internal class StringHelper
    {
        internal static string 截取文本(string AText, string ATag1, string ATag2, int AOffset)
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

        internal static string[] 截取文本(string AText, string ATag1, string ATag2, string ATag3, int AOffset)
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
    }
}
