using OrderManage.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    /// <summary>
    /// 操作表account类
    /// </summary>
    internal class OrderStore
    {
        internal static string FilterSql(string InText) 
        {
            if (InText == "")
                return InText;
            else
            {
                //InText = InText.replace("and ", "");
                //InText = InText.replace("exec ", "");
                //InText = InText.replace("insert ", "");
                //InText = InText.replace("select ", "");
                //InText = InText.replace("delete ", "");
                //InText = InText.replace("update ", "");
                //InText = InText.replace(" and", "");
                //InText = InText.replace(" exec", "");
                //InText = InText.replace(" insert", "");
                //InText = InText.replace(" select", "");
                //InText = InText.replace(" delete", "");
                //InText = InText.replace(" update ", "");
                //InText = InText.replace("chr ", "");
                //InText = InText.replace("mid ", "");
                //InText = InText.replace(" chr", "");
                //InText = InText.replace(" mid", "");
                //InText = InText.replace("master ", "");
                //InText = InText.replace(" master", "");
                //InText = InText.replace("or ", "");
                //InText = InText.replace(" or", "");
                //InText = InText.replace("truncate ", "");
                //InText = InText.replace("char ", "");
                //InText = InText.replace("declare ", "");
                //InText = InText.replace("join ", "");
                //InText = InText.replace("union ", "");
                //InText = InText.replace("truncate ", "");
                //InText = InText.replace(" char", "");
                //InText = InText.replace(" declare", "");
                //InText = InText.replace(" join", "");
                //InText = InText.replace(" union", "");
                InText = InText.Replace("'", "''");
                //InText = InText.replace("<", "");
                //InText = InText.replace(">", "");
                //InText = InText.replace("%", "");
                //InText = InText.replace("'delete", "");
                //InText = InText.replace("''", "");
                //InText = InText.replace("/" /"", "");
                //InText = InText.replace(",", "");
                //InText = InText.replace(">=", "");
                //InText = InText.replace("=<", "");
                //InText = InText.replace("--", "");
                //InText = InText.replace("_", "");
                //InText = InText.replace(";", "");
                //InText = InText.replace("||", "");
                //InText = InText.replace("[", "");
                //InText = InText.replace("]", "");
                //InText = InText.replace("&", "");
                //InText = InText.replace("/", "");
                //InText = InText.replace("?", "");
                //InText = InText.replace(">?", "");
                //InText = InText.replace("?<", "");
                //InText = InText.replace(" ", "");
                return InText;
            }
        }

        internal static bool Add(string 订单号,string 订单详情,string 备注,string 创建人)
        {
            try
            {
                创建人 = FilterSql(创建人);
                订单详情 = FilterSql(订单详情);
                备注 = FilterSql(备注);
                订单号 = FilterSql(订单号);

                string 所属游戏=StringHelper.截取文本(订单详情, "所属游戏：", "\r\n",0);
                string 账号类型 = StringHelper.截取文本(订单详情, "账号类型：", "\r\n", 0);
                string 提成=CommissionStore.获取提成(所属游戏,账号类型);

                string sql = "insert into [order]([订单号],[订单详情],[备注],[创建人],[提成]) values('" + 订单号 + "','" + 订单详情 + "','" + 备注 + "','" + 创建人 + "','" + 提成 + "')";
                int num = DbHelperSQL.ExecuteSql(sql);
                if (num > 0)
                    return true;
                return false;
            }
            catch{
                return false;
            }
        }

        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select top 100 [订单号],[提成],[订单详情],[状态],[创建时间],[备注],姓名=(select [name] from [user] where id=创建人) from [order] order by 创建时间");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static DataTable 待处理订单()
        {
            try
            {
                string sql = string.Format("select id,[订单号],[订单详情],[创建时间],[备注],姓名=(select [name] from [user] where id=创建人) from [order] where [状态]='等待充值' order by 创建时间");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static DataTable 正在处理的订单(string uid)
        {
            try
            {
                string sql = string.Format("select processOrders.id as pid,[order].id as oid,[订单号],[订单详情],[状态],[创建时间],[备注],姓名=(select [name] from [user] where id=创建人) from [order],processOrders where [order].id=processOrders.orderid and [状态]='正在充值' and processOrders.userid='" + uid + "' order by 创建时间");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static bool IsExist(string orderid)
        {
            try
            {
                string sql = string.Format("select 1 from [order] where [订单号]='"+orderid+"'");
                if (DbHelperSQL.Query(sql).Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        internal static bool IsSuccess(string orderid)
        {
            try
            {
                string sql = string.Format("select 1 from [order] where [订单号]='" + orderid + "' and 状态='充值成功'");
                if (DbHelperSQL.Query(sql).Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        internal static DataTable Serach(string stime, string etime, string 订单号, string 订单详情)
        {
            try
            {
                string sql = string.Format("select [订单号],[订单详情],[提成],[状态],[创建时间],[备注],姓名=(select [name] from [user] where id=创建人) from [order] where 1=1");
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
                sql = sql + " order by 创建时间";
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

    }
}
