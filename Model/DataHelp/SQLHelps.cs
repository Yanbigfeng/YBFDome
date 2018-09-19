using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Model.DataHelp
{
    public class SQLHelps
    {
        static readonly string connStr = ConfigurationManager.ConnectionStrings["DataModelContext"].ConnectionString;

        #region 1.基础方法
        /// <summary>
        /// 执行一个非查询语句，返回受影响行数，如果实行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="paras">需要的参数</param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType type, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    cmd.CommandType = type;
                    conn.Open();

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        /// <summary>
        /// 执行一个查询的SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="paras">参数</param>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, CommandType type, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    cmd.CommandType = type;
                    conn.Open();

                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 读取数据，得到一个SqlDataReader对象，如果出现SQL语句执行错误，那么抛出异常
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string cmdText, CommandType type, params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(connStr);
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                cmd.CommandType = type;
                conn.Open();
                try
                {
                    // 执行SQL语句的时候出现异常
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    throw ex;
                }
            }
        }

        public static DataTable JKAdapter(string cmdText, CommandType type, params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmdText, connStr))
            {
                if (paras != null)
                {
                    sda.SelectCommand.Parameters.AddRange(paras);
                }
                sda.SelectCommand.CommandType = type;
                sda.Fill(dt);
            }
            return dt;
        }
        #endregion

        #region 2.sql语句调用
        // 重载方法与常用方法
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] paras)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, paras);
        }
        public static object ExecuteScalar(string cmdText, params SqlParameter[] paras)
        {

            return ExecuteScalar(cmdText, CommandType.Text, paras);
        }
        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] paras)
        {
            return ExecuteReader(cmdText, CommandType.Text, paras);
        }
        public static DataTable JKAdapter(string cmdText, params SqlParameter[] paras)
        {
            return JKAdapter(cmdText, CommandType.Text, paras);
        }

        #endregion

        #region 3.储存过程的调用
        public static int ExecuteNonQuerySP(string cmdText, params SqlParameter[] paras)
        {
            return ExecuteNonQuery(cmdText, CommandType.StoredProcedure, paras);
        }
        public static object ExecuteScalarSP(string cmdText, params SqlParameter[] paras)
        {
            return ExecuteScalar(cmdText, CommandType.StoredProcedure, paras);
        }
        public static SqlDataReader ExecuteReaderSP(string cmdText, params SqlParameter[] paras)
        {
            return ExecuteReader(cmdText, CommandType.StoredProcedure, paras);
        }
        public static DataTable JKAdapterSP(string cmdText, params SqlParameter[] paras)
        {
            return JKAdapter(cmdText, CommandType.StoredProcedure, paras);
        }
        #endregion

        #region 4.事物操作例子

        public static SqlConnection conn2 = new SqlConnection(connStr);
        public static int ExecuteNonQueryNo(List<string> cmdText)
        {
            return ExecuteNonQueryNo(cmdText, CommandType.Text);
        }

        /// <summary>
        /// 执行一个非查询语句，返回受影响行数，如果实行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQueryNo(List<string> cmdText, CommandType type)
        {
            int count = 0;
            conn2.Open();
            //创建事物  
            SqlTransaction st = conn2.BeginTransaction();
            using (SqlCommand cmd = new SqlCommand(null, conn2))
            {
                try
                {
                    cmd.Transaction = st;
                    if (cmdText != null)
                    {
                        foreach (var text in cmdText)
                        {
                            cmd.CommandText = text;
                            cmd.ExecuteNonQuery();
                            count++;
                        }
                    }
                    //提交事务
                    st.Commit();
                    conn2.Close();
                    return count;
                }
                catch (Exception e)
                {
                    st.Rollback();
                    conn2.Close();
                    throw;
                }

            }
        } 

        #endregion
    }
}
