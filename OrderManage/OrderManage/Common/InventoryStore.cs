using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class InventoryStore
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select * from [inventory]");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static DataTable GetAllByState()
        {
            try
            {
                string sql = string.Format("select * from [inventory] where [state]='启用'");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static bool Add(string appid, string money)
        {
            try
            {
                string sql = string.Format("insert into [inventory]([appid],[money]) values('" + appid + "','" + money + "') ");
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

        internal static bool Update(string id, string appid, string money,string state)
        {
            try
            {
                string sql = "update [inventory] set [appid]='" + appid + "',[money]='" + money + "',[state]='" + state + "' where id=" + id;
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
