using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrderManage.Common
{
    internal class GameStore
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAll()
        {
            try
            {
                string sql = string.Format("select * from game");
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
        internal static bool Add(string gamename)
        {
            try
            {
                string sql = string.Format("INSERT INTO [game]([gamename]) VALUES('" + gamename + "')");
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

        internal static bool Update(string id, string gamename)
        {
            try
            {
                string sql = "UPDATE [game] SET [gamename] = '" + gamename + "' WHERE id='" + id + "'";
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
    }
}
