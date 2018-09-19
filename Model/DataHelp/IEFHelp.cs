using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace ModelSoure.DataHelp
{
     interface IEFHelp<T>
    {
        #region 0.数据源
         IQueryable<T> Entities
        {
            get;
          
        }
        #endregion

        #region 1.新增
         int Add(T model);
        #endregion

        #region 2.删除
         int Del(T model);
        #endregion

        #region 3.修改
        int Modify(T model);
        #endregion


        //事务性的封装

        #region a1.批量处理SaveChange()
        int SaveChange();

        #endregion



        #region b.新增
        void AddNo(T model);

        #endregion

        #region c.删除
        void DelNo(T model);

        #endregion

        #region d.根据条件删除
        void DelByNo(Expression<Func<T, bool>> delWhere);

        #endregion

        #region e.修改

        void ModifyNo(T model);

        #endregion

        //EF调用sql语句

        #region A.执行增加,删除,修改操作(或调用存储过程)
        int ExecuteSql(string sql, params SqlParameter[] pars);

        #endregion

        #region B.执行查询操作
        List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars);

        #endregion
    }

}
