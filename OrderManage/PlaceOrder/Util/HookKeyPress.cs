using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace PlaceOrder.Util
{
    internal class HookKeyPress
    {
        [StructLayout(LayoutKind.Sequential)]
        internal class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        /// <summary>
        /// 用户按键
        /// </summary>
        /// <param name="message"></param>
        internal delegate void KeyPressEventHandler(Keys keys, KeyBoardHookStruct kbh);
        /// <summary>
        /// 用户按键
        /// </summary>
        internal static event KeyPressEventHandler keyPressEvent;

        //委托 
        internal delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        static int hHook = 0;
        internal const int WH_KEYBOARD_LL = 13;
        //LowLevel键盘截获，如果是WH_KEYBOARD＝2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。 
        static HookProc KeyBoardHookProcedure;

        //设置钩子 
        [DllImport("user32.dll")]
        internal static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //抽掉钩子 
        internal static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll")]
        //调用下一个钩子 
        internal static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        internal static extern int GetCurrentThreadId();
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle(string name);

        internal static void Hook_Start()
        {
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyBoardHookProcedure,
                        GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                //如果设置钩子失败. 
                if (hHook == 0)
                {
                    Hook_Clear();
                }
            }
        }

        /// <summary>
        /// 取消钩子事件
        /// </summary>
        internal static void Hook_Clear()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
        }

        internal static int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                Keys k = (Keys)Enum.Parse(typeof(Keys), kbh.vkCode.ToString());
                if (keyPressEvent != null)
                    keyPressEvent(k, kbh);
                
                ////示例代码
                //switch (k)
                //{
                //    case Keys.F10:
                //        if (kbh.flags == 0)
                //        {
                //            // 这里写按下后做什么事
                //            //Main.GB = true;
                //            Thread th = new Thread(new ThreadStart(Start));
                //            th.Start();

                //        }
                //        else if (kbh.flags == 128)
                //        {
                //            //放开后做什么事
                //        }
                //        return 1;
                //}
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

    }
}
