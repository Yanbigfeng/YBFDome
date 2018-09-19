using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoure.DataSoure;
using ModelSoure.DataHelp;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace AdoHelp.ClassKeep
{

    /// <summary>
    /// EF并发方案
    /// </summary>


    public class Concurrent
    {
        #region 主方法
        public static void Run()
        {
            //EfFixed();
            OverMerger();
        }
        #endregion

        #region 同时修改并发
        //创建委托模拟并发
        delegate void MyDelegate(TestInfors str);
        public static void EfFixed()
        {
            MyDelegate myDelegate = new MyDelegate(Update);
            var objDb = new EFHelp<TestInfors>().objectContext();
            TestInfors tStart = new DbContext(objDb, true).Set<TestInfors>().Where(u => u.id == "1").FirstOrDefault();
            DisplayProperty("【开始前】：", tStart);
            //重新新建修改1
            TestInfors t1 = new TestInfors();
            t1.id = "1";
            t1.title = "55";
            t1.contents = "内容";
            //重新新建修改1
            TestInfors t2 = new TestInfors();
            t2.id = "1";
            t2.title = "22";
            t2.contents = "内容";
            //执行并发操作
            myDelegate.BeginInvoke(t1, null, null);
            myDelegate.BeginInvoke(t2, null, null);
            //最后
            TestInfors tFinal = new DbContext(objDb, true).Set<TestInfors>().Where(u => u.id == "1").FirstOrDefault();
            DisplayProperty("【开始前】：", tFinal);
        }

        #region 并发委托方法
        public static void Update(TestInfors t)
        {
            using (var objDb = new EFHelp<TestInfors>().objectContext())
            {
                TestInfors t3 = new DbContext(objDb, true).Set<TestInfors>().Where(u => u.id == "1").FirstOrDefault();
                try
                {
                    if (t3 != null)
                        objDb.ApplyCurrentValues("TestInfors", t);
                    System.Threading.Thread.Sleep(100);
                    objDb.SaveChanges();
                    DisplayProperty("【第一层更新】：", t);

                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    DisplayProperty("【出现了异常】：", t);
                    //刷新当前最新对象
                    objDb.Refresh(RefreshMode.ClientWins, t);
                    throw;
                }
            }

        }
        #endregion

        #endregion

        #region 延时合并
        public static void OverMerger()
        {
            TestInfors tStart;
            TestInfors tFinal;
            using (var objDb = new EFHelp<TestInfors>().baseContext())
            {
                tStart = objDb.TestInfors.Where(u => u.id == "1").FirstOrDefault();
            }
            using (var objDbc = new EFHelp<TestInfors>().baseContext())
            {
                tFinal = objDbc.TestInfors.Where(u => u.id == "1").FirstOrDefault();
            }

            //第一个修改
            tStart.title = "xigai";
            //第二个修改
            tFinal.contents = "66";
            DisplayProperty("【开始前】：", tStart);

            //保存第一个
            using (var objDb1 = new EFHelp<TestInfors>().baseContext())
            {
                try
                {
                    objDb1.Entry(tStart).State = EntityState.Modified;
                    objDb1.SaveChanges();
                    DisplayProperty("【保存第一个】：", tStart);


                }
                catch (DbUpdateConcurrencyException ex)
                {
                    DisplayProperty("【出现了异常】：", tStart);
                }
            }
            System.Threading.Thread.Sleep(1000);

            //保存第二个
            try
            {
                using (var objDb2 = new EFHelp<TestInfors>().baseContext())
                {

                  
                        objDb2.Entry(tFinal).State = EntityState.Modified;
                        objDb2.SaveChanges();
                        DisplayProperty("【保存第二个】：", tFinal);


                                
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var objDb2 = new EFHelp<TestInfors>().baseContext();
                DisplayProperty("【出现了异常】：", tFinal);
                //刷新当前最新对象
                ObjectContext db = ((IObjectContextAdapter)objDb2).ObjectContext;
                TestInfors t3 = objDb2.Set<TestInfors>().Where(u => u.id == "1").FirstOrDefault();
                db.Refresh(RefreshMode.ClientWins, t3);
                //数据对象整合操作
                t3.contents = "222";
                objDb2.Entry(t3).State = EntityState.Modified;
                objDb2.SaveChanges();
            }


        }
        #endregion



        #region 打印输出检测
        public static void DisplayProperty(string message, TestInfors text)
        {
            String data = string.Format("{0}\n  TestInfors Message:\n    Id:{1} ",
                message, text.id, text.title);
            Console.WriteLine(data);
        }
        #endregion


        #region 使用注释
        /***********************************************************************
         * 字段并发需要把相应字段的属性并发模式设置为Fixed值
         * 过期多字段修改需要增加一个版本号属性类型需要设置为timestamp类型
         ********************************************************************/
        #endregion
    }
}
