using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class PlatformStore
    {
        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        internal static bool Add(string userid,string pwd)
        {
            try
            {
                string sql = string.Format("insert into [platform]([账号],[密码]) values('" + userid + "','" + pwd + "')");
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
                string sql = string.Format("delete from [platform] where id='{0}'", id);
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

        internal static bool Update(string id, string uid, string pwd)
        {
            try
            {
                string sql = "update [platform] set [账号]='" + uid + "',[密码]='" + pwd + "' where id=" + id;
                int num = DbHelperSQL.ExecuteSql(sql);
                if (num > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }



        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select 账号,密码,id from [platform] ");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
