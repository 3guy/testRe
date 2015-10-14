using System;
using System.Collections.Generic;
using System.IO;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using RoBot.Util;
using RoBot.Common;

namespace RoBot.Net
{
    public class NetClient
    {
        private string ServerIP; //IP
        private int port;   //端口
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;

        /// <summary>
        /// 对话事件
        /// </summary>
        /// <param name="message"></param>
        public delegate void DuiHuaEventHandler(Object obj);
        /// <summary>
        /// 对话委托
        /// </summary>
        public event DuiHuaEventHandler duiHuaEvent;

        /// <summary>
        /// 失去连接事件
        /// </summary>
        /// <param name="message"></param>
        public delegate void ShiQuLianJieEventHandler();
        /// <summary>
        /// 失去连接事件
        /// </summary>
        public event ShiQuLianJieEventHandler shiQuLianJieEvent;

        public PackageHelper ph = new PackageHelper();


        public void Start()
        {
            try
            {
                this.SetServerIPAndPort();
                //btn_Login.Enabled = false;
                //此处为方便演示，实际使用时要将Dns.GetHostName()改为服务器域名
                //IPAddress ipAd = IPAddress.Parse("182.150.193.7");
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ServerIP), port);
                //AddTalkMessage("连接成功");
                //获取网络流
                NetworkStream networkStream = client.GetStream();
                //将网络流作为二进制读写对象
                br = new BinaryReader(networkStream);
                bw = new BinaryWriter(networkStream);
                //SendMessage("Login," + username);
                Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
                threadReceive.IsBackground = true;
                threadReceive.Start();

                //ThreadPool.QueueUserWorkItem(new WaitCallback(SendTick));
            }
            catch (Exception ex)
            {
                //AddTalkMessage("连接失败，原因：" + ex.Message);
                //btn_Login.Enabled = true;
                //ImportDataLog.WriteLog(ex.Message);
            }
        }

        public void Stop()
        {
            //未与服务器连接前 client 为 null
            if (client != null)
            {
                try
                {
                    //SendMessage("LogOut");
                    SendObj("Logout");
                    isExit = true;
                    br.Close();
                    bw.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    //ImportDataLog.WriteLog(ex.Message);
                }
            }
        }



        /// <summary>
        /// 根据当前程序目录的文本文件‘ServerIPAndPort.txt’内容来设定IP和端口
        /// 格式：127.0.0.1:8885
        /// </summary>
        public void SetServerIPAndPort()
        {
            try
            {

                FileStream fs = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ServerIPAndPort.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string IPAndPort = sr.ReadLine();
                ServerIP = IPAndPort.Split(':')[0]; //设定IP
                port = int.Parse(IPAndPort.Split(':')[1]); //设定端口
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                //ImportDataLog.WriteLog(ex.Message);
                //MessageBox.Show("配置IP与端口失败，错误原因：" + ex.Message);
                //System.Deployment.Application.Exit();
            }
        }


        /// <summary>
        /// 处理服务器信息
        /// </summary>
        public void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    //从网络流中读出字符串，此方法会自动判断字符串长度前缀

                    const int bufferSize = 1024;
                    byte[] buffer = new byte[bufferSize];
                    int readBytes = 0;
                    //将客户端流读入到buffer中
                    readBytes = br.Read(buffer, 0, bufferSize);

                    byte[] newbytes = null;
                    if (ph.tempbytes != null)
                    {
                        newbytes = new byte[ph.tempbytes.Length + readBytes];
                        System.Buffer.BlockCopy(ph.tempbytes, 0, newbytes, 0, ph.tempbytes.Length);
                        System.Buffer.BlockCopy(buffer, 0, newbytes, ph.tempbytes.Length, readBytes);
                    }
                    else
                    {
                        newbytes = new byte[readBytes];
                        System.Buffer.BlockCopy(buffer, 0, newbytes, 0, readBytes);
                    }

                    string[] cmdtemp = System.Text.Encoding.ASCII.GetString(newbytes).Split(new string[] { "[NetEnd]" }, StringSplitOptions.None);


                    byte[] execute = newbytes;
                    for (int i = 0; i < cmdtemp.Length; i++)
                    {
                        if (cmdtemp[i] != "" && i == cmdtemp.Length - 1)
                        {
                            ph.tempbytes = new byte[cmdtemp[i].Length];
                            System.Buffer.BlockCopy(execute, 0, ph.tempbytes, 0, execute.Length);
                            //ImportDataLog.WriteLog("nextexecute:" + System.Text.Encoding.UTF8.GetString(user.ph.tempbytes));
                        }
                        else if (cmdtemp[i] != "")
                        {
                            byte[] temp = new byte[cmdtemp[i].Length];

                            System.Buffer.BlockCopy(execute, 0, temp, 0, temp.Length);

                            //ImportDataLog.WriteLog("execute:" + System.Text.Encoding.UTF8.GetString(temp));
                            byte[] texecute = execute;
                            if ((execute.Length - temp.Length - 8) != 0)
                            {
                                execute = new byte[execute.Length - temp.Length - 8];
                                System.Buffer.BlockCopy(texecute, temp.Length + 8, execute, 0, execute.Length);
                            }
                            //user.last = temp;
                            if (temp != null)
                            {
                                if (duiHuaEvent != null)
                                    duiHuaEvent(new XuLieHua().ToObject(temp));
                            }
                            ph.tempbytes = null;
                        }

                    }



                }
                catch (IOException ie)
                {
                    if (shiQuLianJieEvent != null)
                    {
                        shiQuLianJieEvent();
                    }
                }
                catch (Exception ex)
                {
                    isExit = true;
                    ImportDataLog.WriteLog("ReceiveData:" + ex.Message + "\r\n" +
   "触发异常方法：" + ex.TargetSite + "\r\n" +
   "异常详细信息" + ex.StackTrace + "\r\n");
                    if (shiQuLianJieEvent != null)
                    {
                        shiQuLianJieEvent();
                    }
                    //if (isExit == false)
                    //{
                    //    //MessageBox.Show("与服务器失去连接");
                    //    if (shiQuLianJieEvent != null)
                    //        shiQuLianJieEvent();
                    //}

                    //Thread.Sleep(10000);
                    //ReceiveData();
                }
            }

            //Application.Exit();
        }

        /// <summary>
        /// 向服务端发送数据
        /// </summary>
        /// <param name="buffer"></param>
        public void SendObj(Object obj)
        {
            try
            {
                byte[] bt = new XuLieHua().ToBytes(obj);
                bw.Write(bt);
                bw.Flush();
            }
            catch (Exception ex)
            {
                ImportDataLog.WriteLog("SendObj:" + ex.Message + "\r\n" +
                   "触发异常方法：" + ex.TargetSite + "\r\n" +
                   "异常详细信息" + ex.StackTrace + "\r\n");
            }
        }

    }
}
