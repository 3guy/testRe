using PlaceOrder.Common;
using PlaceOrder.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PlaceOrder.Common
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    internal class AccessStore
    {
        /// <summary>
        /// 添加一个问题
        /// </summary>
        /// <returns></returns>
        internal static bool AddWenti(string wenti)
        {
            try
            {
                string sql = string.Format("Insert INTO 自动回复 (问题) VALUES ('{0}')", wenti);
                int num=DBHelper.ExecuteSql(sql);
                if (num > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 添加一个答案
        /// </summary>
        /// <returns></returns>
        internal static bool AddDaAn(string wenti,string daan)
        {
            try
            {
                string csql = string.Format("select id from 自动回复 where 问题 = '{0}' and( 答案 is null or 答案='')",wenti);
                string id = DBHelper.GetSingle(csql).ToString();
                string sql = string.Format("update 自动回复 set 答案='{0}' where id={1}", daan,id);
                int num = DBHelper.ExecuteSql(sql);
                if (num > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 根据问题得到答案
        /// </summary>
        /// <param name="wenti"></param>
        /// <returns></returns>
        internal static string GetAnwser(string wenti)
        {
            try
            {
                //亲，把艾欧小艾这个留给我吧！
                //把+留
                string sql = string.Format("select 答案 from 自动回复 where '{0}' like 问题 and 是否自动回复='1' order by id desc", wenti);
                return DBHelper.GetSingle(sql).ToString();
            }
            catch
            { return ""; }
        }

        /// <summary>
        /// 获取所有问题答案
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAllWentiDaan()
        {
            try
            {
                string sql = string.Format("select * from 自动回复 order by 是否自动回复,id desc");
                return DBHelper.QueryDataTable(sql);
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 获取所有问题答案
        /// </summary>
        /// <returns></returns>
        internal static DataTable Serach(string msg)
        {
            try
            {
                string sql = string.Format("select * from 自动回复 where '{0}' like 问题 or '{0}' like 答案 order by id desc",msg);
                return DBHelper.QueryDataTable(sql);
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static bool Delete(string id)
        {
            try
            {
                string sql = string.Format("delete from 自动回复 where id={0}", id);
                int num = DBHelper.ExecuteSql(sql);
                if (num > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 根据id修改数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="wenti"></param>
        /// <param name="daan"></param>
        /// <param name="issave"></param>
        /// <returns></returns>
        internal static bool Save(string id, string wenti, string daan, string issave)
        {
            try
            {
                string sql = string.Format("update 自动回复 set 问题='{0}',答案='{1}',是否自动回复='{2}' where id={3}", wenti,daan,issave,id);
                int num = DBHelper.ExecuteSql(sql);
                if (num > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 添加一个自动回复
        /// </summary>
        /// <param name="wenti"></param>
        /// <param name="daan"></param>
        /// <param name="issave"></param>
        /// <returns></returns>
        internal static bool Add(string wenti, string daan, string issave)
        {
            try
            {
                string sql = string.Format("insert into 自动回复(问题,答案,是否自动回复) values('{0}','{1}','{2}')", wenti, daan, issave);
                int num = DBHelper.ExecuteSql(sql);
                if (num > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            { return false; }
        }

    }
}
