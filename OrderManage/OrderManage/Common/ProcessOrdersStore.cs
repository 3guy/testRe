using OrderManage.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class ProcessOrdersStore
    {
        //
        /// <summary>
        /// 获取处理订单
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select top 100 [订单号],[提成],[订单详情],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间,DATEDIFF( Minute, [创建时间], [processOrders].[time]) as 时间差 from [order],[processOrders],[user],[inventory] where [processOrders].[pdid]=[inventory].[id] and [order].id=[processOrders].orderid and [user].id=[processOrders].userid order by [processOrders].[time] desc");
                //string sql = string.Format("select top 100 [订单号],[订单详情],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间 from [order] left join [processOrders] on [order].id=[processOrders].orderid left join [user] on [user].id=[processOrders].userid left join [inventory] on [processOrders].[pdid]=[inventory].[id]  order by [processOrders].[time] desc");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 接受订单
        /// </summary>
        /// <returns></returns>
        internal static bool Add(string uid,string oid)
        {
            try
            {
                string exitsql = "select count(*) from [processOrders] where [orderid]='" + oid + "'";
                string count = DbHelperSQL.GetSingle(exitsql).ToString();
                if (count == "0")
                {
                    List<string> sql = new List<string>();
                    sql.Add("insert into [processOrders]([userid],[orderid]) values('" + uid + "','" + oid + "') ");
                    sql.Add("update [order] set [状态]='正在充值' where id='" + oid + "'");
                    int num = DbHelperSQL.ExecuteSqlTran(sql);
                    if (num > 0)
                        return true;
                    return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        internal static bool Update(string pdid,string price,string note,string poid,string oid,string state)
        {
            try
            {
                List<string> sql = new List<string>();
                sql.Add("update [order] set [状态]='"+state+"' where id='"+oid+"'");
                sql.Add("update [processOrders] set [pdid]='" + pdid + "',[price]='" + price + "',[note]='" + note + "',[money]=(select [money] from [inventory] where id='" + pdid + "'),time='"+DateTime.Now.ToString()+"' where id=" + poid);
                sql.Add("update [inventory] set [money]=[money]-" + price + " where id='" + pdid + "'");

                int num = DbHelperSQL.ExecuteSqlTran(sql);
                if (num > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal static DataTable 查询当前用户已充值(string 充值人id)
        {
            try
            {
                string sql = string.Format("select top 50 [提成],[订单号],[订单详情],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间, DATEDIFF( Minute, [创建时间], [processOrders].[time]) as 时间差 from [order],[processOrders],[user],[inventory] where [processOrders].[pdid]=[inventory].[id] and [order].id=[processOrders].orderid and [user].id=[processOrders].userid ");
                sql = sql + " and [user].[id]='" + 充值人id + "' ";
                sql = sql + " order by [processOrders].[time] desc";
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static DataTable 查询订单详情(string 创建人id)
        {
            try
            {
                string sql = string.Format("select top 100 [订单号],[订单详情],[提成],[状态],[创建时间],[备注],[user].[name] as 处理人,[processOrders].[note] as 销售备注 ,[inventory].appid,[processOrders].[price],[processOrders].[money],[processOrders].[time] from [order] left join [processOrders] on [order].id=[processOrders].orderid left join [user] on [user].id=[processOrders].userid left join [inventory] on [processOrders].[pdid]=[inventory].[id]  order by [processOrders].[time] desc");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static DataTable 条件查询订单详情(string 创建人id, string 订单号, string 订单详情, string 状态, string 充值人, string app账号)
        {
            try
            {
                string sql = string.Format("select [订单号],[订单详情],[状态],[提成],[创建时间],[备注],[user].[name] as 处理人,[processOrders].[note] as 销售备注 ,[inventory].appid,[processOrders].[price],[processOrders].[money],[processOrders].[time] from [order] left join [processOrders] on [order].id=[processOrders].orderid left join [user] on [user].id=[processOrders].userid left join [inventory] on [processOrders].[pdid]=[inventory].[id] where 1=1 ");
                if (订单详情 != "")
                {
                    sql = sql + " and [订单详情] like '%" + 订单详情 + "%' ";
                }
                if (订单号 != "")
                {
                    sql = sql + " and 订单号='" + 订单号 + "' ";
                }
                if (状态 != "")
                {
                    sql = sql + " and [状态]='" + 状态 + "' ";
                } 
                if (充值人 != "")
                {
                    sql = sql + " and [user].[name]='" + 充值人 + "' ";
                }
                if (app账号 != "")
                {
                    sql = sql + " and [inventory].[appid]='" + app账号 + "' ";
                }
                sql = sql + " and  创建时间 between  dateadd([day],-13,getdate()) and getdate()  order by [processOrders].[time] desc";
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static string sum(string stime,string etime,string 订单号,string 订单详情,string 充值人,string app账号,string 状态)
        {
            try
            {
                string sql = string.Format("select sum([price]) from [order],[processOrders],[user],[inventory] where [processOrders].[pdid]=[inventory].[id] and [order].id=[processOrders].orderid and [user].id=[processOrders].userid and [order].备注 like '系统自动采集' and [order].状态='充值成功'");
                //string sql = "select [订单号],[订单详情],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间 from [order] left join [processOrders] on [order].id=[processOrders].orderid left join [user] on [user].id=[processOrders].userid left join [inventory] on [processOrders].[pdid]=[inventory].[id] ";
                if (stime != "")
                {
                    sql = sql + " and [processOrders].time BETWEEN '" + stime + "' and '" + etime + "'";
                }
                if (订单号 != "")
                {
                    sql = sql + " and 订单号='" + 订单号 + "' ";
                }
                if (订单详情 != "")
                {
                    sql = sql + " and [订单详情] like '%" + 订单详情 + "%' ";
                }
                if (充值人 != "")
                {
                    sql = sql + " and [user].[name]='" + 充值人 + "' ";
                }
                if (状态 != "")
                {
                    sql = sql + " and [状态]='" + 状态 + "' ";
                }
                if (app账号 != "")
                {
                    sql = sql + " and [inventory].[appid]='" + app账号 + "' ";
                }
                //sql = sql + " order by [processOrders].[time] desc";
                return DbHelperSQL.GetSingle(sql).ToString();
            }
            catch
            {
                return "";
            }
        }

        internal static string 订单总金额(string stime, string etime, string 订单号, string 订单详情, string 充值人, string app账号, string 状态)
        {
            try
            {
                string sql = string.Format("select sum(CAST(SubString([order].订单详情,CHARINDEX('订单总价：',[order].订单详情)+5,CHARINDEX(char(10),[order].订单详情,CHARINDEX('订单总价：',[order].订单详情))-CHARINDEX('订单总价：',[order].订单详情)-6) AS decimal(18,3))) from [order],[processOrders],[user],[inventory] where [processOrders].[pdid]=[inventory].[id] and [order].id=[processOrders].orderid and [user].id=[processOrders].userid and [order].备注 like '系统自动采集' and [order].状态='充值成功'");
                //string sql = "select [订单号],[订单详情],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间 from [order] left join [processOrders] on [order].id=[processOrders].orderid left join [user] on [user].id=[processOrders].userid left join [inventory] on [processOrders].[pdid]=[inventory].[id] ";
                if (stime != "")
                {
                    sql = sql + " and [processOrders].time BETWEEN '" + stime + "' and '" + etime + "'";
                }
                if (订单号 != "")
                {
                    sql = sql + " and 订单号='" + 订单号 + "' ";
                }
                if (订单详情 != "")
                {
                    sql = sql + " and [订单详情] like '%" + 订单详情 + "%' ";
                }
                if (充值人 != "")
                {
                    sql = sql + " and [user].[name]='" + 充值人 + "' ";
                }
                if (状态 != "")
                {
                    sql = sql + " and [状态]='" + 状态 + "' ";
                }
                if (app账号 != "")
                {
                    sql = sql + " and [inventory].[appid]='" + app账号 + "' ";
                }
                //sql = sql + " order by [processOrders].[time] desc";
                return DbHelperSQL.GetSingle(sql).ToString();
            }
            catch
            {
                return "";
            }
        }

        internal static DataSet Serach(string stime, string etime, string 订单号, string 订单详情, string 充值人, string app账号, string 状态,string 所属游戏)
        {
            try
            {
                string sql = string.Format("select [订单号],[订单详情],[提成],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间,DATEDIFF( Minute, [创建时间], [processOrders].[time]) as 时间差 from [order],[processOrders],[user],[inventory] where [processOrders].[pdid]=[inventory].[id] and [order].id=[processOrders].orderid and [user].id=[processOrders].userid ");
                //string sql = "select [订单号],[订单详情],[状态],[创建时间],[备注],[processOrders].[money],[processOrders].[note] as 订单备注,[user].[name] as 处理人,下单人=(select [name] from [user] where [user].id=[order].创建人),[inventory].[appid] as app账号,[processOrders].[price],[processOrders].[note] as 充值备注,[processOrders].[time] as 完成时间 from [order] left join [processOrders] on [order].id=[processOrders].orderid left join [user] on [user].id=[processOrders].userid left join [inventory] on [processOrders].[pdid]=[inventory].[id] ";
                if (stime != "")
                {
                    sql = sql + " and 创建时间 BETWEEN '" + stime + "' and '" + etime + "'";
                }
                if (订单号 != "")
                {
                    sql = sql + " and 订单号='" + 订单号 + "' ";
                }
                if (订单详情 != "")
                {
                    sql = sql + " and [订单详情] like '%" + 订单详情 + "%' ";
                }
                if (充值人 != "")
                {
                    sql = sql + " and [user].[name]='" + 充值人 + "' ";
                }
                if (状态 != "")
                {
                    sql = sql + " and [状态]='" + 状态 + "' ";
                }
                if (app账号 != "")
                {
                    sql = sql + " and [inventory].[appid]='" + app账号 + "' ";
                }
                if (所属游戏 != "")
                {
                    sql = sql + "and REPLACE(dbo.str_get([订单详情],'所属游戏：',char(10)),'【苹果版】','') like '%"+所属游戏+"%'";
                }

                sql = sql + " order by [processOrders].[time] desc";
                return DbHelperSQL.Query(sql);
            }
            catch
            {
                return null;
            }
        }
    }
}
