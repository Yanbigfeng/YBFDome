using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHelp.Concurrency
{
    class TaskTest
    {

        #region 队列的操作模拟
        public static async void QueueMian()
        {
            QueueA();
            QueueB();
        }
        private static async void QueueA()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    QueueTest.IntoData("QueueA" + i);
                }
            });
            await task;
            Console.WriteLine("QueueA插入完成,进行输出:");
        }

        private static async void QueueB()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    QueueTest.IntoData("QueueB" + i);
                }
            });
            await task;
            Console.WriteLine("QueueB插入完成,进行输出:");

        }

        public static void QueueC()
        {
            Console.WriteLine("Queue插入完成,进行输出:");
            while (QueueTest.GetCount() > 0)
            {
                QueueTest.OutData2();
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
        private static async void SQLA()
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

        private static async void SQLB()
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
        private static async void EFA()
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

        private static async void EFB()
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

        #region 消息队列的操作模拟
        public static void MSMQMian()
        {
            MSMQ.Createqueue(".\\Private$\\myQueue");
            MSMQA();
            MSMQB();
            Console.WriteLine("MSMQ结束");
        }
        private static async void MSMQA()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    MSMQ.SendMessage("MSMQA" + i);
                }
            });
            await task;
            Console.WriteLine("MSMQA发送完成,进行读取:");

            while (MSMQ.GetMessageCount() > 0)
            {
                MSMQ.ReceiveMessage();
            }
        }

        private static async void MSMQB()
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    MSMQ.SendMessage("MSMQB" + i);
                }
            });
            await task;
            Console.WriteLine("MSMQB发送完成,进行读取:");

            while (MSMQ.GetMessageCount() > 0)
            {
                MSMQ.ReceiveMessage();
            }
        }
        #endregion

    }
}
