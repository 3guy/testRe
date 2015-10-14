using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace OrderManage.Util
{
    public class ExitWindows2
    {
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("user32")]
        public static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
        public enum MonitorState
        {
            MonitorStateOn = -1,
            MonitorStateOff = 2,
            MonitorStateStandBy = 1
        }
        public static void ShutDown()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-s -t 00");
                System.Diagnostics.Process.Start(startinfo);
            }
            catch { }
        }
        public static void Reboot()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-r -t 00");
                System.Diagnostics.Process.Start(startinfo);
            }
            catch { }
        }
        public static void LogOff()
        {
            try
            {
                ExitWindowsEx(0, 0);
            }
            catch { }
        }
        public static void LockPC()
        {
            try
            {
                LockWorkStation();
            }
            catch { }
        }
        public static void Turnoffmonitor()
        {
            SetMonitorInState(MonitorState.MonitorStateOff);
        }
        private static void SetMonitorInState(MonitorState state)
        {
            SendMessage(0xFFFF, 0x112, 0xF170, (int)state);
        }

    }

}