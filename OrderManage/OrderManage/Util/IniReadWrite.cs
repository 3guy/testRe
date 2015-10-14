using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OrderManage.Util
{
    /// <summary>
    /// 配置文件读写类
    /// </summary>
    public class IniReadWrite
    {
        public IniReadWrite()
        {

        }

        
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section">段落名称</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>long</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// ini配置文件写操作：成功显示“写入成功！”
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="section">段落名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Write(string fileName, string section, string key, string value)
        {
            //获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
            string filePath = Application.StartupPath + "\\" + fileName ;
            //保存文件
            WritePrivateProfileString(section,key,value,filePath);
            return "写入成功！";
        }

        /// <summary>
        /// 读ini文件
        /// </summary>
        /// <param name="section">INI文件中的段落名称</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="def">无法读取时候时候的缺省数值</param>
        /// <param name="retVal">读取数值</param>
        /// <param name="size">数值的大小</param>
        /// <param name="filePath">INI文件的完整路径和名称</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                  int size, string filePath);

        /// <summary>
        /// ini配置文件读操作；读取失败时返回str参数值
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="section">配置文件段落名</param>
        /// <param name="key">键</param>
        /// <param name="str">读取失败：返回的信息</param>
        /// <returns></returns>
        public static string Read(string fileName,string section,string key,string str)
        {
            string filePath = Application.StartupPath + "\\" + fileName ;
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, str, sb, 255, filePath);
            //显示读取的信息
            return sb.ToString();
        }

    }
}
