using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderManage.Util
{
    internal class Validation
    {
        /// <summary>
        /// 验证价格
        /// </summary>
        /// <param name="number">价格内容</param>
        /// <returns>true 验证成功 false 验证失败</returns>
        internal static bool CheckPrice(string price)
        {
            if (string.IsNullOrEmpty(price))
            {
                return false;
            }
            Regex regex = new Regex(@"^(-)?\d+(\.\d+)?$");
            if (regex.IsMatch(price))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="number">数字内容</param>
        /// <returns>true 验证成功 false 验证失败</returns>
        internal static bool CheckNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            Regex regex = new Regex(@"^[0-9]\d*$");
            if (regex.IsMatch(number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
