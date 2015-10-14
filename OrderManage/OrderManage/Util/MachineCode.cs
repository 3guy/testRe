using System;
using System.Collections.Generic;

using System.Management;
using System.Text;

namespace OrderManage.Util
{
    /// <summary>  
    /// 机器码  
    /// </summary>  
    public class MachineCode
    {
        ///   <summary>   
        ///   获取cpu序列号       
        ///   </summary>   
        ///   <returns> string </returns>   
        public static string GetCpuInfo()
        {
            string cpuInfo = " ";
            using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
            {
                ManagementObjectCollection moc = cimobject.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    mo.Dispose();
                }
            }
            return cpuInfo.ToString();
        }

        ///   <summary>   
        ///   获取硬盘ID       
        ///   </summary>   
        ///   <returns> string </returns>   
        public static string GetHDid()
        {
            string HDid = " ";
            using (ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive"))
            {
                ManagementObjectCollection moc1 = cimobject1.GetInstances();

                foreach (ManagementObject mo in moc1)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                    mo.Dispose();
                    return HDid;
                }
            }
            return HDid.ToString();
        }

        ///   <summary>   
        ///   获取网卡硬件地址   
        ///   </summary>   
        ///   <returns> string </returns>   
        public static string GetMoAddress()
        {
            string MoAddress = " ";
            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                ManagementObjectCollection moc2 = mc.GetInstances();
                foreach (ManagementObject mo in moc2)
                {
                    if ((bool)mo["IPEnabled"] == true)
                        MoAddress = mo["MacAddress"].ToString();
                    mo.Dispose();
                }
            }
            return MoAddress.ToString();
        }

        /// <summary>
        /// 获取cpuid+硬盘id+网卡id 的大写MD5
        /// </summary>
        /// <returns></returns>
        public static string GetMCode()
        {
            string cpuid = MachineCode.GetCpuInfo();
            string hdid = MachineCode.GetHDid();
            string maid = MachineCode.GetMoAddress();
            return Md5Helper.GetMd5(cpuid + hdid + maid).ToUpper();
        }
    }  
}
