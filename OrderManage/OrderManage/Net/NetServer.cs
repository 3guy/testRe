using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using OrderManage.Util;

namespace OrderManage.Net
{
    public class NetServer
    {
        /// <summary>
        /// 保存连接的所有用户
        /// </summary>
        public Dictionary<string, User> userList = new Dictionary<string, User>();

        public Dictionary<string, User> UserList
        {
            get { return userList; }
            set { userList = value; }
        }

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIP;

        public string ServerIP1
        {
            get { return ServerIP; }
            set { ServerIP = value; }
        }

        /// <summary>
        /// 监听端口
        /// </summary>
        public int port;
        private TcpListener myListener;

        public TcpListener MyListener
        {
            get { return myListener; }
            set { myListener = value; }
        }

        /// <summary>
        /// 是否正常退出所有接收线程
        /// </summary>
        bool isNormalExit = false;

        public bool IsNormalExit
        {
            get { return isNormalExit; }
            set { isNormalExit = value; }
        }

        /// <summary>
        /// 新用户登录事件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        public delegate void XinYongHuDengLuEventHandler(User user);

        /// <summary>
        /// 新用户登录委托
        /// </summary>
        public event XinYongHuDengLuEventHandler xinYongHuDengLuEvent;

        /// <summary>
        /// 用户退出事件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        public delegate void YongHuTuiChuEventHandler(User user);

        /// <summary>
        /// 用户退出委托
        /// </summary>
        public event YongHuTuiChuEventHandler yongHuTuiChuEvent;

        /// <summary>
        /// 对话事件
        /// </summary>
        /// <param name="message"></param>
        public delegate void DuiHuaEventHandler(User user, Object obj);
        /// <summary>
        /// 对话委托
        /// </summary>
        public event DuiHuaEventHandler duiHuaEvent;

        /// <summary>
        /// 根据当前程序目录的文本文件‘ServerIPAndPort.txt’内容来设定IP和端口
        /// 格式：127.0.0.1:8885
        /// </summary>
        public void SetServerIPAndPort()
        {
            string IPAndPort = "";
            if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ServerIPAndPort.txt"))
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ServerIPAndPort.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                IPAndPort = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }

            if (IPAndPort == "")
            {
                //string strHostName = Dns.GetHostName();  //得到本机的主机名
                //IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                //ServerIP = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
                //port = 8888;
                NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                int len = interfaces.Length;

                for (int i = 0; i < len; i++)
                {
                    NetworkInterface ni = interfaces[i];
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        if (ni.Name == "本地连接")
                        {
                            IPInterfaceProperties property = ni.GetIPProperties();
                            foreach (UnicastIPAddressInformation ip in
                                property.UnicastAddresses)
                            {
                                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                {
                                    ServerIP = ip.Address.ToString();
                                    port = 8888;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ServerIP = IPAndPort.Split(':')[0]; //设定IP
                port = int.Parse(IPAndPort.Split(':')[1]); //设定端口
            }


            //string strHostName = Dns.GetHostName();  //得到本机的主机名
            //IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
            //ServerIP = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
            //port = 8888;
        }

        /// <summary>
        /// 读取配置，启动服务
        /// </summary>
        public void Start()
        {
            SetServerIPAndPort();
            myListener = new TcpListener(IPAddress.Parse(ServerIP), port);
            myListener.Start();

            //AddItemToListBox(string.Format("开始在{0}:{1}监听客户连接", ServerIP, port));
            //创建一个线程监客户端连接请求
            Thread myThread = new Thread(ListenClientConnect);
            myThread.IsBackground = true;
            myThread.Start();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            isNormalExit = true;
            foreach (var item in userList)
	        {
                RemoveUser(userList[item.Key]);
            }
            //通过停止监听让 myListener.AcceptTcpClient() 产生异常退出监听线程
            myListener.Stop();
        }

        /// <summary>
        /// 接收客户端连接
        /// </summary>
        public void ListenClientConnect()
        {
            TcpClient newClient = null;
            while (true)
            {
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch (Exception ex)
                {
                    //ImportDataLog.WriteLog("连接中断1111：" + ex.Message);
                    //ImportDataLog.WriteLog(ex.Message);
                    //当单击‘停止监听’或者退出此窗体时 AcceptTcpClient() 会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接收一个客户端连接，就创建一个对应的线程循环接收该客户端发来的信息；.
                User user = new User(newClient);
                Thread threadReceive = new Thread(ReceiveData);
                threadReceive.IsBackground = true;
                threadReceive.Start(user);
                userList.Add(user.guid,user);

                if (xinYongHuDengLuEvent != null)
                    xinYongHuDengLuEvent(user);
            }
        }

        /// <summary>
        /// 处理接收的客户端信息
        /// </summary>
        /// <param name="userState">客户端信息</param>
        public void ReceiveData(object userState)
        {
            User user = (User)userState;
            TcpClient client = user.client;
            while (isNormalExit == false)
            {
                try
                {
                    //从网络流中读出字符串，此方法会自动判断字符串长度前缀

                    const int bufferSize = 10240;
                    byte[] buffer = new byte[bufferSize];
                    int readBytes = 0;
                    //将客户端流读入到buffer中
                    
                    readBytes = user.br.Read(buffer, 0, bufferSize);

                    byte[] newbytes = null;
                    if ( user.ph.tempbytes!= null)
                    {
                        newbytes = new byte[user.ph.tempbytes.Length+readBytes];
                        System.Buffer.BlockCopy(user.ph.tempbytes, 0, newbytes, 0, user.ph.tempbytes.Length);
                        System.Buffer.BlockCopy(buffer, 0, newbytes, user.ph.tempbytes.Length, readBytes);
                    }
                    else
                    {
                        newbytes = new byte[readBytes];
                        System.Buffer.BlockCopy(buffer, 0, newbytes, 0, readBytes);
                    }

                    string[] cmdtemp = System.Text.Encoding.ASCII.GetString(newbytes).Split(new string[] { "[NetEnd]" }, StringSplitOptions.None);
                    
                    //debug
                    //ImportDataLog.WriteLog("cmdTempLenth:" + cmdtemp.Length.ToString());
                    //string[] debugcmdtemp = System.Text.Encoding.UTF8.GetString(newbytes).Split(new string[] { "[NetEnd]" }, StringSplitOptions.None);
                    //for (int i = 0; i < debugcmdtemp.Length; i++)
                    //{
                    //    ImportDataLog.WriteLog("debugcmdtemp" + i.ToString() + ":" + debugcmdtemp[i]);
                    //}

                    byte [] execute=newbytes;
                    for (int i = 0; i < cmdtemp.Length; i++)
                    {
                        if (cmdtemp[i] != ""&&i==cmdtemp.Length-1)
                        { 
                            user.ph.tempbytes=new byte[cmdtemp[i].Length];
                            System.Buffer.BlockCopy(execute, 0, user.ph.tempbytes, 0, execute.Length);
                            //ImportDataLog.WriteLog("nextexecute:" + System.Text.Encoding.UTF8.GetString(user.ph.tempbytes));
                        }
                        else if (cmdtemp[i] != "")
                        {
                            byte[] temp = new byte[cmdtemp[i].Length];

                            System.Buffer.BlockCopy(execute, 0, temp, 0, temp.Length);

                            //ImportDataLog.WriteLog("execute:" + System.Text.Encoding.UTF8.GetString(temp));
                            byte [] texecute=execute;
                            if ((execute.Length - temp.Length - 8) != 0)
                            {
                                execute = new byte[execute.Length - temp.Length - 8];
                                System.Buffer.BlockCopy(texecute, temp.Length + 8, execute, 0, execute.Length);
                            }
                            user.last = temp;
                            if (user.last != null)
                            {
                                if (duiHuaEvent != null)
                                    duiHuaEvent(user, new XuLieHua().ToObject(user.last));
                            }
                            user.ph.tempbytes = null;
                        }
                        
                    }
                    

                }
                //如果客户端的请求命令以FILE开头，即获取单个图片文件
                catch (Exception ex)
                {
                    ImportDataLog.WriteLog("ReceiveData:" + ex.Message + "\r\n" +
"触发异常方法：" + ex.TargetSite + "\r\n" +
"异常详细信息" + ex.StackTrace + "\r\n");
                    if (isNormalExit == false)
                    {
                        RemoveUser(user);
                    }
                    break;
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="obj"></param>
        public void SendObjToAllClient(object obj)
        {
            foreach (var item in userList)
            {
                SendObjToClient(userList[item.Key], obj);
            }
        }

        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        public void SendObjToClient(User user, Object obj)
        {
            try
            {
                lock (this)
                {
                    user.bw.Write(new XuLieHua().ToBytes(obj));
                    user.bw.Flush();
                }
            }
            catch (Exception ex)
            {
                ImportDataLog.WriteLog("进入异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="user">指定要移除的用户</param>
        public void RemoveUser(User user)
        {
            if (yongHuTuiChuEvent != null)
                yongHuTuiChuEvent(user);
            userList.Remove(user.guid);
            user.Close();

            //AddItemToListBox(string.Format("当前连接用户数：{0}", userList.Count));
        }
    }
}
