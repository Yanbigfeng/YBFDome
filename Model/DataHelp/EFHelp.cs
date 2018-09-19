using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.SqlClient;
using ModelSoure.DataSoure;
using System.Data.Entity.Core.Objects;

namespace ModelSoure.DataHelp
{
    public class EFHelp<T> : IEFHelp<T> where T : class, new()
    {

        public static readonly DbContext db = new testEntities();


        #region 原始上下文
        public testEntities baseContext()
        {
            return new testEntities();
        }
        #endregion

        #region ObjectContext方式
        public ObjectContext objectContext()
        {
            ObjectContext objectContext= new ObjectContext("name=testEntities");
            objectContext.DefaultContainerName = "testEntities";
            return objectContext; ;
        }
        #endregion

        #region dbConten方式
        public DbContext dbContext()
        {
            return db;
        }
        #endregion


        #region 0.数据源
        public IQueryable<T> Entities
        {
            get
            {
                return db.Set<T>();
            }
        }
        #endregion

        #region 1.添加
        public int Add(T model)
        {
            int count = -1;
            DbSet<T> dst = db.Set<T>();
            dst.Add(model);
            //db.Entry(model).State = EntityState.Modified;
            return count = db.SaveChanges();
        }
        #endregion

        #region 2.删除
        public int Del(T model)
        {
            int iret = -1;
            //Logger("根据ID删除" + typeof(T).Name + "中的数据", () =>
            //{
            db.Set<T>().Attach(model);
            db.Set<T>().Remove(model);
            iret = db.SaveChanges();
            //});           
            return iret;
        }
        #endregion

        #region 3.修改
        public int Modify(T model)
        {
            int iret = -1;
            db.Entry(model).State = EntityState.Modified;
            iret = db.SaveChanges();
            return iret;
        }
        #endregion


        //事务性的封装

        #region a1.批量处理SaveChange()
        public int SaveChange()
        {
            return db.SaveChanges();


        }
        #endregion

        #region b.新增
        public void AddNo(T model)
        {
            db.Set<T>().Add(model);
        }
        #endregion

        #region c.删除(根据主键删除)
        public void DelNo(T model)
        {
            //db.Set<T>().Attach(model);
            //db.Set<T>().Remove(model);
            db.Entry(model).State = EntityState.Deleted;
        }
        #endregion

        #region d.根据条件删除
        public void DelByNo(Expression<Func<T, bool>> delWhere)
        {
            List<T> listDels = db.Set<T>().Where(delWhere).ToList();
            listDels.ForEach(d =>
            {
                db.Set<T>().Attach(d);
                db.Set<T>().Remove(d);
            });
        }
        #endregion

        #region e.修改
        public void ModifyNo(T model)
        {
            db.Entry(model).State = EntityState.Modified;
        }
        #endregion

        //EF调用sql语句

        #region A.执行增加,删除,修改操作(或调用存储过程)
        public int ExecuteSql(string sql, params SqlParameter[] pars)
        {
            return db.Database.ExecuteSqlCommand(sql, pars);
        }

        #endregion

        #region B.执行查询操作
        public List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars)
        {
            return db.Database.SqlQuery<T>(sql, pars).ToList();
        }
        #endregion

    }
}
