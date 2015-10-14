using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OrderManage.Util
{
    /// <summary>
    /// �����ļ���д��
    /// </summary>
    public class IniReadWrite
    {
        public IniReadWrite()
        {

        }

        
        /// <summary>
        /// дINI�ļ�
        /// </summary>
        /// <param name="section">��������</param>
        /// <param name="key">��</param>
        /// <param name="val">ֵ</param>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns>long</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// ini�����ļ�д�������ɹ���ʾ��д��ɹ�����
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="section">��������</param>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        public static string Write(string fileName, string section, string key, string value)
        {
            //��ȡ������Ӧ�ó���Ŀ�ִ���ļ���·������������ִ���ļ������ơ�
            string filePath = Application.StartupPath + "\\" + fileName ;
            //�����ļ�
            WritePrivateProfileString(section,key,value,filePath);
            return "д��ɹ���";
        }

        /// <summary>
        /// ��ini�ļ�
        /// </summary>
        /// <param name="section">INI�ļ��еĶ�������</param>
        /// <param name="key">INI�ļ��еĹؼ���</param>
        /// <param name="def">�޷���ȡʱ��ʱ���ȱʡ��ֵ</param>
        /// <param name="retVal">��ȡ��ֵ</param>
        /// <param name="size">��ֵ�Ĵ�С</param>
        /// <param name="filePath">INI�ļ�������·��������</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                  int size, string filePath);

        /// <summary>
        /// ini�����ļ�����������ȡʧ��ʱ����str����ֵ
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="section">�����ļ�������</param>
        /// <param name="key">��</param>
        /// <param name="str">��ȡʧ�ܣ����ص���Ϣ</param>
        /// <returns></returns>
        public static string Read(string fileName,string section,string key,string str)
        {
            string filePath = Application.StartupPath + "\\" + fileName ;
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, str, sb, 255, filePath);
            //��ʾ��ȡ����Ϣ
            return sb.ToString();
        }

    }
}
