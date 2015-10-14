using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManage.Util
{
    public class MemoryHelper
    {
        /// <summary>
        /// 释放内存
        /// </summary>
        /// <param name="processName"></param>
        public static void ShiFangNeiCun(string processName)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processes)
            {
                if (process.ProcessName == processName)
                {
                    SetProcessMemoryToMin(process.Handle);
                }
            }
        }


        //优化内存
        public static int SetProcessMemoryToMin(IntPtr SetProcess)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return SetProcessWorkingSetSize(SetProcess, -1, -1);
            }
            return -1;
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

    }
}
