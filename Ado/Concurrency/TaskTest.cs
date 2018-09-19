using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHelp
{
    class TaskTest
    {

        #region 队列的操作模拟
        public static void QueueMian()
        {
            QueueA();
            QueueB();
        }
        public static async void QueueA()
        {
            QueueTest queue = new QueueTest();
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    queue.IntoData("QueueA" + i);
                }
            });
            await task;
            Console.WriteLine("QueueAA插入完成,进行输出:");

            while (queue.GetCount() > 0)
            {
                queue.OutData();
            }
        }

        public static async void QueueB()
        {
            QueueTest queue = new QueueTest();
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    queue.IntoData("QueueB" + i);
                }
            });
            await task;
            Console.WriteLine("QueueB插入完成,进行输出:");

            while (queue.GetCount() > 0)
            {
                queue.OutData();
            }
        }
        #endregion

        #region SQL操作模拟

        public static void SQLMian()
        {
            Console.WriteLine("注意马上开始抢票！显示票数信息");
            int totalNub = SQLLockTest.GetData();
            Console.WriteLine($"现在剩余票数:{totalNub}");
            SQLA();
            SQLB();
        }
        public static async void SQLA()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {

                    int totalNub = SQLLockTest.GetData();
                    if (totalNub > 0)
                    {
                        SQLLockTest.SqlUPDLOCK();
                    }
                }
            });
            await task;
            Console.WriteLine("SQLA抢票结束");

        }

        public static async void SQLB()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    int totalNub = SQLLockTest.GetData();
                    if (totalNub > 0)
                    {
                        SQLLockTest.SqlUPDLOCK();
                    }
                }
            });
            await task;
            Console.WriteLine("SQLB抢票结束");
        }
        #endregion

        #region EF操作模拟

        public static void EFMian()
        {
            Console.WriteLine("注意马上开始抢票！显示票数信息");
            int totalNub = EFLockTest.GetData();
            Console.WriteLine($"现在剩余票数:{totalNub}");
            EFA();
            EFB();
        }
        public static async void EFA()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {

                    int totalNub = EFLockTest.GetData();
                    if (totalNub > 0)
                    {
                        EFLockTest.EFUPDLOCK();
                    }
                }
            });
            await task;
            Console.WriteLine("EFA抢票结束");

        }

        public static async void EFB()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    int totalNub = EFLockTest.GetData();
                    if (totalNub > 0)
                    {
                        EFLockTest.EFUPDLOCK();
                    }
                }
            });
            await task;
            Console.WriteLine("EFB抢票结束");
        }
        #endregion

    }
}
