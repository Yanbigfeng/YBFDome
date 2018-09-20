using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;

namespace AdoHelp.Concurrency
{
    /// <summary>
    /// 对类先进先出
    /// </summary>
    public static class QueueTest
    {
        //public static Queue<string> q = new Queue<string>();
        public static ConcurrentQueue<string> q = new ConcurrentQueue<string>();
        //public static Queue q =Queue.Synchronized(new Queue());

        #region 获取队列数量
        public static int GetCount()
        {

            return q.Count;
        }
        #endregion

        #region 队列添加数据
        public static void IntoData(string qStr)
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            q.Enqueue(qStr);
            System.Threading.Thread.Sleep(10);
            Console.WriteLine($"队列添加数据: {qStr};当前线程id:{threadId}");
        }
        #endregion

        #region 队列输出数据
        public static string OutData2()
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            foreach (var item in q)
            {

                Console.WriteLine($"------队列输出数据: {item};当前线程id:{threadId}");
                string d="";
                q.TryDequeue( out d);
            }

            return "211";
        }
        #endregion

    }
}
