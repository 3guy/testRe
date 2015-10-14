using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Text;

namespace PlaceOrder.Util
{
    public class MyProcess
    {
        public static void Kill(string name)
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process item in ps)
                {
                    if (item.ProcessName == name)
                    {
                        item.Kill();
                    }
                }
            }
            catch { }
        }

        public static bool IsExist(string name)
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process item in ps)
                {
                    
                    if (item.ProcessName == name)
                    {
                        return true;
                    }
                }
                
            }
            catch { }
            return false;
        }
    }
}
