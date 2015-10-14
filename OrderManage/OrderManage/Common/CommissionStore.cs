using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class CommissionStore
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select commission.id,gameid,price,gamename,[type],commission.[time] from commission,game where commission.gameid=game.id");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        internal static bool Add(string gameid,string type,string price)
        {
            try
            {
                string sql = string.Format("INSERT INTO [commission]([gameid],[price],[type]) VALUES('"+gameid+"','"+price+"','"+type+"')");
                int num = DbHelperSQL.ExecuteSql(sql);
                if (num > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal static bool Update(string id, string gameid, string type, string price)
        {
            try
            {
                string sql = "UPDATE [Order].[dbo].[commission] SET [gameid] = '"+gameid+"',[price] = '"+price+"',[type] ='"+type+"' WHERE id='" + id + "'";
                int num = DbHelperSQL.ExecuteSql(sql);
                if (num > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal static bool Delete(string id)
        {
            try
            {
                string sql = string.Format("delete from [commission] where id='{0}'", id);
                int num = DbHelperSQL.ExecuteSql(sql);
                if (num > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        internal static string 获取提成(string gamename,string type)
        {
            try
            {
                string sql = string.Format("select price from commission,game where commission.gameid=game.id and gamename ='" + gamename + "' and type='" + type + "'");
                object obj = DbHelperSQL.GetSingle(sql);
                if (obj == null)
                {
                    return "0.500";
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch
            {
                return "0.500";
            }
        }


        /// <summary>
        /// 获取分成
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetCommission(string userid,string stime,string etime)
        {
            try
            {
                string sql = string.Format("select 提成,count(提成) as ordersum,sum(提成) as num from processOrders ,[order] where [order].id=orderid ");
                if (userid!="")
                {
                    sql += " and userid='"+userid+"' ";
                }
                if (stime != "" && etime != "")
                {
                    sql += " and [order].创建时间 between '"+stime+"' and '"+etime+"'";
                }
                sql += " and 状态='充值成功' and pdid<>'25' group by 提成";
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 获取分成
        /// </summary>
        /// <returns></returns>
        internal static string GetTC(string userid, string stime, string etime)
        {
            try
            {
                string sql = string.Format("select sum(提成) as num from processOrders ,[order] where [order].id=orderid ");
                if (userid != "")
                {
                    sql += " and userid='" + userid + "' ";
                }
                if (stime != "" && etime != "")
                {
                    sql += " and [order].创建时间 between '" + stime + "' and '" + etime + "'";
                }
                sql += " and 状态='充值成功' and pdid<>'25'";
                object obj=DbHelperSQL.GetSingle(sql);
                if (obj == null)
                {
                    return "";
                }
                return obj.ToString();
            }
            catch
            {
                return null;
            }
        }

    }
}
