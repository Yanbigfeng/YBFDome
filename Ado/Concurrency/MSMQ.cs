using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace AdoHelp.Concurrency
{ /// <summary>
  /// MSMQ消息队列
  /// </summary>
    class MSMQ
    {
        //.代表的是本机 Private $专用队列 myQueue名字
        static string path = ".\\Private$\\myQueue";
        static MessageQueue queue;
        public static void Createqueue(string queuePath)
        {
            try
            {
                if (MessageQueue.Exists(queuePath))
                {
                    Console.WriteLine("消息队列已经存在");
                    //获取这个消息队列
                    queue = new MessageQueue(queuePath);
                }
                else
                {
                    //不存在，就创建一个新的，并获取这个消息队列对象
                    queue = MessageQueue.Create(queuePath);
                    path = queuePath;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        #region 获取消息队列的数量
        public static int GetMessageCount()
        {
            try
            {
                if (queue != null)
                {
                    int count = queue.GetAllMessages().Length;
                    Console.WriteLine($"消息队列数量：{count}");
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (MessageQueueException e)
            {

                Console.WriteLine(e.Message);
                return 0;
            }


        }
        #endregion

        #region 发送消息到队列
        public static void SendMessage(string qStr)
        {
            try
            {
                //连接到本地队列

                MessageQueue myQueue = new MessageQueue(path);

                //MessageQueue myQueue = new MessageQueue("FormatName:Direct=TCP:192.168.12.79//Private$//myQueue1");

                //MessageQueue rmQ = new MessageQueue("FormatName:Direct=TCP:121.0.0.1//private$//queue");--远程格式

                Message myMessage = new Message();

                myMessage.Body = qStr;

                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                //发生消息到队列中

                myQueue.Send(myMessage);

                string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                Console.WriteLine($"消息发送成功: {qStr};当前线程id:{threadId}");
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region 连接消息队列读取消息
        public static void ReceiveMessage()
        {
            MessageQueue myQueue = new MessageQueue(path);


            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            try

            {

                //从队列中接收消息

                Message myMessage = myQueue.Receive(new TimeSpan(10));// myQueue.Peek();--接收后不消息从队列中移除
                myQueue.Close();

                string context = myMessage.Body.ToString();
                string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                Console.WriteLine($"--------------------------消息内容: {context};当前线程id:{threadId}");

            }

            catch (System.Messaging.MessageQueueException e)

            {

                Console.WriteLine(e.Message);

            }

            catch (InvalidCastException e)

            {

                Console.WriteLine(e.Message);

            }

        }
        #endregion
    }
}
