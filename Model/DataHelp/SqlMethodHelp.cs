using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Model.DataHelp;
using System.Data;

namespace Model.DataHelp
{
    class SqlMethodHelp
    {
        #region 1.表的操作
        /// <summary>
        /// 创建表
        /// </summary>
        public static int AddTable()
        {
            string sql = "create table tabname(id int primary key )";
            return SQLHelps.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除表
        /// </summary>
        public static int DropTable()
        {
            string sql = "drop table tabname";
            return SQLHelps.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加列
        /// </summary>
        public static int AddCloumn(string column, string type)
        {
            string sql = "Alter table tabname add " + column + " " + type;
            return SQLHelps.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询数据库所有表名
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader SearchTable()
        {
            string sql = "select name from sysobjects where type='U'";
            return SQLHelps.ExecuteReader(sql);
        }
        public static SqlDataReader SearchColumn(string table)
        {
            string sql = "select name from syscolumns where id=object_id('" + table + "')";
            return SQLHelps.ExecuteReader(sql);
        }

        #endregion

        #region 2.数据基本操作
        /// <summary>
        /// 添加数据
        /// </summary>
        public static int Add()
        {
            string sql = "insert into BaseTable(name,age) values(@name,@age)";
            return SQLHelps.ExecuteNonQuery(sql, new SqlParameter("@name", "张三"), new SqlParameter("@age", 18));
        }
        /// <summary>
        /// 查询指定表的数据
        /// </summary>
        /// <param name="table"></param>
        public static SqlDataReader Search(string table)
        {
            string sql = "select count(*) from " + table;
            return SQLHelps.ExecuteReader(sql);
        }
        /// <summary>
        /// 根据条件模糊查询指定表总数
        /// </summary>
        /// <param name="table">查询的表</param>
        /// <param name="value">条件值</param>
        /// <returns></returns>
        public static SqlDataReader SearchLike(string table, string value)
        {
            string sql = "select count(*) from " + table + " where name like '%" + value + "%'";
            return SQLHelps.ExecuteReader(sql);
        }
        /// <summary>
        /// 根据条件修改指定表字段的值
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="column1">修改的列</param>
        /// <param name="value1">修改的值</param>
        /// <param name="column2">条件列</param>
        /// <param name="value2">条件值</param>
        /// <returns></returns>
        public static int Update(string table, string column1, string value1, string column2, string value2)
        {
            string sql = "update " + table + " set " + column1 + "=" + value1 + " where " + column2 + "=" + value2;
            return (int)SQLHelps.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="column">列名</param>
        /// <returns></returns>
        public static int Sum(string table, string column)
        {
            string sql = "select sum(" + column + ") as sumvalue from " + table;
            return (int)SQLHelps.ExecuteScalar(sql);
        }
        /// <summary>
        /// 求平均数
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static int Avg(string table, string column)
        {
            string sql = "select avg(" + column + ") as avgvalue from " + table;
            return (int)SQLHelps.ExecuteScalar(sql);
        }
        /// <summary>
        /// 查询范围
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="column">列名</param>
        /// <param name="value1">开始值</param>
        /// <param name="value2">结束值</param>
        /// <returns></returns>
        public static SqlDataReader Between(string table, string column, string value1, string value2)
        {
            string sql = "select * from " + table + " where " + column + " between " + value1 + " and " + value2;
            return SQLHelps.ExecuteReader(sql);
        }
        #endregion

        #region 3.储存过程的调用

        public static SqlDataReader ProcBaseTable(string procname,params SqlParameter[] param)
        {
            return SQLHelps.ExecuteReaderSP(procname,param);
        }
        #endregion

        #region 4.事物操作

        #region 4.1数组模式调用示例
        public static void UpdateNo(List<string> list)
        {
            SQLHelps.ExecuteNonQueryNo(list);
        }
        #endregion

        #region 4.2参数模式
        public static List<string> SqlList = new List<string>();
        /// <summary>
        /// 批量操作（非查询）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        public static void UpdateNo(string sql, params SqlParameter[] paras)
        {
            if (paras != null)
            {
                foreach (var p in paras)
                {
                    sql = sql.Replace(p.ParameterName.ToString(), p.Value.ToString());
                }
            }
            SqlList.Add(sql);

        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public static void Save()
        {
            SQLHelps.ExecuteNonQueryNo(SqlList);
        }
        #endregion

        #endregion

        #region 5.关联表的操作
        /// <summary>
        /// 连接查询
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader Join()
        {
            //内连接
            string sql = "select * from BaseTable join tabname on BaseTable.id=tabname.id";
            //左连接
            string sql1 = "select * from BaseTable left join tabname on BaseTable.id=tabname.id";
            //右连接
            string sql2 = "select * from BaseTable right join tabname on BaseTable.id=tabname.id";
            //完全外连接
            string sql3 = "select * from BaseTable full join tabname on BaseTable.id=tabname.id";
            //交叉外连接
            string sql4 = "select * from BaseTable cross join tabname ";
            return SQLHelps.ExecuteReader(sql);

        }
        /// <summary>
        /// Union
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader Union()
        {
            //检查
            string sql = "select * from BaseTable union select * from  Tables";
            //不检查
            string sql1 = "select * from BaseTable union select * from  Tables";
            return SQLHelps.ExecuteReader(sql);

        }
        /// <summary>
        /// except
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader Except()
        {
            string sql = "select id from BaseTable except select id from tabname";
            return SQLHelps.ExecuteReader(sql);

        }

        /// <summary>
        /// Intersect
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader Intersect()
        {
            string sql = "select id from BaseTable intersect select id from tabname";
            return SQLHelps.ExecuteReader(sql);

        }
        #endregion

    }
}
