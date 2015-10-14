using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ProcessOrder.Util;
using ProcessOrder.Common;

namespace ProcessOrder.Net
{
    /// <summary>
    /// 序列化实体类，用来作为tcp传输协议
    /// </summary>
    public class XuLieHua
    {
        //static XuLieHua()
        //{
        //    Instance = new XuLieHua();
        //}

        //public static XuLieHua Instance
        //{
        //    get;
        //    set;
        //}

        public byte[] ToBytes(object obj)
        {
            MemoryStream ms = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            byte[] bytes = ms.ToArray();
            //byte[] newbytes = StickPackDeal.FengBao(bytes);
            byte[] newbytes = PackageHelper.Packet(bytes);
            //ImportDataLog.WriteLog("序列化Length:"+bytes.Length.ToString());
            ms.Flush();

            ms.Close();
            return newbytes;
        }

        public object ToObject(byte[] buffer)
        {
            byte[] newbytes;
            try
            {
                MemoryStream ms = new MemoryStream(buffer, 0, buffer.Length);
                //ms.Position = 0;
                BinaryFormatter formatter = new BinaryFormatter();
                object obj = formatter.Deserialize(ms);
                ms.Close();
                return obj;
            }
            catch (Exception ex)
            {
                newbytes = buffer;
                ImportDataLog.WriteLog(ex.Message);
                ImportDataLog.WriteLog("Length:" + newbytes.Length + "__Error:" + System.Text.Encoding.ASCII.GetString(newbytes));


                return null;
            }
        }

        /// <summary>
        /// 无序列化转换byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] NoSToBytes(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            byte[] bytes = ms.ToArray();

            ms.Close();

            return bytes;
        }


        /// <summary>
        /// 删除btye数组片段，返回新byte数组。从索引0开始删除。
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="delcount"></param>
        /// <returns></returns>
        public static byte[] DeleteClip(byte[] bt, int delcount)
        {
            byte[] newbt = new byte[bt.Length - delcount];
            System.Buffer.BlockCopy(bt, delcount - 1, newbt, 0, bt.Length - delcount);
            return newbt;
        }
    }
}
