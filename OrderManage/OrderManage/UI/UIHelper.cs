using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    /// <summary>
    /// 界面操作类
    /// </summary>
    internal class UIHelper
    {
        internal static void Message(MainForm mf, string text)
        {
            Dispatch.Instance.Add<MainForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                MessageBox.Show(text);
            });
        }

        /// <summary>
        /// 消息框
        /// </summary>
        /// <param name="mf"></param>
        /// <param name="text"></param>
        internal static void AppendMsg(MainForm mf, string text)
        {
            //这里是把要执行的ui命令加入队列，所以是异步处理的。
            Dispatch.Instance.Add<MainForm, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                //这里是对ui的具体操作，可以操作ui里的任意控件，但是要把ui控件权限设置为internal
                
            });
        }

        /// <summary>
        /// 弹出验证码输入框
        /// </summary>
        /// <param name="mf"></param>
        /// <param name="text"></param>
        internal static void 弹出验证码输入框(WebLogin mf)
        {
            //这里是把要执行的ui命令加入队列，所以是异步处理的。
            Dispatch.Instance.Add<WebLogin, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                //这里是对ui的具体操作，可以操作ui里的任意控件，但是要把ui控件权限设置为internal
                mf.ShowDialog();

            });
        }

        internal static void 弹出登录框(WebLogin2 mf)
        {
            //这里是把要执行的ui命令加入队列，所以是异步处理的。
            Dispatch.Instance.Add<WebLogin2, Guid>(
            mf, Guid.NewGuid(), (c, d) =>
            {
                //这里是对ui的具体操作，可以操作ui里的任意控件，但是要把ui控件权限设置为internal
                mf.ShowDialog();

            });
        }
    }
}
