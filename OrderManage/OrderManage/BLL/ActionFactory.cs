using OrderManage.Common;
using OrderManage.Net;
using OrderManage.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NetEntity;
using System.Data;

namespace OrderManage.BLL
{
    internal class ServerObj
    {
        internal ServerObj(User user, Object obj)
        {
            this.user = user;
            this.obj = obj;
        }
        internal User user;
        internal Object obj;
    }


    internal class ActionFactory
    {
        NetServer ns;
        internal ActionFactory(NetServer ns)
        {
            this.ns = ns;
        }

        public void DoAction(object sobj)
        {
            try
            {
                ServerObj so = (ServerObj)sobj;
                User user = so.user;
                NetEntity.NetCommand ncmd = (NetEntity.NetCommand)so.obj;

                if (user.isLogin)
                {
                    switch (ncmd.cmd)
                    {
                        case "下单":
                            string [] data=(string [])ncmd.data;
                            string 订单号 = data[0];
                            string 备注 = data[1];
                            string 创建人=user.uid;
                            string 订单详情=data[2];

                            bool isok=OrderStore.Add( 订单号, 订单详情,备注,创建人);
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "下单", isok));
                            //ns.SendObjToAllClient(new NetCommand("Refalsh", ""));
                            break;
                        case "待处理订单":
                            ns.SendObjToClient(user,new NetCommand(ncmd.gid,"待处理订单",OrderStore.待处理订单()));
                            break;
                        case "正在处理的订单":
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "正在处理的订单", OrderStore.正在处理的订单(user.uid)));
                            break;
                        case "已处理订单":
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "正在处理的订单", ProcessOrdersStore.查询当前用户已充值(user.uid)));
                            break;
                        case "查询订单详情":
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "查询订单详情", ProcessOrdersStore.查询订单详情(user.uid)));
                            break;
                        case "条件查询订单详情":
                            string [] tjcxdata=(string [])ncmd.data;
                            string 订单号2 = tjcxdata[0];
                            string 订单详情2 = tjcxdata[1];
                            string 状态2 = tjcxdata[2];
                            string 充值人2 = tjcxdata[3];
                            string app账号2 = tjcxdata[4];
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "条件查询订单详情", ProcessOrdersStore.条件查询订单详情(user.uid, 订单号2,订单详情2, 状态2,充值人2,app账号2)));
                            break;
                        case "获取交易猫账号":
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "获取交易猫账号", PlatformStore.GetAll()));
                            break;
                        case "接受订单":
                            lock (this)
                            {
                                string oid = ncmd.data.ToString();
                                string uid = user.uid;
                                ns.SendObjToClient(user, new NetCommand(ncmd.gid, "接受订单", ProcessOrdersStore.Add(uid,oid)));
                            }
                            break;
                        case "完成订单":
                            lock (this)
                            {
                                string[] wcdata = (string[])ncmd.data;
                                string pdid = wcdata[0];
                                string price = wcdata[1];
                                string note = wcdata[2];
                                string poid = wcdata[3];
                                string oid = wcdata[4];
                                string state=wcdata[5];
                                ns.SendObjToClient(user, new NetCommand(ncmd.gid, "接受订单", ProcessOrdersStore.Update(pdid, price, note, poid, oid, state)));
                            }
                            break;
                        case "获取库存":
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "获取库存", InventoryStore.GetAllByState()));
                            break;
                        case "获取面值":
                            string [] hqmz=(string [])ncmd.data;
                            string appid = hqmz[0];
                            string hprice = hqmz[1];
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "获取面值", FacevalueStore.GetValue(appid,hprice)));
                            break;
                        case "今天提成": 
                            string juid=user.uid;
                            string jstime=DateTime.Now.ToString("yyyy-MM-dd");
                            string jetime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "今天提成", CommissionStore.GetTC(juid, jstime, jetime)));
                            break;
                        case "昨天提成":
                            string zuid=user.uid;
                            string zstime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                            string zetime = DateTime.Now.ToString("yyyy-MM-dd");
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "昨天提成", CommissionStore.GetTC(zuid, zstime, zetime)));
                            break;
                        case "所属游戏":
                            ns.SendObjToClient(user, new NetCommand(ncmd.gid, "所属游戏", GameStore.GetAll()));
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (ncmd.cmd == "login")
                    {
                        string msg = ncmd.data.ToString();
                        string uid = msg.Split(':')[0];
                        string pwd = msg.Split(':')[1];
                        string role = msg.Split(':')[2];
                        string []info = UserStore.Login(uid, pwd, role);
                        if (info != null)
                        {
                            user.isLogin = true;
                            user.uid = info[0];
                        }
                        ns.SendObjToClient(user, new NetCommand(ncmd.gid,"login", info));
                    }
                }
                
            }
            catch (Exception ex)
            {
                ImportDataLog.WriteLog("ReceiveData:" + ex.Message + "\r\n" +
                "触发异常方法：" + ex.TargetSite + "\r\n" +
                "异常详细信息" + ex.StackTrace + "\r\n");
            }
        }
    }
}
