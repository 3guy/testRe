using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class UserStore
    {
        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        internal static bool Add(string userid,string pwd,string name,string role)
        {
            try
            {
                string sql = string.Format("insert into [user]([uid],[pwd],[name],[role]) values('" + userid + "','" + pwd + "','" + name + "','" + role + "')");
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

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        internal static string[] Login(string uid,string pwd,string role)
        {
            try
            {
                string sql = string.Format("select [id],[name] from [user] where uid='{0}' and pwd='{1}' and role='{2}'", uid, pwd,role);
                string[] info = new string[1];
                DataTable dt=DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count <= 0)
                    return null;
                else
                    return new string[] { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString() };
            }
            catch
            {
                return null;
            }
        }

        internal static bool Delete(string id)
        {
            try
            {
                string sql = string.Format("delete from [user] where id='{0}'", id);
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

        internal static bool Update(string id, string uid, string pwd, string name, string role)
        {
            try
            {
                string sql = "update [user] set [name]='" + name + "',[uid]='" + uid + "',[pwd]='" + pwd + "',[role]='" + role + "' where id=" + id;
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
                string sql = string.Format("select * from [user] ");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有充值员
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAllCZY()
        {
            try
            {
                string sql = string.Format("select [name],id from [user] where [role]='充值员'");
                return DbHelperSQL.Query(sql).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        internal static string GetUserID(string name)
        {
            try
            {
                string sql = string.Format("select id from [user] where [name] ='"+name+"'");
                DataTable dt=DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return null;
            }
        }
    }
}
