using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHelp
{
    /// <summary>
    /// 对类先进先出
    /// </summary>
    public class QueueTest
    {
        public static Queue<string> q = new Queue<string>();

        #region 获取队列数量
        public int GetCount()
        {

            return q.Count;
        }
        #endregion

        #region 队列添加数据
        public void IntoData(string qStr)
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            q.Enqueue(qStr);
            Console.WriteLine($"队列添加数据: {qStr};当前线程id:{threadId}");
        }
        #endregion

        #region 队列输出数据

        public string OutData()
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            string str = q.Dequeue();
            Console.WriteLine($"队列输出数据: {str};当前线程id:{threadId}");
            return str;
        }
        #endregion

    }
}
