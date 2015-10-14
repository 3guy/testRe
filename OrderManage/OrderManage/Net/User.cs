using OrderManage.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OrderManage.Net
{
    public class User
    {
        public TcpClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        public byte[] last;

        /// <summary>
        /// 唯一id
        /// </summary>
        public string guid;

        /// <summary>
        /// 记录用户数据库id
        /// </summary>
        public string uid;

        /// <summary>
        /// 登入时间
        /// </summary>
        public DateTime time;

        /// <summary>
        /// 当前User是否已关闭
        /// </summary>
        public bool isClose=false;

        public bool isLogin = false;

        /// <summary>
        /// 扩展数据接口
        /// </summary>
        public IUserData userdata;

        public PackageHelper ph = new PackageHelper();

        public User(TcpClient client)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            guid = Guid.NewGuid().ToString("N");
            time = DateTime.Now;
        }


        public void Close()
        {
            isClose = true;
            br.Close();
            bw.Close();
            client.Close();
        }
    }
}
