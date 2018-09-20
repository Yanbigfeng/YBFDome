using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoure.DataSoure;

namespace AdoHelp.Concurrency
{
    /// <summary>
    /// EF
    /// </summary>
    class EFLockTest
    {
        #region 查询票信息
        public static int GetData()
        {
            using (var db = new testEntities())
            {
                string sqlstr = "select totalNub  FROM [dbo].[TicketTest] with(NOLOCK)";
                int totalNub = db.Database.SqlQuery<int>(sqlstr).ToList().First();
                Console.WriteLine($"获取现在剩余票数:{totalNub}");
                return totalNub;
            }
        }
        #endregion

        #region 悲观锁
        /// <summary>
        /// 悲观锁
        /// </summary>
        public static void EFUPDLOCK()
        {
            using (var db = new testEntities())
            {
                Console.WriteLine("开始抢票！抢票有延迟时间期间别人不可修改数据只可查询");
                string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                Console.WriteLine($"线程{threadId}开始抢票");
                //更新锁延迟
                string sqlstr = "SELECT *FROM[dbo].[TicketTest] with(UPDlock) where id='0001' waitfor delay '00:00:05'update TicketTest set totalNub = totalNub - 1   where id='0001' and totalNub > 0";
                //行级锁
                //string sqlstr = "update [dbo].[TicketTest] with(ROWLOCK)  set totalNub=totalNub-1 where id = '0001'";
                int executeRow = db.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sqlstr);
                if (executeRow > 0)
                {
                    Console.WriteLine($"-------------线程{threadId}抢票成功-------------");
                }
                else
                {
                    Console.WriteLine($"线程{threadId}抢票失败");
                }
            }

        }

        #endregion

        #region 乐观锁


        public static void EFUPDLOCK2()
        {

            using (var db = new testEntities())
            {
                Console.WriteLine("开始抢票！抢票有延迟时间期间别人不可修改数据只可查询");
                string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                Console.WriteLine($"线程{threadId}开始抢票");
                //更新锁延迟
                string sqlstr = "declare @flag as TimeStamp SELECT @flag=VersionNum FROM[dbo].[TicketTest]  where id='0001' waitfor delay '00:00:01'update TicketTest set totalNub = totalNub - 1   where id='0001' and versionNum=@flag";

                int executeRow = db.Database.ExecuteSqlCommand(sqlstr);
                if (executeRow > 0)
                {
                    Console.WriteLine($"-------------线程{threadId}抢票成功-------------");
                }
                else
                {
                    Console.WriteLine($"线程{threadId}抢票失败");
                }
            }
        }

        #endregion
    }
}
