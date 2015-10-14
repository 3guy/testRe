using System;
using System.Threading;
using OrderManage.Util;
public class StickPackDeal
{

    private Object _syncObj;

    /// <summary>
    /// 第一次接收数据的标志
    /// </summary>
    private int firstFlg = 0;    //

    /// <summary>
    /// 剩余长度
    /// </summary>
    private int leftLen = 0;     //

    /// <summary>
    /// 缓存
    /// </summary>
    private byte[] bufferCom;    //

    /// <summary>
    /// 粘包分解缓冲区
    /// </summary>
    private byte[] bufferTemp;   
    
    public StickPackDeal()
    {
        _syncObj = new Object();
        firstFlg = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stickPack">粘包</param>
    /// <param name="splitPack">分割包</param>
    public void packSplit(byte[] stickPack, ref byte[] splitPack)
    {
        //包大小字节的长度(占1个字节)+包大小字节(根绝包大小占据N个字节)+需要发出的TCP包
        object obj2;
        Monitor.Enter(obj2 = this.SyncRoot);
        try
        {
            if (bufferTemp == null)
            {
                bufferTemp = stickPack;
            }
            else
            {
                byte[] temp = new byte[bufferTemp.Length + stickPack.Length];
                Buffer.BlockCopy(bufferTemp, 0, temp, 0, bufferTemp.Length);
                Buffer.BlockCopy(stickPack, 0, temp, bufferTemp.Length, stickPack.Length);
                bufferTemp = temp;
            }

            if (this.firstFlg == 0) // 第一次接收数据
            {
                byte[] deslen = new byte[bufferTemp[0]];
                if (bufferTemp.Length < (bufferTemp[0] + 1))
                {
                    return;
                }
                Buffer.BlockCopy(bufferTemp, 1, deslen, 0, bufferTemp[0]);
                leftLen = BitConverter.ToInt32(deslen, 0);  //接收数据的长度
                bufferCom = new byte[leftLen];

                if ((bufferTemp.Length - bufferTemp[0] - 1) <= leftLen)
                {
                    Buffer.BlockCopy(bufferTemp, bufferTemp[0] + 1, bufferCom, 0, bufferTemp.Length - bufferTemp[0] - 1);
                    leftLen = leftLen - (bufferTemp.Length - bufferTemp[0] - 1);
                    bufferTemp = null;
                }
                else
                {
                    Buffer.BlockCopy(bufferTemp, bufferTemp[0] + 1, bufferCom, 0, leftLen);
                    byte[] temp = new byte[bufferTemp.Length - leftLen - bufferTemp[0] - 1];
                    Buffer.BlockCopy(bufferTemp, leftLen + bufferTemp[0] + 1, temp, 0, bufferTemp.Length - leftLen - bufferTemp[0] - 1);
                    bufferTemp = temp;
                    leftLen = 0;
                }
                this.firstFlg++;
            }
            else
            {
                if (bufferTemp.Length <= leftLen)
                {
                    Buffer.BlockCopy(bufferTemp, 0, bufferCom, bufferCom.Length - leftLen, bufferTemp.Length);
                    leftLen = leftLen - bufferTemp.Length;
                    bufferTemp = null;
                }
                else
                {
                    Buffer.BlockCopy(bufferTemp, 0, bufferCom, bufferCom.Length - leftLen, leftLen);
                    byte[] temp = new byte[bufferTemp.Length - leftLen];
                    Buffer.BlockCopy(bufferTemp, leftLen, temp, 0, bufferTemp.Length - leftLen);
                    bufferTemp = temp;
                    leftLen = 0;
                }
                this.firstFlg++;
            }

            if (leftLen == 0)
            {
                this.firstFlg = 0;
                splitPack = bufferCom;
            }
        }
        catch (Exception e)
        {
            ImportDataLog.WriteLog(e.Message);
            throw new Exception(e.Message);
        }
        finally
        {
            Monitor.Exit(obj2);
        }
    }

    public object SyncRoot
    {
        get
        {
            return this._syncObj;
        }
    }
    public static byte[] FengBao(byte []bytes)
    {
        byte[] byteslen = System.Text.Encoding.ASCII.GetBytes(bytes.Length.ToString());
        byte[] byteslenlen = System.Text.Encoding.ASCII.GetBytes(byteslen.Length.ToString());
        byte[] newbytes = new byte[bytes.Length + byteslen.Length + byteslenlen.Length];

        System.Buffer.BlockCopy(byteslenlen, 0, newbytes, 0, byteslenlen.Length);
        System.Buffer.BlockCopy(byteslen, 0, newbytes, byteslenlen.Length, byteslen.Length);
        System.Buffer.BlockCopy(bytes, 0, newbytes, byteslen.Length + byteslenlen.Length, bytes.Length);
        return newbytes;
    }

    public static byte[] ChaiBao(byte[] bytes)
    {
        return null;
    }
}
