using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class FacevalueStore
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select [inventory].appid as app,[facevalue].gamename,[facevalue].price,[facevalue].[value],[facevalue].id,[facevalue].appid from [facevalue] ,[inventory] where [inventory].id=[facevalue].appid ");
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
        internal static DataTable GetAll(string appid,string price)
        {
            try
            {
                string sql = string.Format("select [inventory].appid as app,[facevalue].gamename,[facevalue].price,[facevalue].[value],[facevalue].id,[facevalue].appid from [facevalue] ,[inventory] where [inventory].id=[facevalue].appid ");
                if (appid != "")
                {
                    sql = sql + " and [facevalue].appid ='" + appid + "'";
                }
                if (price != "")
                {
                    sql = sql + " and price='" + price + "' ";
                }
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取面值
        /// </summary>
        /// <returns></returns>
        internal static string GetValue(string appid,string price)
        {
            try
            {
                string sql = string.Format("select value from [facevalue] where appid='" + appid + "' and cast(price as decimal(18, 3))=cast('" + price + "' as decimal(18, 3))");
                return DbHelperSQL.GetSingle(sql).ToString();
            }
            catch
            {
                return "null";
            }
        }

        internal static bool Add(string appid, string gamename,string price,string value)
        {
            try
            {
                string sql="insert into [facevalue]([appid],[gamename],[price],[value]) values('" + appid + "','" + gamename + "','" + price + "','" + value + "') ";
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

        internal static bool Update(string id, string appid, string gamename, string price, string value)
        {
            try
            {
                string sql = "update [facevalue] set [appid]='" + appid + "',[gamename]='" + gamename + "',[price]='" + price + "',[value]='" + value + "' where id='" + id + "'";
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
                string sql = string.Format("delete from [facevalue] where id='{0}'", id);
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
    }
}
