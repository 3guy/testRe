using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using PlaceOrder.Util;

namespace PlaceOrder.Net
{
    /// <summary>
    /// 包大小字节的长度(占1个字节)+包大小字节(根绝包大小占据N个字节)+需要发出的TCP包
    /// </summary>
    public class PackageHelper
    {
        /// <summary>
        /// 封包
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] Packet(byte[] bytes)
        {
            //byte[] byteslen = System.Text.Encoding.ASCII.GetBytes(bytes.Length.ToString());
            //byte[] byteslenlen = System.Text.Encoding.ASCII.GetBytes(byteslen.Length.ToString());
            //byte[] newbytes = new byte[bytes.Length + byteslen.Length + byteslenlen.Length];

            //System.Buffer.BlockCopy(byteslenlen, 0, newbytes, 0, byteslenlen.Length);
            //System.Buffer.BlockCopy(byteslen, 0, newbytes, byteslenlen.Length, byteslen.Length);
            //System.Buffer.BlockCopy(bytes, 0, newbytes, byteslen.Length + byteslenlen.Length, bytes.Length);

            byte[] xieyi = System.Text.Encoding.ASCII.GetBytes("[NetEnd]");
            byte[] newbytes = new byte[xieyi.Length + bytes.Length];
            System.Buffer.BlockCopy(bytes, 0, newbytes, 0, bytes.Length);
            System.Buffer.BlockCopy(xieyi, 0, newbytes, bytes.Length, xieyi.Length);
            //ImportDataLog.WriteLog("进入封包");
            //ImportDataLog.WriteLog("封包是否找到[NetEnd]:" + IsEnd(newbytes));
            //ImportDataLog.WriteLog("封包数据：" + System.Text.Encoding.ASCII.GetString(newbytes));
            return newbytes;
        }

        public byte[] tempbytes;

        /// <summary>
        /// 拆包.如果一直在接收数据则一直返回null，直到接收完成才有返回值
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public byte[] UnPacking(byte[] buffer)
        {

                try
                {
                    //如果第一次未接受完毕则继续接收
                    if (tempbytes == null)
                    {
                        if (IsEnd(buffer) > 0)
                        {
                            byte[] newbytes = new byte[IsEnd(buffer)];
                            System.Buffer.BlockCopy(buffer, 0, newbytes, 0, IsEnd(buffer) );
                            return newbytes;
                            //return buffer;
                        }
                        tempbytes = buffer;
                        return null;
                    }
                    else
                    {
                        byte[] newbytes = new byte[tempbytes.Length + buffer.Length];
                        System.Buffer.BlockCopy(tempbytes, 0, newbytes, 0, tempbytes.Length);
                        System.Buffer.BlockCopy(buffer, 0, newbytes, tempbytes.Length, buffer.Length);
                        tempbytes = newbytes;
                        if (IsEnd(tempbytes) > 0)
                        {
                            //byte[] backbyte = tempbytes;
                            //tempbytes = null;
                            //byte[] newbytes2 = new byte[IsEnd(backbyte) ];
                            //System.Buffer.BlockCopy(backbyte, 0, newbytes2, 0, IsEnd(backbyte) );
                            //return newbytes2;
                            
                            byte[] backbyte = tempbytes;
                            int isends=IsEnd(backbyte);//结束标记的起始索引位置
                            int isende=isends+8;//结束标记的结束索引位置
                            byte[] newbytes2 = new byte[isends];
                            tempbytes=new byte[backbyte.Length-isende];
                            System.Buffer.BlockCopy(backbyte, isende, tempbytes,0, tempbytes.Length);
                            System.Buffer.BlockCopy(backbyte, 0, newbytes2, 0, isends);
                            return newbytes2;
                        }
                        return null;
                    }
                }
                catch
                { return null; }
                finally
                {
                }
            
        }

        public static int IsEnd(byte[] bytes)
        {
            string str = System.Text.Encoding.ASCII.GetString(bytes);
            return str.IndexOf("[NetEnd]");
        }
    }
}
